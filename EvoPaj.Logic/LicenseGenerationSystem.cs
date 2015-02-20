using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base.Entities;
using EvoPaj.Data;
using EvoPaj.Base.Utility;
using System.Web;
using System.Data;

namespace EvoPaj.Logic
{
    public class LicenseGenerationSystem : CoreServices<LicenseGeneration, long>
    {
        public LicenseGenerationSystem()
        {

        }
       
        public bool SaveLicenseGeneration(LicenseGeneration theLicense)
        {
            bool result = false;
            try
            {
                //new LicenseGenerationDAO().Save(theLicense);
                int outcome = InsertLicense(theLicense);
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
        
        public bool UpdateLicenseGeneration(LicenseGeneration theLicense)
        {
            bool result = false;
            try
            {
                //new LicenseGenerationDAO().Update(theLicense);
                int outcome = UpdateLicense(theLicense);
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
        
        public bool DeleteLicenseGeneration(LicenseGeneration theLicense)
        {
            bool result = false;
            try
            {
                new LicenseGenerationDAO().Delete(theLicense);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }

        public static int InsertLicense(LicenseGeneration somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);
           
            db.AddParamAndValue("@NoOfLicense", somebody.NoOfLicense);
            db.AddParamAndValue("@NoOfLicenseApproved", somebody.NoOfLicenseApproved);
            db.AddParamAndValue("@NoOfLicenseUsed", somebody.NoOfUsedLicense);
            if (somebody.TheLicenseType == LicenseType.PC)
                db.AddParamAndValue("@TheLicenseType", "PC");
            else
                db.AddParamAndValue("@TheLicenseType", "Printer");
            db.AddParamAndValue("@DateRequested", somebody.DateRequested);
            db.AddParamAndValue("@RequestingUserID", somebody.RequestingUser.ID);
            db.AddParamAndValue("@InstitutionID", somebody.TheInstitution.ID);
            db.AddParamAndValue("@Approval", "Pending");

            int status = db.ExecuteNonQuery("sp_insert_license_request", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }

        public static int UpdateLicense(LicenseGeneration somebody)
        {            			
            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@ApprovingUserID", somebody.ApprovingUser.ID);
            if (somebody.Approval == EvoPaj.Base.Utility.LicenseGenerationApproval.Approved)
                db.AddParamAndValue("@Approval", "Approved");
            else if (somebody.Approval == EvoPaj.Base.Utility.LicenseGenerationApproval.Declined)
                db.AddParamAndValue("@Approval", "Declined");
            db.AddParamAndValue("@DateApproved", somebody.DateApproved);
            db.AddParamAndValue("@NoOfLicenseApproved", somebody.NoOfLicenseApproved);
            db.AddParamAndValue("@LicenseDuration", somebody.LicenseDuration);
            db.AddParamAndValue("@ID", somebody.ID);

            int status = db.ExecuteNonQuery("sp_update_license_request", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public LicenseGeneration RetrieveByID(long ID)
        {
            return new LicenseGenerationDAO().Retrieve(ID);
        }
        
        public IList<LicenseGeneration> SearchLicenseGeneration(DateTime? DateFrom, DateTime? DateTo, Base.Utility.LicenseType? license, long instionitutionID, string approvalStatus)
        {
            return new LicenseGenerationDAO().SearchLicenseGeneration(DateFrom, DateTo, license, instionitutionID, approvalStatus);
        }
        
        public IList<LicenseGeneration> LicenseRequestList(Base.Utility.LicenseType? license, DateTime dateFrom, DateTime dateTo, int start, int limit, out int numOfResults)
        {
            long instituionID = Convert.ToInt64(HttpContext.Current.Session["InstitutionID"]);

            return new LicenseGenerationDAO().LicenseRequestList(license, instituionID, dateFrom, dateTo, start, limit, out numOfResults);
        }
       
        public IList<LicenseGeneration> GetBUser(long p, LicenseType licenseType)
        {
            return new LicenseGenerationDAO().GetByUser(p, licenseType);
        }
    }
}
