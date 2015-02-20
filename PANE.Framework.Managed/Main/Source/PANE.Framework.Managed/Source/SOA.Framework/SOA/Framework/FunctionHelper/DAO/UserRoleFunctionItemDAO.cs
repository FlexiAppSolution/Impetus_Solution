using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DAO;
using NHibernate.Criterion;
using NHibernate;
using PANE.Framework.Managed.Functions.DTO;

namespace SOA.Framework.FunctionHelper.DAO
{
    public class UserRoleFunctionItemDAO: CoreDAO<UserRoleFunctionItem , long>
    {
        public static List<UserRoleFunctionItem> RetrieveByUserRoleID(string mfbCode, long roleID)
        {
            List<UserRoleFunctionItem> result = new List<UserRoleFunctionItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionItem))
                    .Add(Expression.Eq("TheUserRoleID", roleID))
                    .List<UserRoleFunctionItem>() as List<UserRoleFunctionItem>;
                //result = UserRoleFunctionItemDAO.RetrieveAll(mfbCode);   
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static List<UserRoleFunctionItem> RetrieveByFunctionItemIDAndRoleID(string mfbCode, long functionItemID, long roleID)
        {
            List<UserRoleFunctionItem> result = new List<UserRoleFunctionItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionItem))
                    .Add(Expression.Eq("TheFunctionItemID", functionItemID))
                    .Add(Expression.Eq("TheUserRoleID", roleID))
                    .List<UserRoleFunctionItem>() as List<UserRoleFunctionItem>;
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static List<UserRoleFunctionItem> RetrieveByFunctionItemID(string mfbCode, long functionItemID)
        {
            List<UserRoleFunctionItem> result = new List<UserRoleFunctionItem>();
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(UserRoleFunctionItem))
                    .Add(Expression.Eq("TheFunctionItemID", functionItemID))
                    //.Add(Expression.Eq("TheUserRoleID", roleID))
                    .List<UserRoleFunctionItem>() as List<UserRoleFunctionItem>;
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
