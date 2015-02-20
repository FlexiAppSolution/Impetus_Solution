using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace PANE.Framework.Utility
{
    public class MailConfigurationManager
    {
        private static NameValueCollection PANEErrorLogSection
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("PANE.ErrorLog") as NameValueCollection;
            }
        }
        public static string DirectoryLog
        {
            get
            {
                return PANEErrorLogSection["DirectoryLog"];
            }
        }
    }
}
