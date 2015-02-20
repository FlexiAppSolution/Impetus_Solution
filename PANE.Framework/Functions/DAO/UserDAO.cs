using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DAO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.DTO;

namespace PANE.Framework.Functions.DAO
{
    public class UserDAO : CoreDAO<IUser, long>
    {

        public static IUser RetrieveByUsername(string username)
        {
            IUser result = null;
            ISession session = BuildSession();
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

        public static IUser RetrieveBy(string username, string institutionCode)
        {
            IUser result = null;
            ISession session = BuildSession();
           // ITransaction tran = BuildTransaction(session);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(IUser)).Add(Expression.Eq("UserName", username));
                if (String.IsNullOrEmpty(institutionCode))
                {
                    criteria = criteria.Add(Expression.IsNull("InstitutionID"));
                }
                else
                {
                    IInstitution theInstitution = InstitutionDAO.RetrieveBy(institutionCode);
                    if (theInstitution != null)
                    {
                        criteria = criteria.Add(Expression.Eq("InstitutionID", theInstitution.ID));
                    }
                }
                result = criteria.UniqueResult<IUser>();
            }
            catch
            {
             
            }
            return result;
        }

        public static IUser AuthenticateUser(string username, string password, UserCategory theUserCategory, long? institutionID)
        {
            IUser result = null;
            ISession session = BuildSession();
            //ITransaction tran = BuildTransaction(session);
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(IUser))
                    .Add(Expression.Eq("Password", password))
                    .Add(Expression.Eq("UserName", username));
                
                result = criteria.UniqueResult<IUser>();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static IUser AuthenticateUser(string username, string password, long institutionID)
        {
            IUser result = null;
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(IUser)).Add(Expression.Eq("Password", password)).Add(Expression.Eq("UserName", username));
                if (institutionID == 0)
                {
                    criteria = criteria.Add(Expression.IsNull("InstitutionID"));
                }
                else
                {
                    criteria = criteria.Add(Expression.Eq("InstitutionID", institutionID));
                }
                result = criteria.UniqueResult<IUser>();
            }
            catch
            {
                throw;
            }
            return result;
        }
        public IList<IUser> GetByUserRole(UserRole role)
        {
           IList< IUser> result = null;
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(IUser)).Add(Expression.Eq("Role",role));
                
                
                result = criteria.List<IUser>();
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
