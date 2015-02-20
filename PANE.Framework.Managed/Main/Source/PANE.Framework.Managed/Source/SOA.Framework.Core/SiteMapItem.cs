namespace SOA.Framework.SiteMapHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Web;
    using System.Security.Principal;
    using System.Web.Security;

    [DataContract]
    public class SiteMapItem
    {
        public SiteMapItem(SiteMapNode node, string userName)
            : this(node)
        {
            GenericIdentity id = new GenericIdentity(userName);
            GenericPrincipal pcl = new GenericPrincipal(id, System.Web.Security.Roles.GetRolesForUser(userName));
            foreach (SiteMapNode siteMapNode in node.ChildNodes)
            {
                if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(siteMapNode.Url.Split('?')[0], pcl, "GET"))
                {
                    this.Items.Add(new SiteMapItem(siteMapNode, pcl));
                }
            }
        }

        internal SiteMapItem(SiteMapNode node, GenericPrincipal userPrincipal)
            : this(node)
        {
            foreach (SiteMapNode siteMapNode in node.ChildNodes)
            {
                if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(siteMapNode.Url.Split('?')[0], userPrincipal, "GET"))
                {
                    this.Items.Add(new SiteMapItem(siteMapNode, userPrincipal));
                }
            }
        }

        public SiteMapItem(SiteMapNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            if (!(string.IsNullOrEmpty(node.Url) || node.Url.Contains("http")))
            {
                string managedServiecLocation = System.Configuration.ConfigurationManager.AppSettings["AppZonePortalLocation"];
                if (String.IsNullOrEmpty(managedServiecLocation))
                {
                    //throw new Exception("Please configure the Location of the server of this Managed Service is the App Settings Section.");
                    managedServiecLocation = HttpContext.Current.Request.Url.Authority;
                }

                if (!managedServiecLocation.StartsWith("http"))
                {
                    managedServiecLocation = "http://" + managedServiecLocation;
                }
                this.Url = string.Format("{0}{1}", managedServiecLocation, node.Url);
                //this.Url = string.Format("http://{0}{1}", HttpContext.Current.Request.Url.Authority, node.Url);
            }
            else
            {
                this.Url = node.Url;
            }           this.Title = node.Title;
            this.Key = node.Key;
            this.ResourceKey = node.ResourceKey;
            this.Description = node.Description;
            this.Roles = node.Roles.Cast<string>().ToList<string>();
            this.Items = new List<SiteMapItem>();
        }

        public SiteMapNode GetSiteMapNode(SiteMapProvider theProvider)
        {
            return new SiteMapNode(theProvider, this.Key, this.Url, this.Title, this.Description, this.Roles, null, null, this.ResourceKey);
        }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<SiteMapItem> Items { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public List<string> Roles { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string ResourceKey { get; set; }

     
        [DataMember]
        public string Url { get; set; }
    }
}

