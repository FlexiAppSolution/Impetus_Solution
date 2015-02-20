namespace PANE.ERRORLOG
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public class ConfigurationManager
    {
        public static string DirectoryLog
        {
            get
            {
                return Convert.ToString(ErrorLogConfig["DirectoryLog"]);
            }
        }

        private static NameValueCollection ErrorLogConfig
        {
            get
            {
                return (System.Configuration.ConfigurationManager.GetSection("PANE.ErrorLog") as NameValueCollection);
            }
        }

        public static string ToEmail
        {
            get
            {
                return Convert.ToString(ErrorLogConfig["ToEmail"]);
            }
        }
    }
}

