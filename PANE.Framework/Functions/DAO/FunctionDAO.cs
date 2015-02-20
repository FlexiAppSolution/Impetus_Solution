using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.DAO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using PANE.Framework.Utility;
using NHibernate.Criterion;
using System.Linq;

namespace PANE.Framework.Functions.DAO
{
    public class FunctionDAO : CoreDAO<Function, long>
    {
        public static Function RetrieveByRoleName(string roleName, UserCategory theUserCategory)
        {
            Function result = new Function();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(Function)).Add(Expression.Eq("RoleName", roleName)).Add(Expression.Eq("UserCategory", theUserCategory)).UniqueResult<Function>();
            }
            catch
            {
                throw;
            }

            return result;
        }
        public static Function RetrieveByRoleName(string roleName)
        {
            Function result = new Function();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(Function)).Add(Expression.Eq("RoleName", roleName)).UniqueResult<Function>();
            }
            catch
            {
                throw;
            }

            return result;
        }
        public static List<Function> RetrieveDefaultFunctions(UserCategory theUserCategory)
        {
            List<Function> result = new List<Function>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(Function)).Add(Expression.Eq("IsDefault", true)).Add(Expression.Or(Expression.Eq("UserCategory", theUserCategory), Expression.IsNull("UserCategory"))).List<Function>() as List<Function>;
            }
            catch
            {
                throw;
            }

            return result;
        
        }

        /// <summary>
        /// Retrieves the default functions for optional modular fuctions.
        /// </summary>
        /// <returns></returns>
        public static List<Function> RetrieveDefaultFunctions()
        {
            List<Function> result = new List<Function>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(Function))
                    .Add(Expression.Eq("IsDefault", false))
                    .Add(Expression.IsNull("UserCategory"))
                    .Add(Expression.Not(Expression.Eq("Status", PANE.Framework.Functions.DTO.Status.InActive)))
                    .List<Function>() as List<Function>;
            }
            catch
            {
                throw;
            }

            return result;

        }

        public static List<Function> RetrieveParentFunctions(UserCategory theUserCategory)
        {
            List<Function> functions = new List<Function>();
            ISession session = BuildSession();
            try
            {
                IList<Function> allFunctions = session.CreateCriteria(typeof(Function))
                    .Add(Expression.Eq("UserCategory", theUserCategory))
                    .Add(Expression.IsNull("ParentFunction"))
                    .List<Function>();

                List<Function> repFunctions = allFunctions
                    .Where(theFunc => theFunc.HasSubRoles).ToList();
                functions = allFunctions
                    .Where(theFunc => !theFunc.HasSubRoles).ToList();
                foreach (UserRole subRoles in UserRoleDAO.SearchUserRole(null, null, null, theUserCategory, null))
                {
                    foreach (Function fun in repFunctions)
                    {
                        Function fc = new Function()
                        {
                            ID = fun.ID,
                            RoleName = fun.RoleName,
                            Description = fun.Description,
                            Name = string.Format("{0} [{1}]", fun.Name, subRoles.Name),
                            HasSubRoles = fun.HasSubRoles,
                            IsDefault = fun.IsDefault,
                            SubUserRoleUpdate = subRoles,
                            Type = fun.Type,
                            
                        };
                        functions.Add(fc);
                    }

                }
            }
            catch
            {
                throw;
            }

            return functions;
        }
        public static IList<Function> RetrieveByUserCategory(UserCategory theUserCategory)
        {

            ISession session = BuildSession();
            return session.CreateCriteria(typeof(Function))
                .Add(Expression.Eq("UserCategory", theUserCategory))
                .AddOrder(Order.Asc("ParentFunction"))
                .SetFetchMode("ParentFunction", FetchMode.Eager)
                .List<Function>();
        }        
    }
   
}