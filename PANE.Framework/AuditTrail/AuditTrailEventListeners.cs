using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Event;
using System.Web;
using PANE.Framework.DTO;
using PANE.Framework.AuditTrail.Attributes;
using System.Reflection;
using NHibernate;
using PANE.Framework.AuditTrail.Enums;
using NHibernate.Engine;

namespace PANE.Framework.AuditTrail
{
    internal class AuditEventListener : NHibernate.Event.Default.DefaultMergeEventListener,  IPreUpdateEventListener,IPreInsertEventListener, IMergeEventListener, IPreDeleteEventListener,IPostInsertEventListener,IPostUpdateEventListener,IPostDeleteEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent e)
        {
            LogAuditTrail(AuditAction.UPDATE, e.Session, e.Entity, e.OldState, e.Persister.PropertyNames);
            return false;
        }
        public bool OnPreInsert(PreInsertEvent e)
        {
            LogAuditTrail(AuditAction.CREATE, e.Session, e.Entity, null, e.Persister.PropertyNames);
            return false;
        }
        public bool OnPostUpdate(PostUpdateEvent e)
        {
            LogAuditTrail(AuditAction.UPDATE, e.Session, e.Entity, e.OldState, e.Persister.PropertyNames);
            foreach (var collection in e.Session.PersistenceContext.CollectionEntries.Values)
            {
                var collectionEntry = collection as CollectionEntry;
                collectionEntry.IsProcessed = true;
            }

            return false;
        }
        public bool OnPostInsert(PostUpdateEvent e)
        {
            LogAuditTrail(AuditAction.CREATE, e.Session, e.Entity, null, e.Persister.PropertyNames);
            return false;
        }
        #region IMergeEventListener Members

        public override void OnMerge(MergeEvent @event)
        {
            if (@event.Original != null)
            {
                
                Object originalObject = @event.Session.Get(@event.Original.GetType(), ((@event.Original) as DataObject).ID);
                LogAuditTrail(AuditAction.UPDATE, @event.Session, @event.Original, originalObject);
                //@event.Original=originalObject;
                
         
            base.OnMerge(@event);
            
            }
        }

        #endregion

        #region IPreDeleteEventListener Members

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            LogAuditTrail(AuditAction.DELETE, @event.Session, @event.Entity, @event.DeletedState, @event.Persister.PropertyNames);
            return false;
        }

        private void LogAuditTrail(AuditAction action, ISession session, object entity, object[] previousState, string[] propertyNames)
        {
            object prevObject = null;
            if (previousState != null)
            {
                prevObject = Activator.CreateInstance(entity.GetType(), true);
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    PropertyInfo prop = prevObject.GetType().GetProperty(propertyNames[i]);
                    if (prop != null)
                    {
                        prop.SetValue(prevObject, previousState[i], null);
                    }
                }

            }

            LogAuditTrail(action, session, entity, prevObject);
        }

        private void LogAuditTrail(AuditAction action, ISession session, object entity, object prevEntity)
        {

            //Class must have Loggable attribute to be enabled for Audit Trail.
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                IAuditable iAuditable = entity as IAuditable;
                long userID = 0;
                //iAuditable.ActionInitiatorUserID = Convert.ToInt64(HttpContext.Current.Session["UserID"]);
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserID"] != null)
                {
                    userID = Convert.ToInt64(HttpContext.Current.Session["UserID"]);

                }
                if (iAuditable.ActionInitiatorUserID == 0)
                {
                    iAuditable.ActionInitiatorUserID = userID;
                }
                else
                {
                    userID = iAuditable.ActionInitiatorUserID;

                }
                DataObject dt = entity as DataObject;
                if (entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false).Length > 0 && iAuditable != null && iAuditable.ActionInitiatorUserID > 0&&!dt.OmitApprovalLog)
                {
                    PANE.Framework.Managed.AuditTrail.DTO.AuditTrail trail = new PANE.Framework.Managed.AuditTrail.DTO.AuditTrail();
                    switch (action)
                    {
                        case AuditAction.CREATE:
                            trail.Action = Managed.AuditTrail.Enums.AuditAction.CREATE;
                            break;
                        case AuditAction.DELETE:
                            trail.Action = Managed.AuditTrail.Enums.AuditAction.DELETE;
                            break;
                        case AuditAction.LOGIN:
                            trail.Action = Managed.AuditTrail.Enums.AuditAction.LOGIN;
                            break;
                        case AuditAction.LOGOUT:
                            trail.Action = Managed.AuditTrail.Enums.AuditAction.LOGOUT;
                            break;
                        case AuditAction.UPDATE:
                            trail.Action = Managed.AuditTrail.Enums.AuditAction.UPDATE;
                            break;
                    }
                    trail.ApplicationName = "Card Issuance";
                    

                    //Get current user id

                    if (iAuditable != null)
                    {
                        trail.UserName = iAuditable.ActionInitiatorUserName.Contains(':') ? iAuditable.ActionInitiatorUserName.Split(':')[0] : iAuditable.ActionInitiatorUserName;
                    }
                    trail.UserID = userID;

                    trail.SubjectIdentifier = (iAuditable as IEntity).Name;
                    trail.ActionDate = DateTime.Now;                                     //Time action happened.
                    trail.ClientIPAddress = HttpContext.Current.Request.UserHostAddress; //IpAddress of the computer.

                    try
                    {
                        trail.ClientName = System.Net.Dns.GetHostEntry(trail.ClientIPAddress).HostName;//The name of the computer, if available.
                    }
                    catch
                    {
                        trail.ClientName = "[Could not resolve Client Name]";
                    }
                    string name = string.Empty;
                    trail.ObjectID = (entity as DataObject).ID;   //The unique ID of the object.
                    name = ((LoggableAttribute)entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false)[0]).LoggedName;
                    if (name == string.Empty) name = entity.GetType().Name;
                    trail.DataType = name;

                    object currentObject = entity;
                    if (action == AuditAction.DELETE)
                    {
                        currentObject = null;
                    }

                    trail.Data = Utility.BinarySerializer.SerializeData(prevEntity, currentObject);
                    
                    //Get MfbCode
                    string mfbCode = "";
                    if (!String.IsNullOrEmpty(iAuditable.ActionInitiatorUserName))
                    {
                        string[] splitString = iAuditable.ActionInitiatorUserName.Split(':');
                        if (splitString != null && splitString.Length > 1 && !String.IsNullOrEmpty(splitString[1]))
                        {
                            mfbCode = splitString[1];
                        }
                    }

                    ISession localSession = PANE.Framework.Managed.NHibernateManager.NHibernateSessionManager.Instance.GetSession(mfbCode, Managed.NHibernateManager.Configuration.DatabaseSource.Local);
                    localSession.Save(trail);
                    localSession.Flush();
                }
            }
        }
        

        #endregion

        public bool OnPostDelete(PostDeleteEvent @event)
        {
            LogAuditTrail(AuditAction.DELETE, @event.Session, @event.Entity, @event.DeletedState, @event.Persister.PropertyNames);
            return false;
        }

        #region IPostDeleteEventListener Members

        void IPostDeleteEventListener.OnPostDelete(PostDeleteEvent @event)
        {
            LogAuditTrail(AuditAction.DELETE, @event.Session, @event.Entity, @event.DeletedState, @event.Persister.PropertyNames);
            
        }

        #endregion

        #region IPostUpdateEventListener Members

        void IPostUpdateEventListener.OnPostUpdate(PostUpdateEvent e)
        {
            LogAuditTrail(AuditAction.UPDATE, e.Session, e.Entity, e.OldState, e.Persister.PropertyNames);
        }

        #endregion

        #region IPostInsertEventListener Members

        public void OnPostInsert(PostInsertEvent e)
        {
            LogAuditTrail(AuditAction.CREATE, e.Session, e.Entity, null, e.Persister.PropertyNames);
        }

        #endregion
    }
}
