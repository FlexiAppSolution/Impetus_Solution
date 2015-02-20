using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Functions.DAO;
using PANE.Framework.Functions.DTO;
using System.Web.Security;

namespace PANE.Framework.Functions
{
    public class FunctionsEngine
    {
        public static List<UserRole> GetRoles()
        {
            return UserRoleDAO.RetrieveAll();
        }



        public static UserRoleFunction GetCurrentUserRoleFunction(string functionRoleName)
        {
            UserRoleFunction urf = new UserRoleFunction()
            {
                TheUserRole = (Membership.GetUser() as FunctionsMembershipUser).UserDetails.Role
            };
            urf.TheFunction = FunctionDAO.RetrieveByRoleName(functionRoleName, urf.TheUserRole.UserCategory);
            return urf;
        }
        public static List<UserRole> RetrieveAuthorizedSubUserRolesForFunction(UserRoleFunction theUserRoleFunction)
        {
            List<UserRole> result = new List<UserRole>();
            //List<UserRoleFunctionSubRole> subs = UserRoleFunctionSubRoleDAO.RetrieveByUserRoleAndFunction(theUserRoleFunction.TheUserRole, theUserRoleFunction.TheFunction) as List<UserRoleFunctionSubRole>;
            UserRoleFunction userRoleFunction = UserRoleFunctionDAO.RetrieveByUserRoleAndFunction(theUserRoleFunction.TheUserRole, theUserRoleFunction.TheFunction);

            if (userRoleFunction != null)
            {
                foreach (UserRoleFunctionSubRole usrSub in userRoleFunction.SubUserRoles)
                {
                    result.Add(usrSub.TheSubUserRole);
                }
            }
            return result;
        }

    }
}

