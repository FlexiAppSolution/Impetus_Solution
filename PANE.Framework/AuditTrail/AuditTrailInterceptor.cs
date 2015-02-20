using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Web.Security;
using System.Web;
using PANE.Framework.Functions.DTO;
using PANE.Framework.AuditTrail.Attributes;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DAO;
using PANE.Framework.DAO;
using System.Reflection;

namespace PANE.Framework.AuditTrail
{
    public class AuditTrailInterceptor : IInterceptor
    {
        private string SYSTEM_CHANGE_TEXT = "[[SYSTEM CHANGE]]";
        public AuditTrailInterceptor(ISession auditTrailLoggerSession)
        {
            this._auditTrailLoggerSession = auditTrailLoggerSession;
        }

        private ISession _auditTrailLoggerSession;

        private ISession _session;
        private List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail> _inserts = new List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail>();
        private List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail> _deletes = new List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail>();
        private List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail> _updates = new List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail>();

        private Dictionary<long, DataObject> dataObjects = new Dictionary<long, DataObject>();
        private int entityIndex = 0;
        // private List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail> _login = new List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail>();
        //  private List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail> _logout = new List<PANE.Framework.Managed.AuditTrail.DTO.AuditTrail>();
        #region IInterceptor Members

        public void AfterTransactionBegin(ITransaction tx)
        {
            // throw new NotImplementedException();
        }

        public void AfterTransactionCompletion(ITransaction tx)
        {
            //throw new NotImplementedException();
        }

        public void BeforeTransactionCompletion(ITransaction tx)
        {
            //throw new NotImplementedException();
        }

        public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            // throw new NotImplementedException();
            return null;
        }

        public object Instantiate(Type type, object id)
        {
            //throw new NotImplementedException();
            return null;
        }

        public object IsUnsaved(object entity)
        {
            //throw new NotImplementedException();
            return null;
        }


