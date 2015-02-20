using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DAO;
using PANE.Framework.Approval.DTO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;
using System.Data.Sql;
using System.Linq;
using PANE.Framework.Functions.DAO;
using System.Collections;
using PANE.Framework.DTO;
using System.Web.Security;

namespace PANE.Framework.Approval.DAO
{
    public class ApprovalDAO : CoreDAO<DTO.Approval, long>
    {
        private static string mailRecepient;

        public static List<DTO.Approval> SearchBy(DateTime requestedDateFrom, DateTime requestedDateTo, string entityName,
           string initiatorLastName, string initiatorOtherNames, string approverLastName, string approverOtherNames, Int32 status, long functionID, List<long> userIDs)
        {
            List<DTO.Approval> results = new List<PANE.Framework.Approval.DTO.Approval>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(DTO.Approval));

                DateTime dateTo = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                DateTime dateFrom = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                if (requestedDateFrom > DateTime.MinValue)
                    dateFrom = requestedDateFrom;
                if (requestedDateTo > DateTime.MinValue)
                    dateTo = requestedDateTo;

                criteria.Add(Expression.Between("DateRequested", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));

                if (!String.IsNullOrEmpty(entityName) && !String.IsNullOrEmpty(entityName.Trim()))
                {
                    criteria.Add(Expression.Like("EntityName", entityName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverLastName) && !String.IsNullOrEmpty(approverLastName.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverLastName", approverLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverOtherNames) && !String.IsNullOrEmpty(approverOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverOtherNames", approverOtherNames, MatchMode.Anywhere));
                }

                if (status > 0)
                {
                    criteria.Add(Expression.Eq("Status", (DTO.ApprovalStatus)status));
                }
                if (!String.IsNullOrEmpty(initiatorLastName) && !String.IsNullOrEmpty(initiatorLastName.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorLastName", initiatorLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(initiatorOtherNames) && !String.IsNullOrEmpty(initiatorOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorOtherNames", initiatorOtherNames, MatchMode.Anywhere));
                }

                if (functionID > 0)
                {
                    criteria.Add(Expression.Eq("ApprovalFunction", PANE.Framework.Functions.DAO.FunctionDAO.Retrieve(functionID)));
                }

