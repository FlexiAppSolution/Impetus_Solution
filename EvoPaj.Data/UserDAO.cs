using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using NHibernate;
using NHibernate.Criterion;

namespace EvoPaj.Data
{
    public class UserDAO : CoreDAO<User, long>
    {

        public User GetUserByUserName(string userName)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(User));
            criteria.Add(Expression.Eq("UserName", userName));
            return criteria.UniqueResult<User>();
        }

        public User GetUserByIDAndUserName(long InstitutionID, string userName, string Password)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(User));
            criteria.Add(Expression.Eq("UserName", userName));
            criteria.Add(Expression.Eq("Password", Password));
            criteria.Add(Expression.Eq("TheInstitution.ID", InstitutionID));
            return criteria.UniqueResult<User>();
        }

        public User GetUser(string userName, string Password)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(User));
            criteria.Add(Expression.Eq("UserName", userName));
            criteria.Add(Expression.Eq("Password", Password));           
            return criteria.UniqueResult<User>();
        }

        public IList<User> GetUser(User TheUser)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(User));
            if (TheUser.TheInstitution.TheInstitutionID != 0)
                criteria.Add(Expression.Eq("TheInstitution.ID", TheUser.TheInstitution.TheInstitutionID));
            if(TheUser.LastName.Trim()!="")
                criteria.Add(
                    Expression.Or
                    (
                    Expression.Like("LastName", TheUser.LastName,MatchMode.Anywhere),
                    Expression.Like("FirstName", TheUser.LastName,MatchMode.Anywhere)
                    ));
            return criteria.List<User>();
        }

        public IList<User> GetUser(string LastName, long ID, int start, int limit, out int numOfResults)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(User));
            if (ID != 0)
                criteria.Add(Expression.Eq("TheInstitution.ID", ID));
            if (LastName.Trim() != "")
                criteria.Add(
                    Expression.Or
                    (
                    Expression.Like("LastName", LastName, MatchMode.Anywhere),
                    Expression.Like("FirstName", LastName, MatchMode.Anywhere)
                    ));

            numOfResults = criteria.List().Count;
            criteria.SetMaxResults(limit).SetFirstResult(start);

            return criteria.List<User>();
        }
    }
}
