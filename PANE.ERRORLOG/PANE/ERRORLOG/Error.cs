namespace PANE.ERRORLOG
{
    //using PANE.Framework.Utility;
    using System;
    using System.IO;
    using System.Web;
    using System.Diagnostics;

    public class Error : IHttpModule
    {
        EventLog eventLog1 = new EventLog();
        public Error()
        {
            try
            {
                if (!EventLog.Exists("AppzoneLog"))
                    EventLog.CreateEventSource("Appzone Notification Event", "AppzoneLog");
            }
            catch { }
            finally
            {
                eventLog1.Source = "Appzone Notification Event";
                eventLog1.Log = "AppzoneLog";
            }
        }
        private void app_Error(object sender, EventArgs e)
        {
            this.LogToFile(HttpContext.Current.Server.GetLastError());
        }

        protected string BuildErrorMsg(Exception EX)
        {
            string Page="";string Trace="";
            Exception BaseException = EX.GetBaseException();
            if (EX != null)
            {
                string date = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                string msg = EX.GetBaseException().Message;
                if (BaseException.TargetSite != null)
                {
                   Page   = BaseException.TargetSite.Name;
                }
                if (HttpContext.Current != null)
                {
                    Page = HttpContext.Current.Request.RawUrl;
                }
                if (BaseException.StackTrace != null)
                {
                    Trace = EX.GetBaseException().StackTrace;
                }
                return string.Format("\r\n\r\n[{0}]\r\n Subject: \t{1}\r\n Page Request: \t{2}\r\n Stack Trace : \t{3}", new object[] { date, msg, Page, Trace });
            }
            return "";
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.Error += new EventHandler(this.app_Error);
        }

        public void LogToFile(Exception EX)
        {
            try
            {
                File.AppendAllText(ConfigurationManager.DirectoryLog + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", this.BuildErrorMsg(EX));
            }
            catch (NullReferenceException)
            {
                try
                {
                    eventLog1.WriteEntry("A(n)" + EX + "ERROR has occured while trying to send mail\n\n\nYou must add the PANE.ERRORLog section to your configuration file and ensure that the value of the DirectoryLog key is a valid system path with Read/Write Permissions to the appropriate user for the error to be properly logged\n\n\nThe mail could not be sent because ", EventLogEntryType.Error);
                }
                catch { }
            }
            catch (FileNotFoundException FilenotFoundEx)
            {
                try
                {
                    eventLog1.WriteEntry("The path specified in the DirectoryLog key of the PANE.ERROR section of the configuration file does not exist" + FilenotFoundEx, EventLogEntryType.Error);
                }
                catch { }
            }
            catch (Exception e)
            {
                try
                {
                    eventLog1.WriteEntry("An ERROR has occured while trying to send mail.\n\n\n" + e.Message + "\n\nThe error occured at " + e.StackTrace + "\n\n" + e.InnerException, EventLogEntryType.Error);
                }
                catch { }
            }
           
        }

        public void SendEmail(Exception EX)
        {
            try
            {
               // new MailMaster().SendEmail(ConfigurationManager.ToEmail, this.BuildErrorMsg(EX));
            }
            catch
            {
            }
        }

        public void SendEmailAndLogToFile(Exception EX)
        {
            try
            {
                this.SendEmail(EX);
                this.LogToFile(EX);
            }
            catch
            {
            }
        }
        public void LogInfo(string InfoMessage)
        {
            string msg = "The appzone notification system has successfully sent a message but could not log it in the appropriate log file ";
            try
            {
                string value="\n"+DateTime.Now.TimeOfDay.ToString()+" :  "+InfoMessage;
                File.AppendAllText(ConfigurationManager.DirectoryLog + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", value);
            }
            catch (NullReferenceException)
            {
                try
                {
                    eventLog1.WriteEntry(msg + "\n\n\nYou must add the PANE.ERRORLog section to your configuration file and ensure that the value of the DirectoryLog key is a valid system path with Read/Write Permissions to the appropriate user", EventLogEntryType.Warning);
                }
                catch { }
            }
            catch (FileNotFoundException FilenotFoundEx)
            {
                try
                {
                    eventLog1.WriteEntry(msg + FilenotFoundEx, EventLogEntryType.Warning);
                }
                catch { }
            }
            catch (IOException ioexception)
            {
                try
                {
                    eventLog1.WriteEntry(msg + " because an i/o error occured while wrtitng to the file" + ioexception, EventLogEntryType.Warning);
                }
                catch { }
            }
            catch (Exception e)
            {
                try
                {
                    eventLog1.WriteEntry(msg+"\n\n\n" + e.Message + "\n\n" + e.StackTrace + "\n\n" + e.InnerException, EventLogEntryType.Warning);
                }
                catch { }
            }
        }
    }
}