        public void OnDelete(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            //Class must have TrailableAttribute attribute to be enabled for Audit Trail.
            object[] trailable = entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false);
            if (trailable.Length > 0)
            {
                //if (!(entity as PANE.Framework.DTO.DataObject).UseAuditTrail)
                //{
                //    return;
                //}
              
                if ((entity as DataObject).OmitApprovalLog)
                {
                    return;
                }
                PANE.Framework.Managed.AuditTrail.DTO.AuditTrail trail = CreateTrail(Managed.AuditTrail.Enums.AuditAction.DELETE, entity, trailable);

                //Serialize just the data to be deleted.
                trail.Data = Utility.BinarySerializer.SerializeData(entity, null);
                _deletes.Add(trail);
            }
        }

        public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            //Class must have Loggable attribute to be enabled for Audit Trail.
            object[] trailable = entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false);
            if (trailable.Length > 0)
            {
                //if (!(entity as DataObject).UseAuditTrail)
                //{
                //    return false;
                //}
                if ((entity as DataObject).OmitApprovalLog)
                {
                    return false;
                }
                PANE.Framework.Managed.AuditTrail.DTO.AuditTrail trail = CreateTrail(Managed.AuditTrail.Enums.AuditAction.UPDATE, entity, trailable);
                //Create a deep copy of the current entity.
                byte[] newObjectBytes = PANE.Framework.Utility.BinarySerializer.SerializeObject(entity);
                object newObject = PANE.Framework.Utility.BinarySerializer.DeSerializeObject(newObjectBytes);

                //Get the old copy.
                _session.Refresh(entity);

                //Get the comparison b/w the old and new data and serialize as bytes.
                trail.Data = Utility.BinarySerializer.SerializeData(entity, newObject);
                _updates.Add(trail);

                return true;
            }
            return false;
        }

        public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            //throw new NotImplementedException();
            return false;
        }


        public bool OnSave(object entity, object id, object[] state, string[] propertyNames, NHibernate.Type.IType[] types)
        {
            //Class must have TrailableAttribute attribute to be enabled for Audit Trail.
            object[] trailable = entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false);
            if (trailable.Length > 0)
            {
                //if (!(entity as DataObject).UseAuditTrail)
                //{
                //    return false;
                //}
                if ((entity as DataObject).OmitApprovalLog)
                {
                    return false;
                }
                PANE.Framework.Managed.AuditTrail.DTO.AuditTrail trail = CreateTrail(Managed.AuditTrail.Enums.AuditAction.CREATE, entity, trailable);

                //Serialize just the data to be saved.
                trail.Data = Utility.BinarySerializer.SerializeData(null, entity);
                _inserts.Add(trail);

                return true;
            }
            return false;
        }


        private PANE.Framework.Managed.AuditTrail.DTO.AuditTrail CreateTrail(Managed.AuditTrail.Enums.AuditAction action, object entity, object[] trailable)
        {
            entityIndex++;
            PANE.Framework.Managed.AuditTrail.DTO.AuditTrail trail = new PANE.Framework.Managed.AuditTrail.DTO.AuditTrail();
            trail.Action = action; //Action currently performed.
            trail.ActionDate = DateTime.Now;                                   //Current date.

            DataObject dataObject = entity as DataObject;
            //trail.Object = new PANE.Framework.Managed.DTO.DataObject;
            trail.ObjectID = dataObject.ID;
            dataObjects.Add(entityIndex, dataObject);
            if (action == Managed.AuditTrail.Enums.AuditAction.CREATE)
            {
                trail.ObjectID = entityIndex;
            }

            //if (!trail.Object.IsASystemChange)
            {

                try
                {
                    if (HttpContext.Current != null)
                    {
                        trail.ClientIPAddress = PANE.Framework.Managed.Utility.IPResolver.GetIP4Address(true); //IP Address of the host computer.
                        trail.ClientName = System.Net.Dns.GetHostEntry(trail.ClientIPAddress).HostName; //The name of host the computer.
                    }
                }
                catch
                {
                    trail.ClientName = "[Could not resolve Client Name]";
                }


                IAuditable iAuditable = entity as IAuditable;
                //TODO: Also get the User Name of the trail.
                //if (auditable != null && auditable.AuditableUser != null)
                //{
                //    trail.UserID = auditable.AuditableUser.ID;
                //    trail.UserName = auditable.AuditableUser.UserName.Split(':')[0];
                //}

                //iAuditable.ActionInitiatorUserID = Convert.ToInt64(HttpContext.Current.Session["UserID"]);
                //if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserID"] != null)
                //{
                //    userID = Convert.ToInt64(HttpContext.Current.Session["UserID"]);
                //}

                //if (iAuditable.ActionInitiatorUserID == 0)
                //{
                //    iAuditable.ActionInitiatorUserID = userID;
                //}

                trail.UserID = iAuditable.ActionInitiatorUserID;
                trail.UserName = iAuditable.ActionInitiatorUserName;
                trail.SubjectIdentifier = (iAuditable as IEntity).Name;
            }


            trail.ApplicationName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];

            try
            {
                IUser iUser = entity as IUser;
                if (iUser != null)//&& !trail.Object.IsASystemChange)
                {
                    trail.SubjectIdentifier = iUser.UserName;
                    trail.DataType = String.Format("User({0})", Convert.ToString(iUser.Role.Name).Trim());
                    trail.ChangeObjectIDWhenSaving = true;
                }
                else
                {
                    //The Data Type of the entity to be persisted, e.g. User, Branch, etc.
                    trail.DataType = ((LoggableAttribute)entity.GetType().GetCustomAttributes(typeof(LoggableAttribute), false)[0]).LoggedName;
                    if (String.IsNullOrEmpty(trail.DataType)) trail.DataType = entity.GetType().Name;
                    trail.DataType = PANE.Framework.Managed.Utility.EnumBinder.SplitAtCapitalLetters(trail.DataType);

                    //PropertyInfo pInfo = entity.GetType().GetProperty((trailable[0] as LoggableAttribute).MainIdentifier);
                    //if (pInfo != null)
                    //{
                    //    object identifier = pInfo.GetValue(entity, null);
                    //    if (identifier != null)
                    //    {
                    //        if (pInfo.PropertyType.IsEnum)
                    //        {
                    //            trail.SubjectIdentifier = PANE.Framework.Managed.Utility.EnumBinder.SplitAtCapitalLetters(identifier.ToString());
                    //            if ((trailable[0] as LoggableAttribute).UseMainIdentifierAsObjectID)
                    //            {
                    //                trail.ChangeObjectIDWhenSaving = false;
                    //                trail.ObjectID = Convert.ToInt32(identifier);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            trail.ChangeObjectIDWhenSaving = true;
                    //            trail.SubjectIdentifier = identifier.ToString();
                    //        }
                    //    }
                    //}

                }
            }
            catch { }

            //if (trail.Object.IsASystemChange)
            //{
            //    trail.ClientIPAddress = SYSTEM_CHANGE_TEXT;
            //    trail.ClientName = SYSTEM_CHANGE_TEXT;
            //    trail.UserName = SYSTEM_CHANGE_TEXT;
            //}

            return trail;
        }

        public void PostFlush(System.Collections.ICollection entities)
        {
            ISession session = _auditTrailLoggerSession;

            try
            {

                string relatedEventsGuid = Guid.NewGuid().ToString();
                foreach (PANE.Framework.Managed.AuditTrail.DTO.AuditTrail auditTrailItem in _inserts)
                {
                    //TODO: Handle appropriately.
                    //auditTrailItem.ObjectID = (auditTrailItem.Object as DataObject).ID;

                    auditTrailItem.ObjectID = dataObjects[auditTrailItem.ObjectID].ID;
                    auditTrailItem.GroupName = relatedEventsGuid;
                    session.Save(auditTrailItem);
                }
                foreach (PANE.Framework.Managed.AuditTrail.DTO.AuditTrail auditTrailItem in _updates)
                {
                    auditTrailItem.GroupName = relatedEventsGuid;
                    session.Save(auditTrailItem);
                }
                foreach (PANE.Framework.Managed.AuditTrail.DTO.AuditTrail auditTrailItem in _deletes)
                {
                    auditTrailItem.GroupName = relatedEventsGuid;
                    session.Save(auditTrailItem);
                }
            }
            catch (HibernateException e)
            {
                throw new CallbackException(e);
            }
            finally
            {
                _inserts.Clear();
                _updates.Clear();
                _deletes.Clear();
                //session.Flush();
                //session.Close();
            }

        }

        public void PreFlush(System.Collections.ICollection entities)
        {
            //throw new NotImplementedException();
        }

        public void SetSession(ISession session)
        {
            //throw new NotImplementedException();
            _session = session;
        }

        #endregion

        #region IInterceptor Members


        public object GetEntity(string entityName, object id)
        {
            //throw new NotImplementedException();
            return null;
        }

        public string GetEntityName(object entity)
        {
            //throw new NotImplementedException();
            return null;
        }

        public object Instantiate(string entityName, EntityMode entityMode, object id)
        {
            //throw new NotImplementedException();
            return null;
        }

        public bool? IsTransient(object entity)
        {
            //throw new NotImplementedException();
            return null;
        }

        public void OnCollectionRecreate(object collection, object key)
        {
            //throw new NotImplementedException();
        }

        public void OnCollectionRemove(object collection, object key)
        {
            //throw new NotImplementedException();
        }

        public void OnCollectionUpdate(object collection, object key)
        {
            //throw new NotImplementedException();
        }

        public NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            return sql;
        }

        #endregion
    }
}
