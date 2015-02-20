using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace EvoPaj.Base.Ninject.Configuration
{
    public class ConfigurationManager
    {
        private static NameValueCollection NinjectConfig
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("Ninject") as NameValueCollection;
            }
        }

        public static string ExtensionModuleAssembly
        {
            get
            {
                return NinjectConfig["ExtensionModuleAssembly"];
            }
        }
    }
}
