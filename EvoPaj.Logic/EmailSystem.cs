using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using Evopaj.Data;
using EvoPaj.Base.Utility;
using EvoPaj.Logic.Utility;
using System.Net.Mail;
using System.Net;
using EvoPaj.Data;
using System.Data;

namespace EvoPaj.Logic
{
    public class EmailSystem : CoreServices<Email, long>
    {
        public void LogEmail(Email email)
        {
            EmailDAO dao = new EmailDAO();
            try
            {
                
                email.Status = EmailStatus.Pending;
                //dao.Save(email);
                //dao.CommitChanges();
                int outcome = InsertEmail(email);
                
            }
            catch
            {
                dao.RollbackChanges();
            }
        }
       
        public static int InsertEmail(Email somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@ToAddress", somebody.ToAddress);
            db.AddParamAndValue("@Title", somebody.Title);
            db.AddParamAndValue("@Body", somebody.Body);
            if (somebody.Status == EmailStatus.Pending)
                db.AddParamAndValue("@Status", "Pending");
            else
                db.AddParamAndValue("@Status", "Pending");

            int status = db.ExecuteNonQuery("sp_insert_email", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public void SendPendingEmails()
        {
            new PANE.ERRORLOG.Error().LogToFile(new Exception("Inside sending pending mails method"));
            List<Email> pendingEmails =  EmailDAO.GetPendingEmails();
            new PANE.ERRORLOG.Error().LogToFile(new Exception("After getting pending mails method"));
            if (pendingEmails != null && pendingEmails.Count > 0)
            {
                foreach (Email email in pendingEmails)
                {
                   
                    try
                    {
                        new PANE.ERRORLOG.Error().LogToFile(new Exception("Inside for each mails method"));
                        MailConfigurationManager mCm = new MailConfigurationManager();
                        SmtpClient client = GetClient(mCm);
                        //MailMessage msg = new   GetMessage(mail, mail.MFBCode, new MailConfigurationManager());
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress(mCm.MailFrom);
                        foreach (string s in email.ToAddress.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            msg.To.Add(new MailAddress(s));
                        }

                        msg.Subject = email.Title;
                        msg.IsBodyHtml = true;
                        
                        msg.Body = string.Format("<html><head><b>{1}</b></head><body>{0}</body></html>", email.Body, email.Title);
                        client.Send(msg);
                        email.Status = EmailStatus.Sent;
                        new EmailDAO().Update(email);
                        //new EmailDAO().Delete(email);
                        new EmailDAO().CommitChanges();
                        
                    }
                    catch (Exception ex)
                    {

                        //email.Status = EmailStatus.Failed;
                        //email.Comments = ex.Message;
                        new PANE.ERRORLOG.Error().LogToFile(ex);
                        //new EmailDAO().Update(email);
                        //new EmailDAO().CommitChanges();
                       
                    }
                }
            }

        }
        
        public bool SaveEmail(Email email)
        {
            bool result = false;
            try
            {
                email.Status = EmailStatus.Pending;
                new EmailDAO().Save(email);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
                new EmailDAO().RollbackChanges();
            }
            return result;
        }
        
        private SmtpClient GetClient(MailConfigurationManager mCm)
        {
            System.Net.Mail.SmtpClient toReturn = new System.Net.Mail.SmtpClient();
            toReturn.Host = mCm.Host;
            toReturn.Port = Convert.ToInt32(mCm.Port);
            if (mCm.UsesAuthentication)
            {
                toReturn.UseDefaultCredentials = false;
                if (System.Configuration.ConfigurationManager.AppSettings["UseGmail"] == "true")
                {
                    toReturn.Credentials = new NetworkCredential(mCm.UserName, mCm.Password);
                }
                else
                {
                    toReturn.Credentials = new NetworkCredential(mCm.UserName, mCm.Password, mCm.Host);
                }
            }
            toReturn.EnableSsl = mCm.EnableSSL;
            return toReturn;
        }
    }
}
