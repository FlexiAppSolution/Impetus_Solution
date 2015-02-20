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
    public class CaseDAO : CoreDAO<Case, long>
    {

        //public IList<Case> GetCaseByErrorCode(string errorCode)
        //{
        //    ISession session = BuildSession();
        //    ICriteria criteria = session.CreateCriteria(typeof(Case));
        //    criteria.Add(Expression.Eq("ErrorCode", errorCode));
        //    criteria.List<Case>() as List();


        //}      

        public IList<Case> SearchCase(long instionitutionID, string Name, DateTime? DateFrom, DateTime? DateTo, Case TheCase)
        {

            IList<Case> result = new List<Case>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Case ));
            
            if (instionitutionID != 0)
            {
                criteria.Add(Expression.Eq("LoggingInstition.ID", instionitutionID));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                criteria.Add(Expression.Like("Name", Name, MatchMode.Anywhere));
            }
            if (DateFrom.HasValue)
            {
                criteria.Add(Expression.Gt("DateLogged", DateFrom.Value));
            }
            if(DateTo.HasValue)
            {
                criteria.Add(Expression.Lt("DateLogged",DateTo.Value.AddDays(1).AddSeconds(-1)));
            }
            if (!(TheCase.TheCaseType == EvoPaj.Base.Utility.CaseType.All))
            {
                criteria.Add(Expression.Eq("TheCaseType", TheCase.TheCaseType));
            }
            
            result = criteria.List<Case>();
            return result;
        }

        public IList<Case> SearchCaseSub(long instionitutionID, string Name, DateTime DateFrom, DateTime DateTo, CaseType TheCaseType, int start, int limit, out int numOfResult)
        {

            IList<Case> result = new List<Case>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Case));
           
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

            result = criteria.List<Case>();
            return result;
        }
    }
}
