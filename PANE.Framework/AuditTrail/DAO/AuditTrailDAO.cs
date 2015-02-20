using System;
using System.Collections; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.DAO;
using PANE.Framework.AuditTrail.Enums;
using PANE.Framework.Utility;
using PANE.Framework.AuditTrail.DTO;
using System.Reflection;
using System.Data.SqlTypes;
using PANE.Framework.AuditTrail.Attributes;
using System.Web.Security;
using PANE.Framework.Functions.DTO;  


namespace PANE.Framework.AuditTrail.DAO
{
    public class AuditTrailDAO : CoreDAO<PANE.Framework.AuditTrail.DTO.AuditTrail, long>
    {
        public static IList RetrieveAllAuditTrails()
        {
            return BuildSession().CreateCriteria(typeof(PANE.Framework.AuditTrail.DTO.AuditTrail)).AddOrder(Order.Asc("DataType")).List() as IList;
        }
        public static IList<PANE.Framework.AuditTrail.DTO.AuditTrail> SearchAuditTrail(DateTime dateFrom, DateTime dateTo, AuditAction? actionType, string dataType, string subject, long? institutionid)
        {
            List<PANE.Framework.AuditTrail.DTO.AuditTrail> result = new List<PANE.Framework.AuditTrail.DTO.AuditTrail>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(PANE.Framework.AuditTrail.DTO.AuditTrail));
                criteria.AddOrder(Order.Desc("ActionDate"));
                if (!dateFrom.Equals(DateTime.MinValue) && !dateTo.Equals(DateTime.MinValue))
                {
                    criteria.Add(Expression.Between("ActionDate", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));
                }
                if (actionType.HasValue)
                {
                    criteria.Add(Expression.Eq("Action", actionType));
                }
                if (institutionid.HasValue)
                {
                    criteria.Add(Expression.Eq("InstitutionID", institutionid));

                }

                //if (!String.IsNullOrEmpty(actionBy))
                //{
                //    criteria.Add(Expression.Sql(String.Format("Select * from AuditTrail as at where Concat(at.LastName,' ',at.Othernames) like %{0}%", actionBy)));
                //}

                if (!String.IsNullOrEmpty(dataType) && !String.IsNullOrEmpty(dataType.Trim()))
                {
                    criteria.Add(Expression.Like("DataType", dataType.Trim(), MatchMode.Anywhere));
                }
                if (!String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(subject.Trim()))
                {
                    criteria.Add(Expression.Like("SubjectIdentifier", subject.Trim(), MatchMode.Anywhere));
                }
                FunctionsMembershipUser theUser = Membership.GetUser() as FunctionsMembershipUser;
                if (theUser.UserDetails.Role.UserCategory != PANE.Framework.Functions.DTO.UserCategory.ServiceProvider)
                {
                    criteria.Add(Expression.Eq("UserCategory", theUser.UserDetails.Role.UserCategory));
                    criteria.Add(Expression.Eq("InstitutionID", theUser.UserDetails.InstitutionID));
                }
                result = criteria.List<PANE.Framework.AuditTrail.DTO.AuditTrail>() as List<PANE.Framework.AuditTrail.DTO.AuditTrail>;
            }
            catch
            {
                throw;
            }

            return result;

        }

        public static IList<PANE.Framework.AuditTrail.DTO.AuditTrail> SearchAuditTrailWithUserCategory(DateTime dateFrom, DateTime dateTo, AuditAction? actionType, string dataType, string subject, UserCategory  usercategory)
        {
            List<PANE.Framework.AuditTrail.DTO.AuditTrail> result = new List<PANE.Framework.AuditTrail.DTO.AuditTrail>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(PANE.Framework.AuditTrail.DTO.AuditTrail));
                criteria.AddOrder(Order.Desc("ActionDate"));
                if (!dateFrom.Equals(DateTime.MinValue) && !dateTo.Equals(DateTime.MinValue))
                {
                    criteria.Add(Expression.Between("ActionDate", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));
                }
                if (actionType.HasValue)
                {
                    criteria.Add(Expression.Eq("Action", actionType));
                }

                if (usercategory !=null )
                {
                    criteria.Add(Expression.Eq("UserCategory",  usercategory)); 
                }

                //if (!String.IsNullOrEmpty(actionBy))
                //{
                //    criteria.Add(Expression.Sql(String.Format("Select * from AuditTrail as at where Concat(at.LastName,' ',at.Othernames) like %{0}%", actionBy)));
                //}

                if (!String.IsNullOrEmpty(dataType) && !String.IsNullOrEmpty(dataType.Trim()))
                {
                    criteria.Add(Expression.Like("DataType", dataType.Trim(), MatchMode.Anywhere));
                }
                if (!String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(subject.Trim()))
                {
                    criteria.Add(Expression.Like("SubjectIdentifier", subject.Trim(), MatchMode.Anywhere));
                }

                result = criteria.List<PANE.Framework.AuditTrail.DTO.AuditTrail>() as List<PANE.Framework.AuditTrail.DTO.AuditTrail>;
            }
            catch
            {
                throw;
            }

            return result;

        }
        public static IList<PANE.Framework.AuditTrail.DTO.AuditTrail> SearchAuditTrail(DateTime dateFrom, DateTime dateTo, AuditAction? actionType, string dataType, string subject)
        {
            List<PANE.Framework.AuditTrail.DTO.AuditTrail> result = new List<PANE.Framework.AuditTrail.DTO.AuditTrail>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(PANE.Framework.AuditTrail.DTO.AuditTrail));
                criteria.AddOrder(Order.Desc("ActionDate"));
                if (!dateFrom.Equals(DateTime.MinValue) && !dateTo.Equals(DateTime.MinValue))
                {
                    criteria.Add(Expression.Between("ActionDate", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));
                }
                if (actionType.HasValue)
                {
                    criteria.Add(Expression.Eq("Action", actionType));
                }

                //if (!String.IsNullOrEmpty(actionBy))
                //{
                //    criteria.Add(Expression.Sql(String.Format("Select * from AuditTrail as at where Concat(at.LastName,' ',at.Othernames) like %{0}%", actionBy)));
                //}
              
                if (!String.IsNullOrEmpty(dataType) && !String.IsNullOrEmpty(dataType.Trim()))
                {
                    criteria.Add(Expression.Like("DataType", dataType.Trim(), MatchMode.Anywhere));
                }
                if (!String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(subject.Trim()))
                {
                    criteria.Add(Expression.Like("SubjectIdentifier", subject.Trim(), MatchMode.Anywhere));
                }

                result = criteria.List<PANE.Framework.AuditTrail.DTO.AuditTrail>() as List<PANE.Framework.AuditTrail.DTO.AuditTrail>;
            }
            catch
            {
                throw;
            }

            return result;

        }


        public static List<TrailItem> GetDeserializedObject(byte[] data)
        {
            //return BinarySerializer.Deserialize(DataBefore, DataAfter, DataType, action);
            return BinarySerializer.DeSerializeObject(data) as List<TrailItem>;

        }

        public static List<TrailItem> GetDeserializedObject(Byte[] dataBefore,Byte[] dataAfter)
        {
            return PANE.Framework.Utility.BinarySerializer.DeSerializeObject(dataBefore, dataAfter);
        }

        
    }



   
}
