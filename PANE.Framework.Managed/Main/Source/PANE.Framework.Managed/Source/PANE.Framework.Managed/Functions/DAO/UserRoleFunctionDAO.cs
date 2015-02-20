using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.DAO;
using PANE.Framework.Managed.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Managed.Functions.DAO
{
    public class UserRoleFunctionDAO : CoreDAO<UserRoleFunction, long>
    {
        public static IList<UserRoleFunction> RetrieveByUserRole(string mfbCode, UserRole role)
        {
            IList<UserRoleFunction> result = new List<UserRoleFunction>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction))
                    .Add(Expression.Eq("TheUserRoleID", role.ID))
                    .List<UserRoleFunction>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static IList<UserRoleFunction> RetrieveByFunctionItemIDAndRoleID(string mfbCode, long functionID, long roleID)
        {
            IList<UserRoleFunction> result = new List<UserRoleFunction>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction))
                    .Add(Expression.Eq("TheUserRoleID", roleID))
                    .Add(Expression.Eq("TheFunctionID", functionID))
                    .List<UserRoleFunction>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static IList<UserRoleFunction> RetrieveByFunctionID(string mfbCode, long functionID)
        {
            IList<UserRoleFunction> result = new List<UserRoleFunction>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction))
                    .Add(Expression.Eq("TheFunctionID", functionID))
                    .List<UserRoleFunction>();
            }
            catch
            {
                throw;
            }

            return result;
        }


    }
}
