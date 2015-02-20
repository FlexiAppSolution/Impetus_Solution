using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.DAO;
using PANE.Framework.Managed.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Managed.Functions.DAO
{
    public class UserRoleFunctionSubRoleDAO : CoreDAO<UserRoleFunctionSubRole, long>
    {
        public static IList<UserRoleFunctionSubRole> RetrieveByUserRole(string mfbCode,long roleId)
        {
            IList<UserRoleFunctionSubRole> result = new List<UserRoleFunctionSubRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRole)).CreateCriteria("TheUserRoleFunction").Add(Expression.Eq("TheUserRoleID", roleId)).List<UserRoleFunctionSubRole>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static IList<UserRoleFunctionSubRole> RetrieveByUserRoleFunction(string mfbCode, long theUserRoleFunctionId)
        {
            return RetrieveByUserRoleFunctions(mfbCode, new long[] { theUserRoleFunctionId });
        }

        public static IList<UserRoleFunctionSubRole> RetrieveBySubUserRolesAndUserRoleFunctions(string mfbCode, long[] theSubUserRoles, long[] theUserRoleFunctionIds)
        {
            if (theSubUserRoles == null || theSubUserRoles.Length == 0)
            {
                return new List<UserRoleFunctionSubRole>();
            }
            if (theUserRoleFunctionIds == null || theUserRoleFunctionIds.Length == 0)
            {
                return new List<UserRoleFunctionSubRole>();
            }

            IList<UserRoleFunctionSubRole> result = new List<UserRoleFunctionSubRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRole)).Add(Expression.In("TheSubUserRoleID", theSubUserRoles))
                    .Add(Expression.In("TheUserRoleFunctionID", theUserRoleFunctionIds))
                    .List<UserRoleFunctionSubRole>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static IList<UserRoleFunctionSubRole> RetrieveByUserRoleFunctions(string mfbCode, long[] theUserRoleFunctionIds)
        {
            if (theUserRoleFunctionIds == null || theUserRoleFunctionIds.Length == 0)
            {
                return new List<UserRoleFunctionSubRole>();
            }

            IList<UserRoleFunctionSubRole> result = new List<UserRoleFunctionSubRole>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRole)).Add(Expression.In("TheUserRoleFunctionID", theUserRoleFunctionIds)).List<UserRoleFunctionSubRole>();
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
