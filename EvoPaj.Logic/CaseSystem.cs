using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Web;


namespace EvoPaj.Logic
{
    public class CaseSystem  : CoreServices<Case, long>
    {
        public CaseSystem()
        {

        }

        public bool SaveCase(Case theCase)
        {
            bool result = false;
            try
            {
                new CaseDAO().Save(theCase);
                result = true;
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}",ex.Message));
                result = false;
                new CaseDAO().RollbackChanges();
            }
            return  result;
        }

        public bool UpdateCase(Case theCase)
        {
            bool result = false;
            try
            {
                new CaseDAO().Update(theCase);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        public bool DeleteCase(Case theCase)
        {
            bool result = false;
            try
            {
                new CaseDAO().Delete(theCase);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        public bool UpdateCase(string query)
        {
            bool result = false;
            try
            {
                new CaseDAO().RunQuery(query);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }

        public bool CommitChanges()
        {
            bool result = false;
            try
            {
                new CaseDAO().CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;

        }
        public Case RetrieveByID(long ID)
        {
            return new CaseDAO().Retrieve(ID);
        }
        public IList<Case> SearchCase(long instionitutionID, string Name, DateTime? DateLogged, DateTime? DateTo, Case thecase)
        {
            return new CaseDAO().SearchCase(instionitutionID, Name, DateLogged, DateTo, thecase);
        }

        public IList<Case> SearchLoggedCase(string Name, DateTime DateLogged, DateTime DateTo, int start, int limit, out int numOfResult)
        {
            long instituionID = Convert.ToInt64(HttpContext.Current.Session["InstitutionID"]);                     

            return new CaseDAO().SearchCaseSub(instituionID, Name, DateLogged, DateTo, EvoPaj.Base.Utility.CaseType.Existing, start, limit, out numOfResult);
        }

        public bool LogCase(string issue, string description, string errorcode)
        {
            try
            {
                bool result = false;

                Case TheCase = new Case();

                var currentUser = HttpContext.Current.Session["loggedinuser"];

                if (currentUser == null)
                {
                    throw new Exception("Your session has timed out. Kindly logout and login afresh.");
                }
                else
                {
                    User theUser = (User)currentUser;

                    Institution theInstitution = new Institution(theUser.TheInstitution.ID);
                    TheCase.Name = issue;
                    TheCase.Description = description;
                    TheCase.ErrorCode = errorcode;
                    TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.New;
                    TheCase.DateLogged = System.DateTime.Now;
                    TheCase.DateResolved = System.DateTime.Now;
                    TheCase.LoggingUser = theUser;
                    TheCase.LoggingInstition = theInstitution;

                    bool save = new CaseSystem().SaveCase(TheCase);
                    if (save == true)
                    {
                        result = true;
                        Email logEmail = new Email();
                        string websiteurl = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"];
                        logEmail.ToAddress = System.Configuration.ConfigurationManager.AppSettings["PajunoEmail"];
                        logEmail.Title = "Impetus Web Case Creation";
                        logEmail.Body = "A case has been created on Impetus Web Solution with the following details:<br/>";
                        logEmail.Body += "<b>Issue:</b>" + TheCase.Name + "<br/>";
                        logEmail.Body += "<b>Description:</b>" + TheCase.Description + "<br/>";
                        logEmail.Body += "<b>Logged By:</b>" + TheCase.LoggingUser.FullName + "<br/>";
                        logEmail.Body += "<b>Institution:</b>" + TheCase.LoggingUser.TheInstitution.InstitutionName + "<br/>";
                        logEmail.Comments = "With Impetus Web Solution, you get instant resolution to any printer issue.";
                        new EmailSystem().LogEmail(logEmail);
                    }
                    else
                    {
                        throw new Exception("Case logging was not successful. Kindly try again.");
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IList<Case> GetCaseByErrorCode(string errorCode)
        //{
        //   // return new CaseDAO().GetCaseByErrorCode(errorCode);
        //}
    }
}
