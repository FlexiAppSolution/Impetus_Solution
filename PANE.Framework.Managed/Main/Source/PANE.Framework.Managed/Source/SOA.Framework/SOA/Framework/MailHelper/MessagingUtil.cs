using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace SOA.Framework.MailHelper
{
    public class MessagingUtil
    {
        string _institution;
        public MessagingUtil(string institution)
        {
            this._institution = institution;
        }

        public MessagingUtil()
        {
            this._institution = InstitutionName;
        }

        public bool SendMail(string body, params string[] emailAddresses)
        {
            if (this.DisableEmail)
            {
                return true;
            }

            string emails = String.Join(";", emailAddresses);
            try
            {
                using (SOA.Framework.US.UtilityServiceClient messagingClient = new SOA.Framework.US.UtilityServiceClient())
                {
                    //messagingClient.SendEmail(String.Format("{0} {1}", this._institution , ApplicationName), emails, this._title, body);
                    messagingClient.SendEmail(String.Format("{0} {1}", this._institution, ApplicationName), emails, body);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMailToAppZoneSupport(string body)
        {
            return SendMail(body, this.AppZoneSupportEmail);
        }

        private NameValueCollection NameValueCollection
        {
            get
            {
                return (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("Messaging.Notifier");
            }
        }

        public string AppZoneSupportEmail
        {
            get { return this.NameValueCollection["AppZoneSupportEmail"]; }
        }

        public string InstitutionName
        {
            get { return this.NameValueCollection["InstitutionName"]; }
        }

        public string ApplicationName
        {
            get { return this.NameValueCollection["ApplicationName"]; }
        }

        public string ApplicationUrl
        {
            get
            {
                using (SOA.Framework.US.UtilityServiceClient messagingClient = new SOA.Framework.US.UtilityServiceClient())
                {
                    return messagingClient.GetApplicationUrl();
                };
            }
        }

        public bool DisableEmail
        {
            get { return Convert.ToBoolean(this.NameValueCollection["DisableEmail"]); }
        }
    }
}
