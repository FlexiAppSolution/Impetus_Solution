namespace SOA.Framework.FunctionHelper
{
    using SOA.Framework.FS;
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Security;
    using PANE.Framework.Managed.Functions.DTO;
    using PANE.Framework.Managed.Functions;
    using PANE.Framework.Managed.DTO;
    using PANE.Framework.Managed.Approval.DTO;
    using PANE.Framework.Managed.Functions.DAO;
    using System.Collections.Generic;
    using PANE.Framework.Managed.Approval.DAO;
    using System.Linq;
    using SOA.Framework.FunctionHelper.DAO;


   public class ManagedFunctionRoleProvider:FunctionsRoleProvider
    {
       public const string MFBCode = "::SS_MFBCODE::";

        private string _endpointName = null;
        private string _providerName = null;
        private string applicationName;
        private string assemblyName;
        public string EndPoint
        {
            get { return this._endpointName; }
        }

        public string AssemblyName
        {
            get { return this.assemblyName; }
        }

       public ManagedFunctionRoleProvider()
        {

        }

       public override void AddUsersToRoles(string[] usernames, string[] roleNames)
       {
           throw new Exception("The method or operation is not implemented.");
       }

       public override string ApplicationName
       {
           get
           {
               return applicationName;
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
           PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser memUser = Membership.GetUser() as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
           if (memUser == null || memUser.UserDetails == null || memUser.UserDetails.Role == null)
           {
               return new string[0];
           }
           return SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.RetrieveAll("", PANE.Framework.Managed.NHibernateManager.Configuration.DatabaseSource.Core)
               .Select(r => r.Name).ToArray();
       }

       public override string[] GetRolesForUser(string username)
       {
           List<string> userRoles = new List<string>();
           try
           {
               string[] usernames = username.Split(':');
               string mfbCode = usernames[1];
               string impersonateCode = mfbCode;
               if (usernames.Length > 3 && usernames[3] == "*")
               {
                   mfbCode = "";
               }
               PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser theUser = Membership.GetUser(username) as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
               if (theUser != null)
               {
                   List<UserRoleFunctionItem> userRoleFuncItems = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, theUser.UserDetails.Role.ID);
                   if (userRoleFuncItems != null && userRoleFuncItems.Count > 0)
                   {
                       long? institutionID = null;
                       if (!String.IsNullOrEmpty(mfbCode))
                       {
                           PANE.Framework.Managed.MfbServiceRef.Mfb institution = null;
                           using (PANE.Framework.Managed.MfbServiceRef.MfbServiceClient institutionClient = new PANE.Framework.Managed.MfbServiceRef.MfbServiceClient())
                           {
                               institution = institutionClient.GetByCode(mfbCode);
                               institutionID = institution.ID;
                           }
                       }

                       //Retrieve all the functionItems for that particular User Category.
                       List<FunctionItem> functionItemsForCurrentUserRole = FunctionItemDAO.GetByIDsAndUserCategory(institutionID,
                                       userRoleFuncItems.Select(urfi => urfi.TheFunctionItemID).ToArray(), theUser.UserDetails.Role.UserCategory);

                       if (functionItemsForCurrentUserRole != null && functionItemsForCurrentUserRole.Count > 0)
                       {
                           //Get the functions Role Names
                           userRoles.AddRange(functionItemsForCurrentUserRole.Select(f => f.RoleName));
                       }
                   }
               }
           }
           catch
           {

           }

           return userRoles.ToArray();
           //return SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.GetFunctionItemsForCurrentUser(username)
           //    .Select(r => r.Name).ToArray();
       }

       public override string[] GetUsersInRole(string roleName)
       {
           throw new Exception("The method or operation is not implemented.");
       }

       public override bool IsUserInRole(string username, string roleName)
       {
           try
           {
               string[] usernames = username.Split(':');
               string mfbCode = usernames[1];
               string impersonateCode = mfbCode;
               if (usernames.Length > 3 && usernames[3] == "*")
               {
                   mfbCode = "";
               }
               List<string> userRoles = new List<string>();
               PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser theUser = Membership.GetUser(username) as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
               if (theUser != null)
               {

                   FunctionItem function = FunctionItemDAO.RetrieveByRoleName(roleName, theUser.UserDetails.Role.UserCategory);
                   if (function == null) return false;

                   IList<UserRoleFunctionItem> userRoleFuncItems = UserRoleFunctionItemDAO.RetrieveByFunctionItemIDAndRoleID(mfbCode, function.ID, theUser.UserDetails.Role.ID);
                   if (userRoleFuncItems != null && userRoleFuncItems.Count > 0)
                   {
                       return true;
                   }
               }
           }
           catch
           {
               //return false;
           }
           return false;

       }

       public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
       {
           throw new Exception("The method or operation is not implemented.");
       }

       public override bool RoleExists(string roleName)
       {
           throw new Exception("The method or operation is not implemented.");
       }




       public override void Initialize(string name, NameValueCollection attributes)
       {
           lock (this)
           {
               base.Initialize(name, attributes);
               this._providerName = name;
               this._endpointName = attributes["endpoint"];
               this.applicationName = attributes["applicationName"];
               this.assemblyName = attributes["assemblyName"];
           }
       }
    }
}
