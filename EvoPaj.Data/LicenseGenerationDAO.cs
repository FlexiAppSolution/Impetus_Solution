using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base.Entities;
using NHibernate;
using NHibernate.Criterion;
using EvoPaj.Base.Utility;

namespace EvoPaj.Data
{
    public class LicenseGenerationDAO : CoreDAO<LicenseGeneration, long>
    {
        public IList<LicenseGeneration> SearchLicenseGeneration(DateTime? DateFrom, DateTime? DateTo, Base.Utility.LicenseType? license, long instionitutionID, string ApprovalStatus)
        {
            IList<LicenseGeneration> result = new List<LicenseGeneration>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(LicenseGeneration));

            if (DateFrom.HasValue)
            {
                criteria.Add(Expression.Gt("DateRequested", DateFrom.Value));
            }
            if (DateTo.HasValue)
            {
                criteria.Add(Expression.Lt("DateRequested", DateTo.Value.AddDays(1).AddSeconds(-1)));
            }
            if (instionitutionID != 0)
            {
                criteria.Add(Expression.Eq("TheInstitution.ID", instionitutionID));
            }
            if (license.Value != 0)
            {
                criteria.Add(Expression.Eq("TheLicenseType", license));
            }
            if (!string.IsNullOrEmpty(ApprovalStatus))
            {
                LicenseGenerationApproval appStatus = new LicenseGenerationApproval();
                if (ApprovalStatus.Equals("Pending"))
                    appStatus = LicenseGenerationApproval.Pending;
                else if (ApprovalStatus.Equals("Approved"))
                    appStatus = LicenseGenerationApproval.Approved;
                else if (ApprovalStatus.Equals("Declined"))
                    appStatus = LicenseGenerationApproval.Declined;

                criteria.Add(Expression.Eq("Approval", appStatus));
            }
            result = criteria.List<LicenseGeneration>();
            return result;
        }

        public IList<LicenseGeneration> LicenseRequestList(Base.Utility.LicenseType? license, long instionitutionID, DateTime dateFrom, DateTime dateTo, int start, int limit, out int numOfResults)
        {
            IList<LicenseGeneration> result = new List<LicenseGeneration>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(LicenseGeneration));

            if (dateFrom != DateTime.Parse("1/1/0001"))
                criteria.Add(Expression.Ge("DateRequested", dateFrom));  
                         
            if (dateTo != DateTime.Parse("1/1/0001"))
                criteria.Add(Expression.Le("DateRequested", dateTo));            

            if (instionitutionID != 0)
            {
                criteria.Add(Expression.Eq("TheInstitution.ID", instionitutionID));
            }

            if (license.Value != 0)
            {
                criteria.Add(Expression.Eq("TheLicenseType", license));
            }

            numOfResults = criteria.List().Count;
            criteria.SetMaxResults(limit).SetFirstResult(start);

            result = criteria.List<LicenseGeneration>();
            return result;
        }

        public IList<LicenseGeneration> GetByUser(long p, LicenseType? licenseType)
        {
            IList<LicenseGeneration> result = new List<LicenseGeneration>();
            ISession session = BuildSession();
            ICriteria criteria = session.CreateCriteria(typeof(LicenseGeneration));

            if (p != 0)
            {
                criteria.Add(Expression.Eq("RequestingUser.ID", p));
            }
            if (licenseType.HasValue)
            {
                criteria.Add(Expression.Eq("TheLicenseType", licenseType));
            }
            result = criteria.List<LicenseGeneration>();
            return result;
        }
    }
}
