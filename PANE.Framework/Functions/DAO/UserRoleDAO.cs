using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PANE.Framework.DAO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.DTO;

namespace PANE.Framework.Functions.DAO
{
    public class UserRoleDAO : CoreDAO<UserRole, long>
    {
        public static UserRole GetUserRoleByName(string name)
        {
            UserRole userRole = new UserRole();
            ISession session = BuildSession();
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

        public static UserRole GetUserRoleByNameAndInstitution(string name, long? inst, UserCategory category )
        {
            UserRole userRole = new UserRole();
            ISession session = BuildSession();
            try
            {
                userRole = session.CreateCriteria(typeof(UserRole))
                    .Add(Expression.Eq("Name", name))
                    .Add(Expression.Or(Expression.Eq("InstitutionID", inst), Expression.IsNull("InstitutionID")))
                    .Add(Expression.Eq("UserCategory", category))
                    .UniqueResult<UserRole>();
            }
            catch
            {
                throw;
            }
            return userRole;
        }
        
        public static List<UserRole> SearchUserRole(string rolename, UserRoleScope? scope, Status? status, UserCategory theUserCategory, Int64? institutionID)
        {
            List<UserRole> result = new List<UserRole>();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole)).Add(Expression.Eq("UserCategory", theUserCategory));
                if (institutionID == null)
                {
                    criteria.Add(Expression.IsNull("InstitutionID"));
                }
                else
                {
                    criteria.Add(Expression.Eq("InstitutionID", institutionID));
                }
                if (!string.IsNullOrEmpty(rolename) && !string.IsNullOrEmpty(rolename.Trim()))
                {
                    criteria.Add(Expression.Like("Name", rolename.Trim(), MatchMode.Anywhere));
                }
                if (scope.HasValue)
                {
                    criteria.Add(Expression.Eq("Scope", scope.Value));
                }
                if (status.HasValue)
                {
                    criteria.Add(Expression.Eq("Status", status.Value));
                }

                result = criteria.List<UserRole>() as List<UserRole>;
            }
            catch
            {
                throw;
            }
            return result;


        }

        public static UserRole GetSearchResults(bool IsDefault, UserCategory UserCategory)
        {
            UserRole userRole = new UserRole();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));

                criteria.Add(Expression.Eq("IsDefault", IsDefault));
                criteria.Add(Expression.Eq("UserCategory", UserCategory));
                criteria.Add(Expression.IsNull("InstitutionID"));
                userRole = criteria.UniqueResult<UserRole>();

            }
            catch
            {
                throw;
            }
            return userRole;
        }

        public static IList<UserRole> ListRole(long usercategory)
        {
            List<UserRole> result = new List<UserRole>();
            ISession session = PANE.Framework.DAO.CoreDAO<UserRole, long>.BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(UserRole));

                if (usercategory != 0)
                {
                    criteria.Add(Expression.Eq("UserCategory", (UserCategory)usercategory));
                }

                result = criteria.List<UserRole>().ToList();
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
