using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;
using EvoPaj.Base.Utility;
using EvoPaj.Base.Entities;
using System.Web.Security;
using System.Web;
using System.Data;

namespace EvoPaj.Logic
{
    public class LicenseSystem : CoreServices<Institution, long>
    {
        public LicenseSystem()
        {

        }
        
        public bool SaveLicense(License theLicense)
        {
            bool result = false;
            try
            {
               // new LicenseDAO().Save(theLicense);
                int outcome = InsertEXEFile(theLicense);
                if (outcome > 0)
                    result = true;
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}",ex.Message));
                result = false;
            }
            return  result;
        }
        
        public bool UpdateLicense(License theLicense)
        {
            bool result = false;
            try
            {
                //new LicenseDAO().Update(theLicense);
                int outcome = UpdateEXEFile(theLicense);
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
        
        public bool DeleteLicense(License theLicense)
        {
            bool result = false;
            try
            {
                //new LicenseDAO().Delete(theLicense);
                int outcome = DeleteEXEFile(theLicense);
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
       
        public static int InsertEXEFile(License somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@FileName", somebody.FileName);
            db.AddParamAndValue("@FilePath", somebody.FilePath);
            db.AddParamAndValue("@NumberOfProposdLicense", somebody.NumberOfProposedLicense);
            db.AddParamAndValue("@InstitutionID", somebody.TheInstitution.ID);
          
            int status = db.ExecuteNonQuery("sp_insert_license", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
       
        public static int UpdateEXEFile(License somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@FileName", somebody.FileName);
            db.AddParamAndValue("@FilePath", somebody.FilePath);
            db.AddParamAndValue("@NumberOfProposdLicense", somebody.NumberOfProposedLicense);
            db.AddParamAndValue("@InstitutionID", somebody.TheInstitution.ID);
            db.AddParamAndValue("@ID", somebody.ID);

            int status = db.ExecuteNonQuery("sp_update_license", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }

        public static int DeleteEXEFile(License somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);
            
            db.AddParamAndValue("@ID", somebody.ID);

            int status = db.ExecuteNonQuery("sp_delete_license", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
       
        public IList<License> SelectFile(License TheLicense)
        {
            return new LicenseDAO().GetFile(TheLicense);
        }

        public IList<License> SelectFiles(long ID, int start, int limit, out int numOfResults)
        {
            return new LicenseDAO().GetFiles(ID,start,limit, out numOfResults);
        }

        public PrintUtilityServiceReference.LicenseData GetLicense(string token, string licenseType, string duration)
        {
            PrintUtilityServiceReference.LicenseData licenseData = new PrintUtilityServiceReference.LicenseData();
            using (var item = new PrintUtilityServiceReference.PrintUtilityServiceSoapClient())
            {
                licenseData = item.GenerateLicenseFromToken(token, licenseType, duration);
            }
            return licenseData;
        }

        public PrintUtilityServiceReference.LicenseData GetRightCode(string wrongCode)
        {
            PrintUtilityServiceReference.LicenseData licenseData = new PrintUtilityServiceReference.LicenseData();
            using (var item = new PrintUtilityServiceReference.PrintUtilityServiceSoapClient())
            {
                licenseData = item.GenerateRightCodeFromErrorCode(wrongCode);
            }
            return licenseData;
        }

        public PrintUtilityServiceReference.LicenseData GetServiceCode(string wrongCode)
        {
            PrintUtilityServiceReference.LicenseData licenseData = new PrintUtilityServiceReference.LicenseData();
            using (var item = new PrintUtilityServiceReference.PrintUtilityServiceSoapClient())
            {
                licenseData = item.GenerateRightServiceCode(wrongCode);
            }
            return licenseData;
        }

        public bool RequestForLicense(Int32 NumofLicense, LicenseType TheLicenceType)
        {
            bool result = false;
            
            int usedLicense = 0;
            int approvedLicense = 0;
            var currentUser = HttpContext.Current.Session["userID"];
            if (currentUser != null)
            {
                long UserID = Convert.ToInt64(currentUser);
                User theUser = new UserDAO().Retrieve(UserID);

                IList<LicenseGeneration> licenseGeneration = new LicenseGenerationSystem().GetBUser(UserID, TheLicenceType);
                if (licenseGeneration != null && licenseGeneration.Count > 0)
                {
                    usedLicense = licenseGeneration.Sum(x => x.NoOfUsedLicense);
                    approvedLicense = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfLicenseApproved));
                }
                if (usedLicense >= approvedLicense)
                {
                    LicenseGeneration theLicenseToGenerate = new LicenseGeneration();
                    theLicenseToGenerate.NoOfLicense = NumofLicense;
                    theLicenseToGenerate.TheLicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), Convert.ToString(TheLicenceType));
                    theLicenseToGenerate.DateRequested = DateTime.Now;
                    theLicenseToGenerate.RequestingUser = theUser;
                    theLicenseToGenerate.TheInstitution = theUser.TheInstitution;
                    //send email to pajuno            
                    Email logEmail = new Email();

                    logEmail.ToAddress = System.Configuration.ConfigurationManager.AppSettings["PajunoEmail"];
                    logEmail.Title = string.Format("License Request From {0} ", theLicenseToGenerate.RequestingUser.FullName);
                    logEmail.Body = string.Format("A license request from {0} by {1}:<br/>", theLicenseToGenerate.RequestingUser.TheInstitution.Name, theLicenseToGenerate.RequestingUser.UserName);
                    logEmail.Body += "<b>Institution Name:</b>" + theLicenseToGenerate.RequestingUser.TheInstitution.Name + "<br/>";
                    logEmail.Body += "<b>Institution Code:</b>" + theLicenseToGenerate.RequestingUser.TheInstitution.Code + "<br/>";
                    logEmail.Body += "<b>No of License Code:</b>" + theLicenseToGenerate.NoOfLicense + "<br/>";
                    logEmail.Body += "<b>License Type:</b>" + theLicenseToGenerate.TheLicenseType.ToString() + "<br/>";
                    logEmail.Comments = "";
                    new EmailSystem().LogEmail(logEmail);

                    bool saved = new LicenseGenerationSystem().SaveLicenseGeneration(theLicenseToGenerate);
                    if (saved)
                    {
                        new LicenseGenerationDAO().CommitChanges();
                        result = true;
                    }
                    else
                        result = false;

                }
                else
                {
                    throw new Exception("You have not exhausted your license");                    
                }
            }
            else
            {
               throw new Exception("Your Session has timed out. Kindly login in again.");
            }
            return result;
        }

        public string License(string token, LicenseType TheLicenseType)
        {
            string License = "";
            try
            {                
                var currentUser = HttpContext.Current.Session["userID"];
                long userdID = Convert.ToInt64(currentUser);
                
                IList<LicenseGeneration> licenseGeneration = new List<LicenseGeneration>();
                int totalLicenseApproved = 0;
                int totalLicenseUsed = 0;

                if (currentUser != null)
                {

                    licenseGeneration = new LicenseGenerationSystem().GetBUser(userdID, TheLicenseType);
                    
                    if (licenseGeneration != null && licenseGeneration.Count > 0)
                    {
                        totalLicenseApproved = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfLicenseApproved));
                        totalLicenseUsed = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfUsedLicense));
                    }

                }
                else
                {
                    throw new Exception("Your session has timed out. Kindly logout and login again.");
                }

                EvoPaj.Logic.PrintUtilityServiceReference.LicenseData data = new EvoPaj.Logic.PrintUtilityServiceReference.LicenseData();
                
                if ((totalLicenseApproved - totalLicenseUsed) >= 0 && licenseGeneration != null)
                {
                    string licensetype = ((int)TheLicenseType).ToString();
                    string duration = Convert.ToString(licenseGeneration.OrderByDescending(x => x.ID).FirstOrDefault().LicenseDuration);
                    
                    data = new LicenseSystem().GetLicense(token, licensetype, duration);
                    
                    License = data.LicenseField;

                    LicenseGeneration toUpdate = licenseGeneration.OrderByDescending(x => x.ID).FirstOrDefault();
                    if (toUpdate != null)
                    {
                        toUpdate.NoOfUsedLicense += 1;
                        new LicenseGenerationSystem().UpdateLicenseGeneration(toUpdate);
                        new LicenseGenerationDAO().CommitChanges();
                    }
                }
                else
                {
                    throw new Exception("You have exhausted your current license for this license type");
                }

                return License;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public string ServiceCode(string wrongServiceCode)
        {
            string RightServiceCode = "";
            try
            {
                EvoPaj.Logic.PrintUtilityServiceReference.LicenseData data = new EvoPaj.Logic.PrintUtilityServiceReference.LicenseData();
                data = new LicenseSystem().GetRightCode(wrongServiceCode);
                RightServiceCode = data.rightCode;

                return RightServiceCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RightCode(string wrongCode)
        {
            string result = "";

            try
            {
                EvoPaj.Logic.PrintUtilityServiceReference.LicenseData data = new EvoPaj.Logic.PrintUtilityServiceReference.LicenseData();
                data = new LicenseSystem().GetRightCode(wrongCode);
                result = string.Format("RIGHT CODE: {0} ADMIN PASSWORD: {1}", data.rightCode, data.AdminPassword);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
