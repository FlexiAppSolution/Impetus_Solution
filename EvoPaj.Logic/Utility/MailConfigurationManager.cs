using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace EvoPaj.Logic.Utility
{
    public class MailConfigurationManager
    {
        private static NameValueCollection MailConfig
        {
            get
            {
                return System.Configuration.ConfigurationManager.GetSection("Messaging.Configuration") as NameValueCollection;
            }
        }

        public bool UsesAuthentication
        {
            get
            {
                return Convert.ToBoolean(MailConfig["UsesAuthentication"]);
            }
            set
            {
                UsesAuthentication = value;
            }
        }

        public string MailFrom
        {
            get
            {
                return MailConfig["MailFrom"];
            }
            set
            {
                MailFrom = value;
            }
        }

        public string ApplicationUrl
        {
            get
            {
                return MailConfig["ApplicationUrl"];
            }
            set
            {
                ApplicationUrl = value;
            }
        }

        public string Host
        {
            get
            {
                return MailConfig["Host"];
            }
            set
            {
                Host = value;
            }
        }

        public string UserName
        {
            get
            {
                return MailConfig["UserName"];
            }
            set
            {
                UserName = value;
            }
        }
        public string Password
        {
            get
            {
                return MailConfig["Password"];
            }
            set
            {
                Password = value;
            }
        }

        public string Domain
        {
            get
            {
                return MailConfig["Domain"];
            }
            set
            {
                Domain = value;
            }
        }
        public string Port
        {
            get
            {
                return MailConfig["Port"];
            }
            set
            {
                Port = value;
            }
        }
        public bool EnableSSL
        {
            get
            {
                return Convert.ToBoolean(MailConfig["EnableSSL"]);
            }
            set
            {
                EnableSSL = value;
            }
        }

        public string MailTemplatePath
        {
            get
            {
                return MailConfig["MailTemplatePath"];
            }
            set
            {
                MailTemplatePath = value;
            }
        }
        public string ReturnPath
        {
            get
            {
                return MailConfig["ReturnPath"];
            }
            set
            {
                ReturnPath = value;
            }
        }
    }
}
