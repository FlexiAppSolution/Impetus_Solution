using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Web;
using System.Data;

namespace EvoPaj.Logic
{
    public class IssueLogic: CoreServices<Issue, long>
    {
        public IssueLogic()
        {

        }

        public bool SaveCase(Issue theCase)
        {
            bool result = false;
            try
            {
                //new IssueDAO().Save(theCase);
                int outcome = InsertIssue(theCase);
                if (outcome > 0)
                    result = true;
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}",ex.Message));
                result = false;
                //new IssueDAO().RollbackChanges();
            }
            return  result;
        }

        public bool UpdateCase(Issue theCase)
        {
            bool result = false;
            try
            {
                //new IssueDAO().Update(theCase);
                int outcome = ModifyIssue(theCase);
                if (outcome > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool DeleteCase(Issue theCase)
        {
            bool result = false;
            try
            {
                new IssueDAO().Delete(theCase);
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
                new IssueDAO().RunQuery(query);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public static int InsertIssue(Issue somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);
           
            db.AddParamAndValue("@Name", somebody.Name.ToUpper());
            db.AddParamAndValue("@Resolution", string.IsNullOrEmpty(somebody.Resolution) ? "Not Set" : somebody.Resolution);
            db.AddParamAndValue("@Description", string.IsNullOrEmpty(somebody.Description) ? "Not Set" : somebody.Description);
            db.AddParamAndValue("@ErrorCode", somebody.ErrorCode);
            if (somebody.TheCaseType == Base.Utility.CaseType.Existing)
                db.AddParamAndValue("@CaseType", "Existing");
            else
                db.AddParamAndValue("@CaseType", "New");
            db.AddParamAndValue("@DateLogged", somebody.DateLogged);
            db.AddParamAndValue("@DateResolved", somebody.DateResolved);
            db.AddParamAndValue("@LoggingUserID", somebody.LoggingUser.ID);
            db.AddParamAndValue("@LoggingInstitutionID", somebody.LoggingInstition.ID);
            db.AddParamAndValue("@CaseNumber", somebody.CaseNumber);
           

            int status = db.ExecuteNonQuery("sp_insert_issue", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public static int ModifyIssue(Issue somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@Name", somebody.Name.ToUpper());
            db.AddParamAndValue("@Resolution", somebody.Resolution.ToUpper());
            db.AddParamAndValue("@Description", somebody.Description.ToUpper());
            db.AddParamAndValue("@ErrorCode", somebody.ErrorCode);
            if (somebody.TheCaseType == Base.Utility.CaseType.Existing)
                db.AddParamAndValue("@CaseType", "Existing");
            else
                db.AddParamAndValue("@CaseType", "New");            
            db.AddParamAndValue("@DateLogged", somebody.DateLogged);
            db.AddParamAndValue("@DateResolved", somebody.DateResolved);
            db.AddParamAndValue("@LoggingUserID", somebody.LoggingUser.ID);
            db.AddParamAndValue("@LoggingInstitutionID", somebody.LoggingInstition.ID);
            db.AddParamAndValue("@CaseNumber", somebody.CaseNumber);
            db.AddParamAndValue("@ID", somebody.ID);


            int status = db.ExecuteNonQuery("sp_update_issue", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public bool CommitChanges()
        {
            bool result = false;
            try
            {
                new IssueDAO().CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;

        }
        
        public Issue RetrieveByID(long ID)
        {
            return new IssueDAO().Retrieve(ID);
        }
        
        public IList<Issue> SearchCase(long instionitutionID, string Name, DateTime? DateLogged, DateTime? DateTo, Issue thecase)
        {
            return new IssueDAO().SearchCase(instionitutionID, Name, DateLogged, DateTo, thecase);
        }

        public IList<Issue> SearchLoggedCase(string Name, DateTime DateLogged, DateTime DateTo, int start, int limit, out int numOfResult)
        {
            long instituionID = Convert.ToInt64(HttpContext.Current.Session["InstitutionID"]);

            return new IssueDAO().SearchCaseSub(instituionID, Name, DateLogged, DateTo, EvoPaj.Base.Utility.CaseType.Existing, start, limit, out numOfResult);
        }

        public bool LogCase(string issue, string description, string errorcode)
        {
            try
            {
                bool result = false;

                Issue TheCase = new Issue();

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

                    bool save = new IssueLogic().SaveCase(TheCase);
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

        public IList<Issue> GetCaseByErrorCode(string errorCode)
        {
             return new IssueDAO().GetCaseByErrorCode(errorCode);
        }
    }
}
