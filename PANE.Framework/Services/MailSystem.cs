using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PANE.Framework.Services;


using System.Net.Mail;
using System.Net;

using System.IO;
using PANE.Framework.Utility;
using PANE.Framework.Functions.DTO;
using PANE.Framework.Data;
using System.Reflection;
using PANE.Framework.DTO;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using PANE.Framework.ExceptionHandling;


namespace PANE.Framework.Services
{
    public class MailSystem
    {
        public IList<Mail> RetrieveUnsent()
        {
            return new MailDAO().RetrieveUnsentMails();
        }
        public IList<Mail> RetrieveUnsentBatched(int batchNo)
        {
            return new MailDAO().RetrieveUnsentMails(batchNo);
        }
        public void SendAllUnsentMails()
        {
            SmtpClient client = GetClient();
            IList<Mail> unSentMails = RetrieveUnsentBatched(10);
            foreach(Mail mail in unSentMails)
            {
                long configValidDays = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["DefaultNumberOfDays"]);
                try
                {
                    if (mail.ValidDays == 0)
                    {

                        mail.ValidDays = configValidDays;
                    }
                    if (DateTime.Now<=mail.DateCreated.AddDays(mail.ValidDays))// check if the valid days set for the mail havent been exceded
                    {
                        MailMessage msg = GetMessage(mail);

                        SendMail(msg, client, mail);
                    }
                }
                catch(Exception ex)
                {
                    Logger.LogException(ex);
                    continue;
                }
               
            }
        }
        public MailMessage GetMessage(Mail mail)
        {

   //            bool UseConfig = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UseConfigSettings"]);
            MailMessage msg;
           string fromAddress = "";
          
           fromAddress = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
           
           if (!mail.ToAddress.Contains(';'))
               msg = new MailMessage(fromAddress, mail.ToAddress, mail.subject, mail.MailMessage);
           else
           {
               MailAddressCollection cc = new MailAddressCollection();
               List<string> values = mail.ToAddress.Split(";".ToCharArray()).ToList();
              MailAddress toMail = new MailAddress(values[0]);
              msg=new MailMessage(fromAddress,values[0],mail.subject,mail.MailMessage);
              values.RemoveAt(0);
              foreach (string str in values)
              {
                  msg.CC.Add(new MailAddress(str));
              }
              
           }
            msg.IsBodyHtml = true;
            return msg;
        }
        public SmtpClient GetClient()
        {

            SmtpClient client = new SmtpClient();
            # region Commented Code
            
            //bool UseConfig = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UseConfigSettings"]);
            //if (UseConfig)//Get Settings from config file
            //{
            //    string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            //    string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            //    string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                
                
            //    int port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            //    client = new SmtpClient(host, port);
            //    client.Credentials = new NetworkCredential(userName, password);
            //}
           
            //AppConfig config = new AppConfigSystem().Retrieve();
            //SmtpClient client = new SmtpClient(config.MailServer,config.Port);

            //if(string.IsNullOrEmpty(config.EmailSenderPassword)||string.IsNullOrEmpty(config.EmailSenderPassword.Trim()))
            //{
             
            //}
            //else
            //{
            //    client.Credentials = new NetworkCredential(config.EmailSenderUserName, config.EmailSenderPassword);
            //}
            //return client;
            //else
            //{
            //    AppConfig config = new AppConfigSystem().Retrieve();
            //    client.Host = config.MailServer;
            //    client.Port = config.Port;
            //    if (!(string.IsNullOrEmpty(config.EmailSenderPassword ) || string.IsNullOrEmpty(config.EmailSenderPassword.Trim())))
            //    {
            //        client.Credentials = new NetworkCredential(config.EmailSenderUserName,new AES().DecryptString( config.EmailSenderPassword));
            //    }


            //}

            # endregion
            return client;
                
        }
        public void SendMail(MailMessage msg, SmtpClient client,Mail mail)
        {
            try
            {
                
                string userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
                string password = System.Configuration.ConfigurationManager.AppSettings["Password"];
                //client.=new System.Net.SystemNetworkCredential
                System.Net.NetworkCredential credential = new System.Net.NetworkCredential(userName, password);
                client.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSSL"]);
                client.Credentials = credential;
                client.Send(msg);
                mail.DateSent = DateTime.Now;           
                
                
            }
            catch (Exception ex)
            {
                mail.TryCount++;
                mail.MailStatus = MailStatus.Failed;
                Logger.LogException(ex);
                
                # region CommentedCode
                //if (mail.DataType == Datatype.User && mail.TryCount >= 15)
                //{
                //    User user = new UserSystem().RetrieveById(mail.DataID);
                //    mail.MailStatus = MailStatus.Sent;
                //    if (user != null)
                //    {
                //        user.Password = new MD5Password().CreateSecurePassword("password");
                //        new UserSystem().Update(user);
                //    }
                //    MailDAO.Update(mail);
                //}
                # endregion

            }
            finally
            {
                MailDAO.Update(mail);
                MailDAO.CommitChanges();
            }
        }
        public IList<string> GetEmailToSave(string path,string fromEmail, Dictionary<string, object> entities)
        {
            IList<string> toReturn = new List<string>();
            try
            {
                WebRequest request = HttpWebRequest.Create(path);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string mailBody = reader.ReadToEnd();
                foreach (KeyValuePair<string, object> entity in entities)
                {
                    mailBody = TransformMessage(mailBody, entity.Value, entity.Key);
                }

                Match subjectMatch = Regex.Match(mailBody, @"<title>[0-9a-zA-Z -]*</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string subject = "";
                if (subjectMatch.Success) subject = Regex.Replace(subjectMatch.Value, "(<title>|</title>)", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                // SendEmail(Toemail, mailBody, subject);
                toReturn.Add(fromEmail);
                toReturn.Add(mailBody);
                toReturn.Add(subject);
            }
            catch
            {
                throw;
            }
            return toReturn;
        }
        string TransformMessage(string mailBody, object entity, string prefix)
        {
            mailBody = Regex.Replace(mailBody.ToString(), string.Format(@"#{0}\.\S*#", prefix), x =>
            {
                PropertyInfo pInfo = entity.GetType().GetProperty(x.Value.Trim('#').Replace(prefix + ".", ""), BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                if (pInfo != null && pInfo.CanRead)
                {
                    if (pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.Equals(typeof(DateTime)) || pInfo.PropertyType.Equals(typeof(String)))
                    {
                        return Convert.ToString(pInfo.GetValue(entity, null));
                    }
                    else
                    {
                        IEntity subEntity = pInfo.GetValue(entity, null) as IEntity;
                        if (subEntity != null) return subEntity.Name;
                        else return string.Empty;
                    }
                }
                return string.Empty;
            });
            return mailBody;
        }

        public Mail CreateEmail(string path,string fromEmail,IList<string> emailAddresses,  Dictionary<string, Object> entities,DataObject obj)
        {
            IList<string> mailValues = GetEmailToSave(path, fromEmail, entities);
            Mail mail = new Mail();
            string toAddreses = string.Empty;
               foreach (string str in emailAddresses)
                {
                    toAddreses += str + ";";
                }
                toAddreses = toAddreses.TrimEnd(";".ToCharArray());//the string will always have a trailing ";" so we shuld remove it
                //if(toAddreses.EndsWith(";"))
                //{
                //    toAddreses.Remove(toAddreses.LastIndexOf(';'),1);
                //}

            mail.DataID = (obj as Approval.DTO.Approval).ID;
            mail.DataType = DataType.Approval;
            mail.MailStatus = MailStatus.NotSent;
            mail.TryCount = 0;
            mail.DateCreated = DateTime.Now;
            mail.DateSent = (DateTime)SqlDateTime.Null;

            mail.ToAddress = toAddreses;

            mail.MailMessage = mailValues[1];
            mail.subject = mailValues[2];
            return mail;
        }
        
    }
}
