using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DAO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Functions.DAO
{
    public class UserRoleFunctionDAO : CoreDAO<UserRoleFunction, long>
    {
        public static IList<UserRoleFunction> RetrieveByUserRole(UserRole role)
        {
            IList<UserRoleFunction> result = new List<UserRoleFunction>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction))
                    .Add(Expression.Eq("TheUserRole.ID", role.ID))
                    .SetFetchMode("TheFunction", FetchMode.Eager)
                    .List<UserRoleFunction>();
            }
            catch
            {
                throw;
            }

            return result;
        }
        public static void DeleteUserRoleFunction(UserRoleFunction theUserRoleFunction)
        {
            ISession session = BuildSession();
            ITransaction tran = session.BeginTransaction();
            StringBuilder delete = new StringBuilder();
            delete.Append(string.Format("delete from UserRoleFunctions where ID={0} ;", theUserRoleFunction.ID));
            try
            {
                session.CreateSQLQuery(delete.ToString()).AddEntity(typeof(UserRoleFunction)).List<UserRoleFunction>();
                session.Flush();
            }
            catch
            {
                tran.Rollback();
            }
        }

        public static UserRoleFunction RetrieveByUserRoleAndFunction(UserRole role, Function func)
        {
            UserRoleFunction result = null;
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction)).Add(Expression.Eq("TheUserRole.ID", role.ID)).Add(Expression.Eq("TheFunction.ID", func.ID)).UniqueResult<UserRoleFunction>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public IList<UserRoleFunction> GetByUserRole(UserRole role)
        {
            IList<UserRoleFunction> result = new List<UserRoleFunction>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunction))
                    .Add(Expression.Eq("TheUserRole.ID", role.ID))
                    .SetFetchMode("TheFunction", FetchMode.Eager)
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
