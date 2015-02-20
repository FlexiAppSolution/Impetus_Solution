using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.Managed.DAO;
using SOA.Framework.FunctionHelper.DAO;
using SOA.Framework.FunctionHelper;
using System.Linq;
using SOA.Framework.FS;
using PANE.Framework.Managed.NHibernateManager.Configuration;

namespace SOA.Framework.ApprovalConfigHelper.DAO
{
    public class  ApprovalConfigDAO :CoreDAO<ApprovalConfig,long>
    {


        public static IList<ApprovalConfig> RetrieveByMakerRole(string mfbCode, long? makerRoleID, string makeRoleName)
        {
            IList<ApprovalConfig> apprConfig = new List<ApprovalConfig>();
            ISession session = BuildSession(mfbCode, DatabaseSource.Core);

            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(ApprovalConfig));

                criteria.Add(Expression.Eq("MakerRole", makerRoleID));

                if (!String.IsNullOrEmpty(makeRoleName) && !String.IsNullOrEmpty(makeRoleName.Trim()))
                {

                    //criteria.Add(Expression.Eq("MakeRoleName", makeRoleName.Trim()));

                }
                apprConfig = criteria.List<ApprovalConfig>();
            }
            catch
            {

                return null;


            }
            return apprConfig;


        }

        public static IList<ApprovalConfig> RetrieveByMakerRoleID(string mfbCode, long? makerRoleID)
        {
            IList<ApprovalConfig> apprConfig = new List<ApprovalConfig>();
            ISession session = BuildSession(mfbCode, DatabaseSource.Core);

            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(ApprovalConfig));

                criteria.Add(Expression.Eq("MakerRole", makerRoleID));


                apprConfig = criteria.List<ApprovalConfig>();
            }
            catch
            {

                return null;


            }
            return apprConfig;


        }

   

       


       
    }
}
