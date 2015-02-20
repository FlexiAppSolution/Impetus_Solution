using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PANE.Framework.DAO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.Functions.DTO;


namespace PANE.Framework.Data
{
    public class MailDAO : CoreDAO<Mail, long>
    {

        public  IList<Mail> RetrieveUnsentMails()
        {
            IList<Mail> result = new List<Mail>();

            ISession session = BuildSession();

            ICriteria criteria = session.CreateCriteria(typeof(Mail))
                .Add(Expression.Not (Expression.Eq("MailStatus", MailStatus.Sent)));
            
              
               

            result = criteria.List<Mail>();
           

            return result;
        }
       
      
        
        public IList<Mail> RetrieveUnsentMails(int BatchNo)
        {
            IList<Mail> result = new List<Mail>();

            ISession session = BuildSession();

            ICriteria criteria = session.CreateCriteria(typeof(Mail))
                .Add(Expression.Not(Expression.Eq("MailStatus", MailStatus.Sent)));

            criteria.SetMaxResults(BatchNo);

            result = criteria.List<Mail>();


            return result;
        }
    }
}
