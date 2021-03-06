﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;
using NHibernate;
using NHibernate.Criterion;

namespace PANE.Framework.Managed.DAO
{
    public class IInstitutionDAO: CoreDAO<IInstitution , long>
    {
        public static IInstitution GetByCode(string code)
        {
            IInstitution result = null;
            ISession session = BuildSession("");
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(IInstitution));
                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(code.Trim()))
                {
                    criteria.Add(Expression.Eq("Code", code));
                }

                result = criteria.UniqueResult<IInstitution>();
            }
            catch { throw; }

            return result;


        }
    }
}
