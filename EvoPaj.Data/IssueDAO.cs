using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using NHibernate;
using NHibernate.Criterion;
using EvoPaj.Base.Utility;

namespace EvoPaj.Data
{
    public class IssueDAO : CoreDAO<Issue, long>
    {

        public IList<Issue> GetCaseByErrorCode(string errorCode)
        {
            IList<Issue> result = new List<Issue>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Issue));
            criteria.Add(Expression.Eq("ErrorCode", errorCode));
            result = criteria.List<Issue>();
            return result;
        }      

        public IList<Issue> SearchCase(long instionitutionID, string Name, DateTime? DateFrom, DateTime? DateTo, Issue TheCase)
        {

            IList<Issue> result = new List<Issue>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Issue));

            if (instionitutionID != 0)
            {
                criteria.Add(Expression.Eq("LoggingInstition.ID", instionitutionID));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                criteria.Add(Expression.Like("Name", Name, MatchMode.Anywhere));
            }
            if (DateFrom.HasValue && DateFrom.Value != DateTime.MinValue)
            {
                criteria.Add(Expression.Gt("DateLogged", DateFrom.Value));
            }
            if (DateTo.HasValue && DateTo.Value != DateTime.MaxValue)
            {
                criteria.Add(Expression.Lt("DateLogged", DateTo.Value.AddDays(1).AddSeconds(-1)));
            }
            if (!(TheCase.TheCaseType == EvoPaj.Base.Utility.CaseType.All))
            {
                criteria.Add(Expression.Eq("TheCaseType", TheCase.TheCaseType));
            }

            result = criteria.List<Issue>();
            return result;
        }

        public IList<Issue> SearchCaseSub(long instionitutionID, string Name, DateTime DateFrom, DateTime DateTo, CaseType TheCaseType, int start, int limit, out int numOfResult)
        {

            IList<Issue> result = new List<Issue>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Issue));

            if (instionitutionID != 0)
            {
                criteria.Add(Expression.Eq("LoggingInstition.ID", instionitutionID));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                criteria.Add(Expression.Like("Name", Name, MatchMode.Anywhere));
            }
            if ((DateTime.MinValue < DateFrom) && (DateTime.MaxValue > DateTo))
            {
                criteria.Add(Expression.Between("DateLogged", DateFrom, DateTo.AddDays(1).AddSeconds(-1)));
            }

            criteria.Add(Expression.Eq("TheCaseType", TheCaseType));


            numOfResult = criteria.List().Count;
            criteria.SetMaxResults(limit).SetFirstResult(start);

            result = criteria.List<Issue>();
            return result;
        }
    }
}
