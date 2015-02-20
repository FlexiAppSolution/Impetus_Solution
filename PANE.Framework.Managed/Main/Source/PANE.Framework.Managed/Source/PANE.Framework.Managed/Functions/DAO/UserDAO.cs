using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.DAO;
using PANE.Framework.Managed.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Managed.Functions.DAO
{
    public class UserDAO : CoreDAO<IUser, long>
    {

        public static IUser RetrieveByUsername(string mfbCode, string username)
        {
            IUser result = null;
            ISession session = BuildSession(mfbCode);

            try
            {
                result = session.CreateCriteria(typeof(IUser)).Add(Expression.Eq("UserName", username)).UniqueResult<IUser>();
            }
            catch
            {
                throw;
            }
            return result;
        }

       
        public static IUser AuthenticateUser(string mfbCode, string username, string password)
        {
            IUser result = null;
            ISession session = BuildSession(mfbCode);
            try
            {
                result = session.CreateCriteria(typeof(IUser)).Add(Expression.Eq("Password", password)).Add(Expression.Eq("UserName", username)).UniqueResult<IUser>();
            }
            catch
            {
                throw;
            }
            return result;
        }

    }



}
