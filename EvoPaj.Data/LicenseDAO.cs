using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using NHibernate.Criterion;
using NHibernate;

namespace EvoPaj.Data
{
    public class LicenseDAO : CoreDAO<License, long>
    {
        public IList<License> GetFile(License TheLicense)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(License));
            if (TheLicense.TheInstitution.ID != 0)
                criteria.Add(Expression.Eq("TheInstitution.ID", TheLicense.TheInstitution.ID));
            return criteria.List<License>() as List<License>;
        }

        public IList<License> GetFiles(long ID, int start, int limit, out int numOfResults)
        {
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(License));
            if (ID != 0)
                criteria.Add(Expression.Eq("TheInstitution.ID", ID));
            
            numOfResults = criteria.List().Count;
            criteria.SetMaxResults(limit).SetFirstResult(start);

            return criteria.List<License>() as List<License>;
        }
    }
}
