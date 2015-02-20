using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PANE.Framework.DAO;
using PANE.Framework.Approval.DTO;
using PANE.Framework.Functions.DTO;
using NHibernate;
using NHibernate.Criterion;
using PANE.Framework.Functions.DAO;

namespace PANE.Framework.Approval.DAO
{
    public class ApprovalConfigurationDAO : CoreDAO<ApprovalConfiguration, long>
    {
        public static List<ApprovalConfiguration> RetrieveByApprovableUserRole(UserRole userRole)
        {
            IList<ApprovalConfiguration> result = new List<ApprovalConfiguration>();
            ISession session = BuildSession();
            try
            {
                result = session.CreateCriteria(typeof(ApprovalConfiguration)).Add(Expression.Eq("IsApprovable", true)).Add(Expression.Eq("ApprovingRole.ID", userRole.ID)).List<ApprovalConfiguration>();
            }
            catch
            {
                throw;
            }

            return result as List<ApprovalConfiguration>;
        }
        public static int CountByApprovableUserRole(UserRole userRole)
        {
            int result = 0;
            ISession session = BuildSession();
            try
            {
                result = (int)session.CreateCriteria(typeof(ApprovalConfiguration))
                    .Add(Expression.Eq("IsApprovable", true))
                    .Add(Expression.Eq("ApprovingRole.ID", userRole.ID))
                    .SetProjection(Projections.Count(Projections.Id()))
                    .UniqueResult();
            }
            catch
            {
                throw;
            }

            return result;
        }
        public static List<Function> RetrieveFunctionsForApprovalConfiguration(UserCategory theUserCategory, long? institutionID)
        {
            List<Function> functions = new List<Function>();
            ISession session = BuildSession();
            try
            {
                IList<Function> allFunctions = session.CreateCriteria(typeof(Function)).Add(Expression.Eq("UserCategory", theUserCategory)).Add(Expression.Eq("IsApprovable", true)).List<Function>();
                List<Function> repFunctions = allFunctions.Where(theFunc => theFunc.HasSubRoles).ToList();
                functions = allFunctions.Where(theFunc => !theFunc.HasSubRoles).ToList();
                foreach (UserRole subRoles in UserRoleDAO.SearchUserRole(string.Empty, null, null, theUserCategory, institutionID))
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
                            SubUserRoleUpdate = subRoles
                        };
                        fc.TheApprovalConfigurations = fun.TheApprovalConfigurations.Where(theApprov => theApprov.SubUserRole == subRoles).ToList();
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

        public static ApprovalConfiguration RetrieveByMakerRoleName(string roleName, long? subUserRoleID, UserCategory theUserCategory, long? institutionID)
        {
            ApprovalConfiguration apprConfig = new ApprovalConfiguration();
            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(ApprovalConfiguration));
                if (institutionID != null)
                {
                    criteria.Add(Expression.Eq("InstitutionID", institutionID));
                }
                else
                {
                    criteria.Add(Expression.IsNull("InstitutionID"));
                }
                if (subUserRoleID != null)
                {
                    criteria.Add(Expression.Eq("SubUserRole.ID", subUserRoleID));
                }
                else
                {
                    criteria.Add(Expression.IsNull("SubUserRole"));
                }
                criteria.CreateCriteria("MakerRole").Add(Expression.Eq("RoleName", roleName)).Add(Expression.Eq("UserCategory", theUserCategory));
                try
                {
                    apprConfig = criteria.UniqueResult<ApprovalConfiguration>();
                }
                catch
                {
                    try
                    {
                        session.Clear();
                        apprConfig = criteria.UniqueResult<ApprovalConfiguration>();
                    }
                    catch
                    {
                        throw;
                    }
                }

            }
            catch
            {
                throw;
            }
            return apprConfig;
        }

        public static ApprovalConfiguration RetrieveByFunction(Function theFunction, long? subUserRoleID, long? institutionID)
        {
            ApprovalConfiguration result = null;

            ISession session = BuildSession();
            try
            {
                ICriteria criteria = session.CreateCriteria(typeof(ApprovalConfiguration)).Add(Expression.Eq("MakerRole.ID", theFunction.ID));
                if (institutionID != null)
                {
                    criteria.Add(Expression.Eq("InstitutionID", institutionID));
                }
                else
                {
                    criteria.Add(Expression.IsNull("InstitutionID"));
                }
                if (subUserRoleID.HasValue)
                {
                    criteria.Add(Expression.Eq("SubUserRole.ID", subUserRoleID));
                }
                else
                {
                    criteria.Add(Expression.IsNull("SubUserRole"));
                }
                result = criteria.UniqueResult<ApprovalConfiguration>();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public static List<Function> RetrieveFunctionsForApproval(UserRole theUserRole, UserCategory theUserCategory, long? institutionID)
        {
            List<Function> functions = new List<Function>();
            ISession session = BuildSession();
            try
            {
                IList<Function> allFunctions = session.CreateCriteria(typeof(Function)).Add(Expression.Eq("UserCategory", theUserCategory)).Add(Expression.Eq("IsApprovable", true)).List<Function>();
                List<Function> repFunctions = allFunctions.Where(theFunc => theFunc.HasSubRoles && theFunc.TheApprovalConfigurations.Any(ac => ac.ApprovingRole == theUserRole && ac.IsApprovable)).ToList();
                functions = allFunctions.Where(theFunc => !theFunc.HasSubRoles && theFunc.TheApprovalConfigurations.Any(ac => ac.ApprovingRole == theUserRole && ac.IsApprovable)).ToList();

                List<Function> makerAllFunctions = UserRoleFunctionDAO.RetrieveByUserRole(theUserRole).Select(urf => urf.TheFunction).ToList();
                repFunctions.AddRange(makerAllFunctions.Where(theFunc => theFunc.HasSubRoles && theFunc.TheApprovalConfigurations.Any(ac => ac.ApprovingRole != theUserRole && ac.IsApprovable)));
                functions.AddRange(makerAllFunctions.Where(theFunc => !theFunc.HasSubRoles && theFunc.TheApprovalConfigurations.Any(ac => ac.ApprovingRole != theUserRole && ac.IsApprovable)));

                
                foreach (UserRole subRoles in UserRoleDAO.SearchUserRole(string.Empty, 0, 0, theUserCategory, institutionID))
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
                            SubUserRoleUpdate = subRoles
                        };
                        fc.TheApprovalConfigurations = fun.TheApprovalConfigurations.Where(theApprov => theApprov.SubUserRole == subRoles && theApprov.ApprovingRole == theUserRole && theApprov.IsApprovable).ToList();
                        if (fc.TheApprovalConfigurations != null && fc.TheApprovalConfigurations.Count > 0)
                        {
                            functions.Add(fc);
                        }
                    }

                }
            }
            catch
            {
                throw;
            }

            return functions;
        }
    }
}
