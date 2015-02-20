using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SOA.Framework.FunctionHelper;
using PANE.Framework.Managed.DAO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.Managed.NHibernateManager.Configuration;
using PANE.Framework.Managed.Functions.DTO;

namespace SOA.Framework.FunctionHelper.DAO
{
    public class FunctionItemDAO : CoreDAO<FunctionItem, long>
    {
        public static List<FunctionItem> GetByIDsAndUserCategory(long? institutionID, long[] ids, UserCategory userCategory)
        {
            List<FunctionItem> result = new List<FunctionItem>();

            ISession session = BuildSession("", DatabaseSource.Core);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(FunctionItem));
                criteria.Add(Expression.In("ID", ids));
                criteria.Add(Expression.Eq("UserCategory", userCategory));
                if (institutionID.HasValue)
                {
                    criteria.Add(Expression.Or(Expression.IsNull("InstitutionID"), Expression.Eq("InstitutionID", institutionID)));
                }
                
                result = criteria.List<FunctionItem>() as List<FunctionItem>;
            }
            catch
            {
                throw;
            }

            return result;
        }



        public static List<FunctionItem> RetrieveFunctions(long? institutionID, UserCategory userCategory)
        {
            List<FunctionItem> result = new List<FunctionItem>();
            ISession session = BuildSession("", DatabaseSource.Core);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(FunctionItem));

                //criteria.Add(Expression.Eq("IsDefault", true));
                criteria.Add(Expression.Or(Expression.Eq("UserCategory", userCategory),
                                           Expression.IsNull("UserCategory")));
                
                if (institutionID.HasValue)
                {
                    criteria.Add(Expression.Or(Expression.IsNull("InstitutionID"), Expression.Eq("InstitutionID", institutionID)));
                }
                
                criteria.AddOrder(Order.Asc("ParentFunctionItem")).AddOrder(Order.Asc("Name"));

                result = criteria.List<FunctionItem>() as List<FunctionItem>;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static List<FunctionItem> RetrieveFunctionsForApprovalConfiguration(long? institutionID,  UserCategory userCategory)
        {
            List<FunctionItem> result = new List<FunctionItem>();
            ISession session = BuildSession("", DatabaseSource.Core);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(FunctionItem));

                //criteria.Add(Expression.Eq("IsDefault", true));
                criteria.Add(Expression.Or(Expression.Eq("UserCategory", userCategory),
                                           Expression.IsNull("UserCategory")));
                criteria.Add(Expression.Eq("IsApprovable", true));
                if (institutionID.HasValue)
                {
                    criteria.Add(Expression.Or(Expression.IsNull("InstitutionID"), Expression.Eq("InstitutionID", institutionID)));
                }
                criteria.AddOrder(Order.Asc("ParentFunctionItem"));
                result = criteria.List<FunctionItem>() as List<FunctionItem>;
            }
            catch
            {
                throw;
            }

            return result;
        }
      
        //Parameter mfbCode not needed unlike the other classes.
        public static FunctionItem RetrieveByRoleName(string roleName, UserCategory userCategory)
        {
            FunctionItem result = new FunctionItem();
            ISession session = BuildSession("",DatabaseSource.Core);
            try
            {
                result = session.CreateCriteria(typeof(FunctionItem))
                    .Add(Expression.Eq("RoleName", roleName))
                    .Add(Expression.Eq("UserCategory", userCategory))
                    .UniqueResult<FunctionItem>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static List<FunctionItem> RetreiveByIDs(long? institutionID, long[] functionItemIDs)
        {
            List<FunctionItem> result = new List<FunctionItem>();
            ISession session = BuildSession("", DatabaseSource.Core);
            try
            {

                if (!institutionID.HasValue)
                {
                    result = session.CreateCriteria(typeof(FunctionItem))
                        .Add(Expression.In("ID", functionItemIDs))
                        .AddOrder(Order.Asc("ParentFunctionItem")).AddOrder(Order.Asc("Name"))
                        .List<FunctionItem>() as List<FunctionItem>;
                }
                else
                {

                    result = session.CreateCriteria(typeof(FunctionItem))
                        .Add(Expression.In("ID", functionItemIDs))
                        .AddOrder(Order.Asc("ParentFunctionItem")).AddOrder(Order.Asc("Name"))
                        .Add(Expression.Or(Expression.IsNull("InstitutionID"), Expression.Eq("InstitutionID", institutionID)))
                        .List<FunctionItem>() as List<FunctionItem>;
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static FunctionItem RetreiveByID(long id)
        {
          return  Retrieve("", id, DatabaseSource.Core);
        
        }
        public static List<FunctionItem> GetFunctionItemsForCurrentUser(string userName)
        {
            //Get the Current User
            PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser membershipUser = System.Web.Security.Membership.GetUser() as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
            if (membershipUser == null)
            {
                return new List<FunctionItem>(); 
            }


            string mfbCode = userName.Split(':')[1];
            long userRoleID = membershipUser.UserDetails.Role.ID; //Get the Current Users' Role ID

            //Get the UserRole FunctionItems for the current users' role.
            List<UserRoleFunctionItem> urfs = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, userRoleID);

            long? institutionID = null;
            if (!String.IsNullOrEmpty(mfbCode))
            {
                PANE.Framework.Managed.MfbServiceRef.Mfb institution = null;
                using (PANE.Framework.Managed.MfbServiceRef.MfbServiceClient institutionClient = new PANE.Framework.Managed.MfbServiceRef.MfbServiceClient())
                {
                    institution = institutionClient.GetByCode(mfbCode);
                    institutionID = institution.ID;
                }
            }
            //Use the functionItem IDs and retreive the corresponding FunctionItems.
            long[] functionItemIDs = urfs.Select(f => f.TheFunctionItemID).Distinct().ToArray();
            List<FunctionItem> functionItemsForCurrentUser = new List<FunctionItem>(); 
            if(functionItemIDs.ToList<long>().Count > 0)  
            functionItemsForCurrentUser = FunctionItemDAO.RetreiveByIDs(institutionID,  functionItemIDs);
            return functionItemsForCurrentUser;
        }



/*
        public static List<FunctionItem> GetFunctionUsingApplicationID(string mfbCode, long appID)
        {
            List<FunctionItem> results = new List<FunctionItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                results = session.CreateCriteria(typeof(FunctionItem)).Add(Expression.Eq("ApplicationID", appID)).List<FunctionItem>().ToList();
            }
            catch
            {
                throw;
            }

            return results;
        }
        
        public static List<FunctionItem> RetrieveFunctionsForApprovalConfiguration(string mfbCode)
        {
            List<FunctionItem> functions = new List<FunctionItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                IList<FunctionItem> allFunctions = session.CreateCriteria(typeof(FunctionItem)).Add(Expression.Eq("IsApprovable", true)).List<Function>() as List<Function>;

                List<FunctionItem> repFunctions = allFunctions.Where(theFunc => theFunc.HasSubRoles).ToList();
                functions = allFunctions.Where(theFunc => !theFunc.HasSubRoles).ToList();
                foreach (UserRole subRoles in UserRoleDAO.RetrieveAll(mfbCode))
                {
                    foreach (Function fun in repFunctions)
                    {
                        Function fc = new Function()
                        {
                            ID = fun.ID,
                            RoleName = fun.RoleName,
                            Description = fun.Description,
                            Name = string.Format("{0} [{1}]", fun.Name, subRoles.Name),
                            HasSubRoles = fun.HasSubRoles,
                            SubUserRoleUpdate = subRoles
                        };
                        fc.TheApprovalConfigurations = fun.TheApprovalConfigurations.Where(theApprov => theApprov.SubUserRole == subRoles).ToList();
                        functions.Add(fc);
                    }

                }
            }
            catch
            {
                throw;
            }

            return functions;
        }
        
        public static IEnumerable<FunctionItem> RetrieveFunctionsForUserRole(string mfbCode)
        {
            ISession session = BuildSession(mfbCode);
            return session.CreateCriteria(typeof(FunctionItem)).AddOrder(Order.Asc("ParentFunction")).List<FunctionItem>();

        }
*/

    }

}