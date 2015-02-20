using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using PANE.Framework.DTO;
using System.Net;
using System.Configuration;
using System.Web;

namespace PANE.Framework.Utility
{
    public class MailMaster
    {
        string MailSubject, MailBody, MailFrom;
        public MailMaster()
        {
        }
        public MailMaster(string subject, string body, string EmailTo)
        {
            this.EmailTo = EmailTo;
            this.MailSubject = subject;
            this.MailBody = body;
        }

        public virtual string EmailTo { get; set; }
        public virtual string ToAlias { get; set; }
        public virtual string FromAlias { get; set; }

        public bool SendEmail(string toemail, string body)
        {
            try
            {
                MailMessage msg = new MailMessage();

                if (!String.IsNullOrEmpty(body) && !String.IsNullOrEmpty(body.Trim()))
                    msg.Body = body;
                if (!String.IsNullOrEmpty(toemail) && !String.IsNullOrEmpty(toemail.Trim()))
                    msg.To.Add(toemail);

                SmtpClient client = new SmtpClient();
                client.Host = "localhost";
                msg.From = new MailAddress("mail@paneinternational.com");

                client.Send(msg);
            }
            catch
            {
                throw;

            }
            return true;
        }

        public bool SendEmail(string toemail, string body, string subject)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.IsBodyHtml = true;

                if (!String.IsNullOrEmpty(body) && !String.IsNullOrEmpty(body.Trim()))
                    msg.Body = body;
                if (!String.IsNullOrEmpty(toemail) && !String.IsNullOrEmpty(toemail.Trim()))
                    msg.To.Add(toemail);
                if (!String.IsNullOrEmpty(subject) && !String.IsNullOrEmpty(subject.Trim()))
                    msg.Subject = subject;

                SmtpClient client = new SmtpClient();
                //client.Host = "localhost";
                //msg.From  = new MailAddress("mail@paneinternational.com"); 

                client.Send(msg);
            }
            catch
            {
                throw;

            }
            return true;
        }


        public static string ConstructMailBody(string path, Dictionary<string, string> keyValuePairs)
        {
            string mailBody = File.ReadAllText(path);

            foreach (KeyValuePair<string, string> key in keyValuePairs)
            {
                mailBody.Replace(string.Format("<%", key.Key, "%>"), key.Value);
            }
            return mailBody;
        }

        public bool SendEmail(string path, Dictionary<string, string> KeyValuePairs, string Toemail, string subject)
        {
            try
            {
                //string mailbody = File.ReadAllText(path);
                // foreach (KeyValuePair<string, string> key in KeyValuePairs)
                // {
                //     mailbody.Replace(string.Format("<%", key.Key, "%>"), key.Value);      
                // }



                string mailbody = ConstructMailBody(path, KeyValuePairs);
                SendEmail(Toemail, mailbody, subject);


            }
            catch
            {
                throw;
            }
            return true;
        }

        public bool SendEmail(string path, object entity, string Toemail)
        {
            try
            {
                WebRequest request = HttpWebRequest.Create(path);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string mailBody = reader.ReadToEnd();


                mailBody = Regex.Replace(mailBody.ToString(), @"#\S*#", x =>
                {
                    PropertyInfo pInfo = entity.GetType().GetProperty(x.Value.Trim('#'), BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
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



                Match subjectMatch = Regex.Match(mailBody, @"<title>\D*</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string subject = string.Empty;
                if (subjectMatch.Success) subject = Regex.Replace(subjectMatch.Value, "(<title>|</title>)", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                SendEmail(Toemail, mailBody, subject);
            }
            catch
            {
                throw;
            }
            return true;
        }
        public List<string> GetEmailValues(string path, object entity)
        {
            List<string> toReturn = new List<string>();

            try
            {
                WebRequest request = HttpWebRequest.Create(path);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string mailBody = reader.ReadToEnd();


                mailBody = Regex.Replace(mailBody.ToString(), @"#\S*#", x =>
                {
                    PropertyInfo pInfo = entity.GetType().GetProperty(x.Value.Trim('#'), BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
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

                Match subjectMatch = Regex.Match(mailBody, @"<title>\D*</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string subject = string.Empty;
                if (subjectMatch.Success) subject = Regex.Replace(subjectMatch.Value, "(<title>|</title>)", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

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
            mailBody = Regex.Replace(mailBody.ToString(), @"#\S*#", x =>
            {
                PropertyInfo pInfo = entity.GetType().GetProperty(x.Value.Trim('#'), BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
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
        public bool SendEmail(string path, string Toemail, Dictionary<string, object> entities)
        {
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

                SendEmail(Toemail, mailBody, subject);
            }
            catch
            {
                throw;
            }
            return true;
        }

        public bool SendEmail(MailMessage msg)
        {

            try
            {
                if (msg != null)
                {
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);
                }
                else
                    return false;
            }
            catch
            {
                throw;

            }
            return true;
        }
        public bool Send()
        {

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.Body = this.MailBody;
                mailMessage.Subject = this.MailSubject;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                if (String.IsNullOrEmpty(this.FromAlias))
                {
                    mailMessage.From = new MailAddress("NotificationSystem@Appzonegroup.com");
                }
                else
                {
                    //mailMessage.From = new MailAddress(this.MailFrom, this.FromAlias);
                }


                if (!String.IsNullOrEmpty(this.ToAlias))
                {
                    //  mailMessage.To.Add(new MailAddress(this.EmailTo, this.ToAlias));
                }
                else
                {
                    mailMessage.To.Add(new MailAddress(this.EmailTo));
                }


                SmtpClient smtpClient = new SmtpClient();


                smtpClient.Host =ConfigurationManager.AppSettings["SMTPHost"];
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
                bool EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                string domain = ConfigurationManager.AppSettings["domain"];
                string useAuth = ConfigurationManager.AppSettings["UsesAuthentication"];
                string userName = (ConfigurationManager.AppSettings["userName"]);
                string password = (ConfigurationManager.AppSettings["password"]);

                if (Convert.ToBoolean(useAuth))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(userName, password, domain);

                }

                if (EnableSsl == true)
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);

                }

                mailMessage.From = new MailAddress("NotificationSystem@Appzonegroup.com");
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Could not send Email Message : {0}", ex.GetBaseException().Message));

            }

        }
    }
}
