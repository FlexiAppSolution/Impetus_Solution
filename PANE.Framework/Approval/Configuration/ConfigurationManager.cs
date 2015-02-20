using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace PANE.Framework.Approval.Configuration
{
    public class ConfigurationManager
    {
        private static NameValueCollection ApprovalConfig
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("PANE.Framework.Approval") as NameValueCollection;
            }
        }

        public static Assembly ServiceAssembly
        {
            get
            {
                return Assembly.LoadWithPartialName(ApprovalConfig["ServiceAssemblyName"]);
            }
        }

    }
}
