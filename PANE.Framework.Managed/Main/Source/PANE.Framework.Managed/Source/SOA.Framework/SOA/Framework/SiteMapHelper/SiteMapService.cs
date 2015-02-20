namespace SOA.Framework.SiteMapHelper
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Web;
    using System.Web.Security;
    using System.Linq;
    using System.Security.Principal;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed), ServiceContract(Namespace = "")]
    public class SiteMapService
    {
        [OperationContract]
        public SiteMapItem FindSiteMapNode(string userName, string rawUrl)
        {
            SiteMapNode node = SiteMap.Provider.FindSiteMapNode(rawUrl); 
            GenericIdentity id = new GenericIdentity(userName);
            GenericPrincipal pcl = new GenericPrincipal(id, Roles.GetRolesForUser(userName));

            if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(node.Url.Split('?')[0], pcl, HttpContext.Current.Request.HttpMethod))
            {
                return new SiteMapItem(node);
            }
            return null;
        }

        [OperationContract]
        public SiteMapItem FindSiteMapNodeFromKey(string userName, string key)
        {
            SiteMapNode node = SiteMap.Provider.FindSiteMapNodeFromKey(key);
            return new SiteMapItem(node);
        }

        public bool CheckURLAuthorisation(string userName, SiteMapNode node)
        {
            if (node.Roles == null || node.Roles.Count == 0 ||  node.Roles[0].ToString() == "*")
            {
                return true; 
            }
            if (node.Roles.Count > 0)
            {
                foreach (string s in node.Roles)
                {
                    if (s.Contains("*") == true)
                        return true;  
                }
            }
            string[] rolesss = Roles.GetRolesForUser(userName);// to remove 
             
            foreach (string s in rolesss)
            {
                if (node.Roles.IndexOf(s) > -1)
                {
                    return true; 
                }
            }

            return false; 
        }

        public bool CheckURLAuthorisationforChildNode(string userName, SiteMapNode node)
        {
            if (node.Roles == null || node.Roles.Count == 0 || node.Roles[0].ToString() == "*")
            {
                return true;
            }
            if (node.Roles.Count > 0)
            {
                foreach (string s in node.Roles)
                {
                    if (s.Contains("*") == true)
                        return true;
                }
            }
            string[] rolesss = Roles.GetRolesForUser(userName);// to remove 

            foreach (string s in rolesss)
            {
                if (node.Roles.IndexOf(s) > -1)
                {
                    return true;
                }
            }

            return false;
        }

        [OperationContract]
        public string[] GetRoles(string userName)
        {
            return Roles.GetRolesForUser(userName);   
        }

        [OperationContract]
        public List<SiteMapItem> GetChildNodes(string userName, SiteMapItem item)
        {
            List<SiteMapItem> list = new List<SiteMapItem>();
            SiteMapNode node = SiteMap.Provider.FindSiteMapNodeFromKey(item.Key);
            if (node != null)
            {
                if (node.ChildNodes == null)
                {
                    return list;
                }

                //string[] rolesForUser = Roles.GetRolesForUser(userName);
                string[] rolesForCurrentUser = Roles.GetRolesForUser(userName);
                GenericIdentity id = new GenericIdentity(userName);
                GenericPrincipal pcl = new GenericPrincipal(id, rolesForCurrentUser);

                foreach (SiteMapNode node2 in node.ChildNodes)
                {
                    if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(node2.Url.Split('?')[0], pcl, HttpContext.Current.Request.HttpMethod))
                    {
                        list.Add(new SiteMapItem(node2));
                    }
                }
            }
            return list;
        }

        [OperationContract]
        public SiteMapItem GetParentNode(string userName, SiteMapItem item)
        {
            SiteMapNode node = SiteMap.Provider.FindSiteMapNodeFromKey(item.Key);
            return new SiteMapItem(node);
        }

        [OperationContract]
        public SiteMapItem GetSiteMap(string userName)
        {
            //return new SiteMapItem(SiteMap.RootNode);
            SiteMapItem item = new SiteMapItem(SiteMap.RootNode, userName);
            return item;
        }

        private bool IsValidUser(SiteMapNode node, string userName)
        {
            bool validUser = false;
            
            if (node.Roles == null || node.Roles.Count == 0)
            {
                validUser = true;
            }
            else
            {
                foreach (string role in node.Roles)
                {
                    if (role == "*")
                    { validUser = true; }
                    else
                    {
                        validUser = Roles.IsUserInRole(userName, role);
                        if (!validUser) break;
                    }
                }
            }
            return validUser;
        }

        private bool IsValidUser(SiteMapNode node, string userName, string[] rolesForCurrentUser)
        {
            bool validUser = false;

            if (node.Roles == null || node.Roles.Count == 0)
            {
                validUser = true;
            }
            else
            {
                foreach (string role in node.Roles)
                {
                    if (role == "*")
                    { validUser = true; }
                    else
                    {
                        validUser = rolesForCurrentUser.Contains(role);
                        if (!validUser) break;
                    }
                }
            }
            return validUser;
        }


        private SiteMapItem GetItem(SiteMapNode node, string userName)
        {
            if (node != null)
            {
                bool validUser = this.IsValidUser(node, userName);
                if (validUser)
                {
                    return new SiteMapItem(node);
                }
            }
            return null;
        }

        private SiteMapItem GetItem(SiteMapNode node, string userName, string[] rolesForCurrentUser)
        {
            if (node != null)
            {
                bool validUser = this.IsValidUser(node, userName, rolesForCurrentUser);
                if (validUser)
                {
                    return new SiteMapItem(node);
                }
            }
            return null;
        }

    }
}