                if (userIDs != null)
                {
                    criteria.Add(Expression.In("UserID", userIDs));
                }
                else
                {
                    criteria.Add(Expression.IsNull("UserID"));
                }
                results = criteria.AddOrder(Order.Desc("DateRequested")).List<DTO.Approval>() as List<DTO.Approval>;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }


        private static ICriteria SearchByCriteria(DateTime requestedDateFrom, DateTime requestedDateTo, string entityName,
         string initiatorLastName, string initiatorOtherNames, string approverLastName, string approverOtherNames, Int32 status, long functionID, List<long> userIDs)
        {
            ISession session = BuildSession();
            
                ICriteria criteria = session.CreateCriteria(typeof(DTO.Approval));

                DateTime dateTo = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                DateTime dateFrom = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                if (requestedDateFrom > DateTime.MinValue)
                    dateFrom = requestedDateFrom;
                if (requestedDateTo > DateTime.MinValue)
                    dateTo = requestedDateTo;

                /*if (!dateFrom.Equals(DateTime.MinValue) && !dateTo.Equals(DateTime.MinValue))
                {
                    criteria.Add(Expression.Between("DateRequested", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));
                }*/
                if (!String.IsNullOrEmpty(entityName) && !String.IsNullOrEmpty(entityName.Trim()))
                {
                    criteria.Add(Expression.Like("EntityName", entityName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverLastName) && !String.IsNullOrEmpty(approverLastName.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverLastName", approverLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverOtherNames) && !String.IsNullOrEmpty(approverOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverOtherNames", approverOtherNames, MatchMode.Anywhere));
                }

                if (status > 0)
                {
                    criteria.Add(Expression.Eq("Status", (DTO.ApprovalStatus)status));
                }
                if (!String.IsNullOrEmpty(initiatorLastName) && !String.IsNullOrEmpty(initiatorLastName.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorLastName", initiatorLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(initiatorOtherNames) && !String.IsNullOrEmpty(initiatorOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorOtherNames", initiatorOtherNames, MatchMode.Anywhere));
                }

                if (functionID > 0)
                {
                    criteria.Add(Expression.Eq("ApprovalFunction", PANE.Framework.Functions.DAO.FunctionDAO.Retrieve(functionID)));
                }

                if (userIDs != null)
                {
                    criteria.Add(Expression.In("UserID", userIDs));
                }
                else
                {
                    criteria.Add(Expression.IsNull("UserID"));
                }
                
            return criteria;
        }


        public static IList<DTO.Approval> SearchBy(DateTime requestedDateFrom, DateTime requestedDateTo, string entityName,
         string initiatorLastName, string initiatorOtherNames, string approverLastName, string approverOtherNames, Int32 status, long functionID, List<long> userIDs,
            int start, int limit)
        {
            return SearchByCriteria(requestedDateFrom, requestedDateTo, entityName, initiatorLastName,
             initiatorOtherNames, approverLastName, approverOtherNames, status, functionID, userIDs).AddOrder(Order.Desc("DateRequested")).SetFirstResult(start).SetMaxResults(limit).List<DTO.Approval>();

        }

        public static int SearchByCount(DateTime requestedDateFrom, DateTime requestedDateTo, string entityName,
         string initiatorLastName, string initiatorOtherNames, string approverLastName, string approverOtherNames, Int32 status, long functionID, List<long> userIDs,
            int start, int limit)
        {
            int totalCount = Convert.ToInt32(
                SearchByCriteria(requestedDateFrom, requestedDateTo, entityName, initiatorLastName,
             initiatorOtherNames, approverLastName, approverOtherNames, status, functionID, userIDs)
                .SetProjection(Projections.RowCount()).UniqueResult()
                );
            return totalCount;
        }

        
        public static List<DTO.Approval> SearchBy(DateTime requestedDateFrom, DateTime requestedDateTo, string entityName,
            string initiatorLastName, string initiatorOtherNames, string approverLastName, string approverOtherNames, Int32 status, long functionID)
        {
            List<DTO.Approval> results = new List<PANE.Framework.Approval.DTO.Approval>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(DTO.Approval));

                DateTime dateTo = (DateTime)System.Data.SqlTypes.SqlDateTime.MaxValue;
                DateTime dateFrom = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                if (requestedDateFrom > DateTime.MinValue)
                    dateFrom = requestedDateFrom;
                if (requestedDateTo > DateTime.MinValue)
                    dateTo = requestedDateTo;

                criteria.Add(Expression.Between("DateRequested", dateFrom.Date, dateTo.Date.AddDays(1).AddSeconds(-1)));

                if (!String.IsNullOrEmpty(entityName) && !String.IsNullOrEmpty(entityName.Trim()))
                {
                    criteria.Add(Expression.Like("EntityName", entityName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverLastName) && !String.IsNullOrEmpty(approverLastName.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverLastName", approverLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(approverOtherNames) && !String.IsNullOrEmpty(approverOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("ApproverOtherNames", approverOtherNames, MatchMode.Anywhere));
                }

                if (status > 0)
                {
                    criteria.Add(Expression.Eq("Status", (DTO.ApprovalStatus)status));
                }
                if (!String.IsNullOrEmpty(initiatorLastName) && !String.IsNullOrEmpty(initiatorLastName.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorLastName", initiatorLastName, MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(initiatorOtherNames) && !String.IsNullOrEmpty(initiatorOtherNames.Trim()))
                {
                    criteria.Add(Expression.Like("InitiatorOtherNames", initiatorOtherNames, MatchMode.Anywhere));
                }
                if (functionID == 0)
                {
                            FunctionsMembershipUser theUser = Membership.GetUser() as FunctionsMembershipUser;

                    //IList<UserRole> roles = PANE.Framework.Functions.FunctionsEngine.RetrieveAuthorizedSubUserRolesForFunction(theUserRoleFunction);
                    //List<long> InCriteria = roles.Select(usr => usr.ID).ToList();
                    //criteria.Add(Expression.In("Role.ID", InCriteria));
                            IList<Function> function = new PANE.Framework.Approval.ApprovalEngine().RetrieveFunctionsForApproval(theUser.UserDetails.Role, theUser.UserDetails.Role.UserCategory, theUser.UserDetails.InstitutionID);

                            List<long> InCriteria = function.Select(fun => fun.ID).ToList();
                            criteria.Add(Expression.In("ApprovalFunction", InCriteria));
                }

                if (functionID > 0)
                {
                    criteria.Add(Expression.Eq("ApprovalFunction.ID", functionID));
                }
                results = criteria.List<DTO.Approval>() as List<DTO.Approval>;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        public static void FillUserLastNameAndOtherNames()
        {
            List<DTO.Approval> approvals = ApprovalDAO.RetrieveAll();
            foreach (DTO.Approval approval in approvals)
            {
                if (approval.UserID > 0)
                {
                    approval.InitiatorLastName = UserDAO.Retrieve(approval.UserID).LastName;
                    approval.InitiatorOtherNames = UserDAO.Retrieve(approval.UserID).OtherNames;
                    ApprovalDAO.Update(approval);
                }
                ApprovalDAO.CommitChanges();
            }
        }

        public static IList GetApprovalSublist(long approvalID)
        {
            IApprovalSubList realApproval = GetApprovalWithSublist(approvalID);
            if (realApproval != null)
            {
                return realApproval.SubList;
            }
            return null;
        }

        public static IApprovalSubList GetApprovalWithSublist(long approvalID)
        { 
            DTO.Approval approval = PANE.Framework.Approval.DAO.ApprovalDAO.Retrieve(approvalID);
            if (approval != null && approval.DataAfter != null)
            {
                return PANE.Framework.Utility.BinarySerializer.DeSerializeObject(approval.DataAfter) as IApprovalSubList;
            }
            return null;
        }

        public static string GetLastSerialApprovalEntityNameForTheDay(string descriminator)
        {
            DTO.Approval approval = BuildSession().CreateCriteria(typeof(DTO.Approval)).Add(Expression.Between("DateRequested", DateTime.Today, DateTime.Today.AddDays(1).AddSeconds(-1))).Add(Expression.Like("EntityName", descriminator, MatchMode.Anywhere)).AddOrder(Order.Desc("DateRequested")).SetMaxResults(1).UniqueResult<DTO.Approval>();
            if (approval != null)
            {
                return approval.EntityName;
            }
            return string.Empty;
        }

    }
}
