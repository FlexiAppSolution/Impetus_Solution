using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using System.Linq;
using EvoPaj.Base;
using EvoPaj.Data;

namespace Evopaj.Data
{
    public class EmailDAO : CoreDAO<Email, long>
    {
        public static List<Email> GetPendingEmails()
        {            
            List<Email> result = new List<Email>();        
            try
            {               
                ISession session = BuildSession();
                ICriteria criteria = session.CreateCriteria(typeof(Email)).Add(Expression.Eq("Status", EvoPaj.Base.Utility.EmailStatus.Pending));                
                result = criteria.List<Email>() as List<Email>;
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogToFile(ex);
                throw;
            }

            return result;
        }
    }
}
