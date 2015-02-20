using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace PANE.Framework.Utility
{
    public class ConfigurationHelper
    {
        public static NameValueCollection AssemblyConfig<T>()
        {
            return System.Configuration.ConfigurationManager.GetSection(typeof(T).Assembly.FullName.Split(',')[0]) as NameValueCollection;
        }

        public static R ConfigItem<T, R>(string key)
        {
            R result = default(R);
            NameValueCollection nvc = System.Configuration.ConfigurationManager.GetSection(typeof(T).Assembly.FullName.Split(',')[0]) as NameValueCollection;
            if (nvc != null)
            {
                try
                {
                    result = (R)Convert.ChangeType(nvc[key], typeof(R));
                }
                catch { }
            }
            return result;
        }
    }
}
