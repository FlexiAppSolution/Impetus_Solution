namespace SOA.Framework.SiteMapHelper
{
    using SOA.Framework.SMS;
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.Web.Security;

    public class ManagedSiteMapProvider : SiteMapProvider
    {
        private string _endpointName = null;
        private string _iconName = "None";
        private string _providerName = null;
        private int _applicationID = 0;
        private string HC_ROOT_NODE = "::HC_ROOT_NODE::";
        private string HC_ALL_NODES = "::HC_ALL_NODES::";

        private SiteMapItem TheRootNode
        {
            get
            {
                return HttpContext.Current.Items[HC_ROOT_NODE + _providerName] as SiteMapItem;
            }
            set
            {
                HttpContext.Current.Items[HC_ROOT_NODE + _providerName] = value;
            }
        }

        private Dictionary<string, SiteMapItem> AllNodes
        {
            get
            {
                if (HttpContext.Current.Items[HC_ALL_NODES + _providerName] == null)
                {

                    if (TheRootNode == null)
                    {
                        return new Dictionary<string, SiteMapItem>();
                    }

                    Dictionary<string, SiteMapItem> allNodes = new Dictionary<string, SiteMapItem>();
                    allNodes.Add(TheRootNode.Key, TheRootNode);
                    AddToList(allNodes, TheRootNode.Items);
                    HttpContext.Current.Items[HC_ALL_NODES + _providerName] = allNodes;
                }
                return HttpContext.Current.Items[HC_ALL_NODES + _providerName] as Dictionary<string, SiteMapItem>;
            }
        }

        private void AddToList(Dictionary<string, SiteMapItem> allNodes, List<SiteMapItem> childNodes)
        {
            foreach (SiteMapItem node in childNodes)
            {
                allNodes.Add(node.Key, node);
                AddToList(allNodes, node.Items);
            }
        }

        // private 

        //private SiteMapNode ConvertFrom(SiteMapItem nodeItem)
        //{
        //    if (nodeItem.GetSiteMapNode(this) == null)
        //    {
        //        return null;
        //    }
        //    return new SiteMapNode(this, nodeItem.Key, nodeItem.Url, nodeItem.Title, nodeItem.Description, nodeItem.Roles, null, null, null);
        //}

        //private SOA.Framework.SMS.SiteMapItem ConvertFrom(SiteMapNode node)
        //{
        //    if (node == null)
        //    {
        //        return null;
        //    }
        //    return new SiteMapItem(node) { Key = node.Key, Url = node.Url, Title = node.Title, Description = node.Description, Roles = node.Roles.Cast<string>().ToList<string>() };
        //}

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            SiteMapServiceClient client = new SiteMapServiceClient(this._endpointName);
            SiteMapNode node = null;
            try
            {
                node = client.FindSiteMapNode(HttpContext.Current.User.Identity.Name, rawUrl).GetSiteMapNode(this);
            }
            finally
            {
                client.Close();
            }
            return node;
        }

        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            SiteMapServiceClient client = new SiteMapServiceClient(this._endpointName);
            SiteMapNode node = null;
            try
            {
                node = client.FindSiteMapNodeFromKey(HttpContext.Current.User.Identity.Name, key).GetSiteMapNode(this);
            }
            finally
            {
                client.Close();
            }
            return node;
        }

        //private SiteMapNodeCollection GetChild(string searchKey, SiteMapNodeCollection children)
        //{
        //    foreach (SiteMapNode childNode in children)
        //    {
        //        if (childNode.Key == searchKey)
        //        {
        //            return childNode.ChildNodes;
        //        }
        //        else
        //        {
        //            return GetChild(searchKey, childNode.ChildNodes);
        //        }
        //    }
        //}

        private SiteMapNodeCollection GetSiteMapNodeCollection(SiteMapNode searchNode, SiteMapNodeCollection collectionToSearch)
        {
            foreach (SiteMapNode node in collectionToSearch)
            {
                if (node.Key == searchNode.Key)
                {
                    return node.ChildNodes;
                }

                return GetSiteMapNodeCollection(searchNode, node.ChildNodes);
            }

            return null;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            if (TheRootNode == null)
            {
                return null;
            }
            //if (node.Key == TheRootNode.Key)
            //{
            //    return TheRootNode.ChildNodes;
            //}

            //if (!AllNodes.ContainsKey(node.Key))
            //{
            //    return new SiteMapNodeCollection();
            //}
            SiteMapNodeCollection children = new SiteMapNodeCollection();
            if (AllNodes.ContainsKey(node.Key))
            {
                SiteMapItem parentNode = AllNodes[node.Key];

                foreach (SiteMapItem childNode in parentNode.Items)
                {
                    children.Add(childNode.GetSiteMapNode(this));
                }
            }
            else
            {
                return null;
            }
            return children;//GetSiteMapNodeCollection(node, TheRootNode.ChildNodes);



            //            SiteMapNodeCollection nodes = new SiteMapNodeCollection();
            //SiteMapServiceClient client = new SiteMapServiceClient(this._endpointName);

            //List<SiteMapItem> siteMapItems = client.GetChildNodes(HttpContext.Current.User.Identity.Name, new SiteMapItem(node));
            //if (siteMapItems == null || siteMapItems.Count == 0)
            //{
            //}
            //else
            //{
            //    foreach (SiteMapItem item in siteMapItems)
            //    {
            //        SiteMapNode node2 = item.GetSiteMapNode(this);
            //        nodes.Add(node2);

            //    }
            //}
            //client.Close();
            //return theNode.ChildNodes;
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            SiteMapServiceClient client = new SiteMapServiceClient(this._endpointName);
            SiteMapNode node2 = null;
            try
            {
                node2 = client.GetParentNode(HttpContext.Current.User.Identity.Name, new SiteMapItem(node)).GetSiteMapNode(this);
            }
            finally
            {
                client.Close();
            }
            return node2;
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            SiteMapServiceClient client = new SiteMapServiceClient(this._endpointName);
            client.ClientCredentials.UserName.UserName = "superadmin";
            client.ClientCredentials.UserName.Password = "password";
            try
            {
                TheRootNode = client.GetSiteMap(HttpContext.Current.User.Identity.Name);
            }
            finally
            {
                //Roless = client.GetRoles(HttpContext.Current.User.Identity.Name).ToArray<string>();  
                client.Close();
            }
            return TheRootNode.GetSiteMapNode(this);
        }

        public override void Initialize(string name, NameValueCollection attributes)
        {
            lock (this)
            {
                base.Initialize(name, attributes);
                this._providerName = name;
                this._endpointName = attributes["endpoint"];
                if (attributes["iconName"] != null)
                {
                    this._iconName = attributes["iconName"];
                }
                if (attributes["applicationID"] != null)
                {
                    this._applicationID = Convert.ToInt32(attributes["applicationID"]);
                }
            }
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            return true;
        }

        public bool IsAccessibleToUser(HttpContext context, SiteMapNode node, string[] roless)
        {
            string[] UserRoles = roless;

            if ((node.Roles != null) && (node.Roles.Count > 0))
            {
                foreach (string str in node.Roles)
                {
                    if (str == "*")
                        return true;

                    foreach (string strrole in UserRoles)
                    {
                        if (str.Trim() == strrole.Trim())
                            return true;
                    }

                }
                return false;
            }


            return true;
        }

        public string IconName
        {
            get
            {
                return this._iconName;
            }
        }

        public int ApplicationID
        {
            get
            {
                return this._applicationID;
            }
        }
    }
}

