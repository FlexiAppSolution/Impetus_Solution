using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DAO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Functions.DAO
{
    public class UserRoleFunctionSubRoleDAO : CoreDAO<UserRoleFunctionSubRole, long>
    {
        public static IList<UserRoleFunctionSubRole> RetrieveByUserRole(UserRole role)
        {
            IList<UserRoleFunctionSubRole> result = new List<UserRoleFunctionSubRole>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRole)).CreateCriteria("TheUserRoleFunction").Add(Expression.Eq("TheUserRole.ID", role.ID)).List<UserRoleFunctionSubRole>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static IList<UserRoleFunctionSubRole> RetrieveByUserRoleAndFunction(UserRole role, Function func)
        {
            IList<UserRoleFunctionSubRole> result = new List<UserRoleFunctionSubRole>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRole)).CreateCriteria("TheUserRoleFunction").Add(Expression.Eq("TheUserRole.ID", role.ID)).Add(Expression.Eq("TheFunction.ID", func.ID)).List<UserRoleFunctionSubRole>();
            }
            catch
            {
                throw;
            }

            return result;
        }
        public static void DeleteByUserRoleFunctionSubRole(UserRoleFunctionSubRole subRole)
        {
            ISession session = BuildSession();
            ITransaction tran = session.BeginTransaction();
            StringBuilder delete = new StringBuilder();
            delete.Append(string.Format("delete from UserRoleFunctionSubRoles where ID={0} ;", subRole.ID));
            try
            {
                session.CreateSQLQuery(delete.ToString()).AddEntity(typeof(UserRoleFunctionSubRole)).List<UserRoleFunctionSubRole>();
                session.Flush();
            }
            catch
            {
                tran.Rollback();
            }
        }
    }
}
