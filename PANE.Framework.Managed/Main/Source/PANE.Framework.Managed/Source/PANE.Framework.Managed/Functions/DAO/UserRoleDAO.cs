using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.DAO;
using PANE.Framework.Managed.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using System.Web.Security;
using System.Linq;

namespace PANE.Framework.Managed.Functions.DAO
{
    public class UserRoleDAO : CoreDAO<UserRole, long>
    {
        public static List<UserRole> RetrieveByCodes(string mfbCode, string[] codes)
        {
            if (codes == null || codes.Length == 0)
            {
                return new List<UserRole>();
            }
            List<UserRole> result = new List<UserRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));
                criteria.Add(Expression.In("Code", codes.Distinct().ToArray()));
                result = criteria.List<UserRole>() as List<UserRole>;
            }
            catch { throw; }

            return result;
        }

        public static bool RetrievePermissionsByUserRoleID(string mfbCode, string roleName, long roleID)
        {
            IUser currentUser = (Membership.GetUser() as FunctionsMembershipUser).UserDetails;
            Function roleNameFunction = FunctionDAO.RetrieveByRoleName(roleName, currentUser.Role.UserCategory);
            if (roleNameFunction == null)
            {
                return false;
            }

            IList<UserRoleFunction> userRoleFunctions = UserRoleFunctionDAO.RetrieveByFunctionItemIDAndRoleID(mfbCode, roleNameFunction.ID, currentUser.Role.ID);
            if (userRoleFunctions != null && userRoleFunctions.Count > 0)
            {
                if (!roleNameFunction.HasSubRoles)
                {
                    return true;
                }


                List<long> userRoleFunctionIDs = new List<long>();
                foreach (UserRoleFunction urf in userRoleFunctions)
                {
                    userRoleFunctionIDs.Add(urf.ID);
                }

                //IList<UserRoleFunctionSubRole> subRoles = UserRoleFunctionSubRoleDAO.RetrieveByUserRoleFunctions(mfbCode, userRoleFunctionIDs.ToArray());
                IList<UserRoleFunctionSubRole> subRoles = UserRoleFunctionSubRoleDAO.RetrieveBySubUserRolesAndUserRoleFunctions(mfbCode, new long[] { roleID }, userRoleFunctionIDs.ToArray());
                if (subRoles != null && subRoles.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<long> RetrieveSubUserRoles(string mfbCode, string roleName)
        {
            IUser currentUser = (Membership.GetUser() as FunctionsMembershipUser).UserDetails;
            Function function = FunctionDAO.RetrieveByRoleName(roleName, currentUser.Role.UserCategory);

            IList<UserRoleFunction> userRoleFunctions = UserRoleFunctionDAO.RetrieveByFunctionItemIDAndRoleID(mfbCode, function.ID, currentUser.Role.ID);
            if (userRoleFunctions == null || userRoleFunctions.Count == 0)
            {
                return new List<long>();
            }

            List<long> userRoleFunctionIDs = new List<long>();
            foreach (UserRoleFunction urf in userRoleFunctions)
            {
                userRoleFunctionIDs.Add(urf.ID);
            }

            IList<UserRoleFunctionSubRole> subRoles = UserRoleFunctionSubRoleDAO.RetrieveByUserRoleFunctions(mfbCode, userRoleFunctionIDs.ToArray());
            if (subRoles == null || subRoles.Count == 0)
            {
                return new List<long>();
            }

            List<long> subRoleIDs = new List<long>();
            foreach (UserRoleFunctionSubRole usfs in subRoles)
            {
                subRoleIDs.Add(usfs.TheSubUserRoleID);
            }

            return subRoleIDs;
        }

        public static List<UserRole> RetrieveUserRolesByCurrentUsersPermissions(string mfbCode)
        {
            List<long> subRoleIDs = RetrieveSubUserRoles(mfbCode, "AddandEditUser");
            if (subRoleIDs == null || subRoleIDs.Count == 0)
                return new List<UserRole>();
            return GetByIDs(mfbCode, subRoleIDs.ToArray());
        }

        public static List<UserRole> GetByIDs(string mfbCode, long[] userRoleIDs)
        {
            List<UserRole> result = new List<UserRole>();
            ISession session = BuildSession(mfbCode);

            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));
                criteria.Add(Expression.In("ID", userRoleIDs));

                result = criteria.List<UserRole>() as List<UserRole>;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static UserRole GetUserRoleByName(string mfbCode, string name)
        {
            UserRole userRole = new UserRole();
            ISession session = BuildSession(mfbCode);
            try
            {
                userRole = session.CreateCriteria(typeof(UserRole)).Add(Expression.Eq("Name", name)).UniqueResult<UserRole>();
            }
            catch
            {
                throw;
            }
            return userRole;
        }

        public static UserRole GetUserRoleById(string mfbCode,long Id)
        {
            return Retrieve(mfbCode, Id);
        
        }

        public static List<UserRole> SearchBy(string mfbCode,string name, UserRoleScope? scope, Status? status)
        {
            
            List<UserRole> userRoles = new List<UserRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));
                criteria.AddOrder(Order.Asc("Name"));
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(name.Trim()))
                {
                    criteria.Add(Expression.Like("Name", name.Trim(), MatchMode.Anywhere));
                }
                if (scope != null)
                {
                    criteria.Add(Expression.Eq("Scope", scope));
                }
                if (status != null)
                {
                    criteria.Add(Expression.Eq("Status", status));
                }
                userRoles = criteria.List<UserRole>() as List<UserRole>;
            }
            catch
            {
                throw;
            }
            return userRoles;


        }

        public static List<UserRole> Find(string mfbCode, string name, string code, UserRoleScope? scope, Status? status, int page, int pageSize, string sort, string direction, out int totalCount)
        {
            return Find(mfbCode, name, code, scope, status, null, page, pageSize, sort, direction, out  totalCount);
        }

        public static List<UserRole> Find(string mfbCode, string name, string code, UserRoleScope? scope, Status? status, UserCategory? userCategory, int page, int pageSize, string sort, string direction, out int totalCount)
        {
          
            List<UserRole> result = new List<UserRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));

                //Order in Ascending order of Name
                if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(name.Trim()))
                {
                    criteria.Add(Expression.Like("Name", name.Trim(), MatchMode.Anywhere));
                }

                if (!String.IsNullOrEmpty(code) && !String.IsNullOrEmpty(code.Trim()))
                {
                    criteria.Add(Expression.Like("Code", name.Trim(), MatchMode.Anywhere));
                }

                if (status.HasValue)
                {
                    criteria.Add(Expression.Eq("Status", status.Value));
                }
                if (userCategory.HasValue)
                {
                    criteria.Add(Expression.Eq("UserCategory", userCategory.Value));
                }
                if (scope.HasValue)
                {
                    criteria.Add(Expression.Eq("Scope", scope.Value));
                }

                //This part is for the paging.
                //Bugzz. Wont work over here.
                //criteria.SetFirstResult(page * pageSize).SetMaxResults(pageSize);


                //Before doing the sorting, i get a count Criteria so that it doesnt crash.
                ICriteria countCriteria = CriteriaTransformer.Clone(criteria).SetProjection(Projections.RowCountInt64());
                ICriteria listCriteria = CriteriaTransformer.Clone(criteria).SetFirstResult(page * pageSize).SetMaxResults(pageSize);


                //This section then performs the sort operations on the list. Sorting defaults to the Name column
                if (direction == "Default")
                {
                    listCriteria.AddOrder(Order.Desc("ID"));
                }
                else
                {
                    if (direction == "DESC")
                    {
                        listCriteria.AddOrder(Order.Desc(sort));
                    }
                    else
                    {
                        listCriteria.AddOrder(Order.Asc(sort));
                    }
                }
                //Add the two criteria to the session and retrieve their result.
                IList allResults = session.CreateMultiCriteria().Add(listCriteria).Add(countCriteria).List();

                foreach (var o in (IList)allResults[0])
                {
                    result.Add((UserRole)o);
                }

                totalCount = Convert.ToInt32((long)((IList)allResults[1])[0]);
            }
            catch
            {
                throw;
            }

             
            return result;

        }

    }
}
