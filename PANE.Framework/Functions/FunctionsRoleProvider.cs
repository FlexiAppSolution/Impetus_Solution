using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using PANE.Framework.DTO;
using PANE.Framework.Approval.DTO;
using PANE.Framework.Functions.DAO;
using PANE.Framework.Functions.DTO;
using System.Collections.Generic;
using PANE.Framework.Approval.DAO;

namespace PANE.Framework.Functions
{
    /// <summary>
    /// Summary description for FunctionsRoleProvider
    /// </summary>
    public class FunctionsRoleProvider : RoleProvider
    {
        public FunctionsRoleProvider()
        {

        }

        public bool DemoMode { get; set; }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config["demoMode"] != null)
            {
                DemoMode = Convert.ToBoolean(config["demoMode"]);
            }
            base.Initialize(name, config);
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string ApplicationName
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string[] GetAllRoles()
        {
            List<string> allRoles = new List<string>();
            foreach (Function theFunction in FunctionDAO.RetrieveAll())
            {
                if (theFunction.Status != Status.InActive)
                {
                    allRoles.Add(theFunction.RoleName);
                }
            }

            // kindly add the default functions also
            foreach (Function theFunction in FunctionDAO.RetrieveDefaultFunctions())
            {
                allRoles.Add(theFunction.RoleName);
            }
            return allRoles.ToArray();
        }

        private string[] ContextCurrentRoles
        {
            get
            {
                return (string[])HttpContext.Current.Items["::USER_ROLES::"];
            }
            set
            {
                HttpContext.Current.Items["::USER_ROLES::"] = value;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            if (DemoMode)
            {
                return GetAllRoles();
            }
            if (HttpContext.Current.User.Identity.Name == username)
            {
                if (ContextCurrentRoles != null)
                {
                    return ContextCurrentRoles;
                }
            }


            List<string> userRoles = new List<string>();

            FunctionsMembershipUser theUser = Membership.GetUser(username) as FunctionsMembershipUser;
            if (theUser != null)
            {
                foreach (UserRoleFunction theUserRoleFunction in UserRoleFunctionDAO.RetrieveByUserRole(theUser.UserDetails.Role))
                {
                    if (theUserRoleFunction.TheFunction.Status != Status.InActive)
                    {
                        userRoles.Add(theUserRoleFunction.TheFunction.RoleName);
                    }
                }
                //TODO: ADD APPROVAL ROLES
                int count = ApprovalConfigurationDAO.CountByApprovableUserRole(theUser.UserDetails.Role);
                // List<ApprovalConfiguration> list = ApprovalConfigurationDAO.RetrieveByApprovableUserRole(theUser.UserDetails.Role);
                if (count > 0 || theUser.UserDetails.ApprovalRight)
                {
                    userRoles.Add("Approval");
                }
            }




            // kindly add the default functions also
            foreach (Function theFunction in FunctionDAO.RetrieveDefaultFunctions())
            {
                if (theFunction.Status != Status.InActive)
                {
                    userRoles.Add(theFunction.RoleName);
                }
            }

            if (HttpContext.Current.User.Identity.Name == username)
            {
                ContextCurrentRoles = userRoles.ToArray();
            }
            return userRoles.ToArray();

        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RoleExists(string roleName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsFunctionEnabled(string roleName)
        {
            bool toReturn = true;// return true if the function does not exist
            Function func = FunctionDAO.RetrieveByRoleName(roleName);
            if (func != null && func.Status == Status.InActive)
            {
                return false;// if its inactive return false
            }
            return toReturn;
        }
       
    }

}