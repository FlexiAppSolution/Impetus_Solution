using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using NHibernate.Criterion;
using NHibernate;

namespace EvoPaj.Data
{
    public class InstitutionDAO : CoreDAO<Institution, long>
    {
       
        public Institution getLastInstitution()
        {
            Institution institution = null;
            ISession session = BuildSession();
            institution = session.CreateCriteria(typeof(Institution))
                .SetMaxResults(1)
                .AddOrder(Order.Desc("ID")).UniqueResult<Institution>();
            return institution;
        }

        public Institution getInstitutionByCode(Int32 code)
        {            
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Institution));            
            criteria.Add(Expression.Eq("Code", code));
            return criteria.UniqueResult<Institution>();            
        } 

        public IList<Institution> GetInstitution(Institution TheInstitution)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Institution));
            if (TheInstitution.InstitutionName.Trim() != "")
                criteria.Add(Expression.Like("InstitutionName", TheInstitution.InstitutionName, MatchMode.Anywhere));
            return criteria.List<Institution>();
        }

        public IList<Institution> GetInstitution(Institution TheInstitution, int start, int limit, out int numOfResults)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Institution));
            if (TheInstitution.InstitutionName.Trim() != "")
                criteria.Add(Expression.Like("InstitutionName", TheInstitution.InstitutionName, MatchMode.Anywhere));

            numOfResults = criteria.List().Count;
            criteria.SetMaxResults(limit).SetFirstResult(start);
            
            return criteria.List<Institution>();
        }

        public IList<Institution> GetInstitutionByCode(Institution TheInstitution)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Institution))
                .Add(Expression.Eq("Code", TheInstitution.Code));
            return criteria.List<Institution>();
        }
        public IList<Institution> GetInstitutionByID(Institution TheInstitution)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(Institution))
                .Add(Expression.Eq("ID", TheInstitution.ID));
            return criteria.List<Institution>();
        }
    }
}
