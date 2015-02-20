namespace PANE.Framework.ExceptionHandling
{
    using System;
    using System.IO;
    using System.Web;
    using PANE.Framework.Utility;
    using System.Diagnostics;
    using System.Text;

    public class Logger
    {
        private static string BuildErrorMsg(Exception ex)
        {
            if (ex != null)
            {
                /*string date = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                string msg = ex.GetBaseException().Message;
                string Page = ex.GetBaseException().TargetSite.Name;
                if (HttpContext.Current != null)
                {
                    Page = HttpContext.Current.Request.RawUrl;
                }
                string Trace = ex.GetBaseException().StackTrace;
                return string.Format("\r\n\r\n[{0}]\r\n Subject: \t{1}\r\n Page Request: \t{2}\r\n Stack Trace : \t{3}", new object[] { date, msg, Page, Trace });*/
                StringBuilder exMessage = new StringBuilder(ex.ToString());
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    exMessage.AppendLine();
                    exMessage.AppendLine(ex.ToString());
                }
                return exMessage.ToString();
            }
            return "";
        }

        public static void LogException(Exception ex)
        {
            Trace.TraceError(BuildErrorMsg(ex));
            Trace.Flush();
        }
    }
}

