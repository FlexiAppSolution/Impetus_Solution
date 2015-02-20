using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.Utility;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.DAO
{
    public class InstitutionDAO: CoreDAO<IInstitution, long>
    {
        public static IInstitution RetrieveBy(string institutionCode)
        {
            IInstitution result = null;
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria<IInstitution>()
                    .Add(Expression.Eq("InstitutionCode", institutionCode))
                    .UniqueResult<IInstitution>();
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Gets an IIinstitution based on institutionID and UserCategory. To get default institution use null for institutionID
        /// </summary>
        /// <param name="institutionID">The current user InstitutionID, use null for bank users</param>
        /// <param name="theUserCategory">The current user UserCategory</param>
        /// <returns>IInstitution</returns>
        public static IInstitution RetrieveBy(long? institutionID, UserCategory theUserCategory)
        {
            IInstitution result = null;
            ISession session = BuildSession();
            try
            {
                ICriteria criteria= session.CreateCriteria(typeof(IInstitution));
                //If institutionID is null then get the default institution
                if(institutionID==null)
                {
                   result= criteria.Add(Expression.Eq("IsDefault", true)).UniqueResult<IInstitution>();
                }
                else
                {
                    result = (criteria.Add(Expression.Eq("ID", institutionID)).List<IInstitution>() 
                        as List<IInstitution>).SingleOrDefault(inst => inst.TheUserCategory == theUserCategory);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
