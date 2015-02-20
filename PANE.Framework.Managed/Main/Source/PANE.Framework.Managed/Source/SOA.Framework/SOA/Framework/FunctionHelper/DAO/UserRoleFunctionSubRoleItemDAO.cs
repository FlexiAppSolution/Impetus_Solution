using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DAO;
using NHibernate.Criterion;
using NHibernate;

namespace SOA.Framework.FunctionHelper.DAO
{
    public class UserRoleFunctionSubRoleItemDAO: CoreDAO<UserRoleFunctionSubRoleItem, long>
    {

        public static IList<UserRoleFunctionSubRoleItem> RetrieveByUserRole(string mfbCode, long roleID)
        {
            IList<UserRoleFunctionSubRoleItem> result = new List<UserRoleFunctionSubRoleItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionSubRoleItem))
                    .CreateCriteria("TheUserRoleFunctionItem")
                    .Add(Expression.Eq("TheUserRoleID", roleID)).List<UserRoleFunctionSubRoleItem>();
            }
            catch
            {
                throw;
            }

            return result;
        } 
    }
}
