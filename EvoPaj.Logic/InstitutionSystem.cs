using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Data;


namespace EvoPaj.Logic
{
    public class InstitutionSystem : CoreServices<Institution, long>
    {
        public InstitutionSystem()
        {

        }
       
        public Institution RetrieveByID(long ID)
        {
            return new InstitutionDAO().Retrieve(ID);
        }
        
        public bool SaveInstitution(Institution theInstitution)
        {
            bool result = false;
            try
            {
                //new InstitutionDAO().Save(theInstitution);
                //result = true;
                int outcome = InsertInstitution(theInstitution);
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
       
        public bool UpdateInstitution(Institution theInstitution)
        {
            bool result = false;
            try
            {
                //new InstitutionDAO().Update(theInstitution);
                //result = true;
                int outcome = ModifyInstitution(theInstitution);
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
       
        public bool DeleteInstitution(Institution theInstitution)
        {
            bool result = false;
            try
            {
                new InstitutionDAO().Delete(theInstitution);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool UpdateInstitution(string query)
        {
            bool result = false;
            try
            {
                new InstitutionDAO().RunQuery(query);
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
                new InstitutionDAO().CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;

        }

        public static int InsertInstitution(Institution somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);
           
            db.AddParamAndValue("@InstitutionName", somebody.InstitutionName.ToUpper());
            db.AddParamAndValue("@Website", somebody.Website);
            db.AddParamAndValue("@Code", somebody.Code);
            db.AddParamAndValue("@Address", somebody.Address);
            db.AddParamAndValue("@ContactEmail", somebody.ContactEmail);
            db.AddParamAndValue("@ContactName", somebody.ContactName.ToUpper());
            db.AddParamAndValue("@ContactPhoneNumber", somebody.ContactPhoneNumber);       
            db.AddParamAndValue("@status", somebody.Status);

            int status = db.ExecuteNonQuery("sp_insert_institution", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }

        public static int ModifyInstitution(Institution somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@InstitutionName", somebody.InstitutionName.ToUpper());
            db.AddParamAndValue("@Website", somebody.Website);
            db.AddParamAndValue("@Code", somebody.Code);
            db.AddParamAndValue("@Address", somebody.Address);
            db.AddParamAndValue("@ContactEmail", somebody.ContactEmail);
            db.AddParamAndValue("@ContactName", somebody.ContactName.ToUpper());
            db.AddParamAndValue("@ContactPhoneNumber", somebody.ContactPhoneNumber);
            db.AddParamAndValue("@status", somebody.Status);
            db.AddParamAndValue("@ID", somebody.ID);

            int status = db.ExecuteNonQuery("sp_update_institution", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }

        public List<Institution> GetInstitutions()
        {
            return new InstitutionDAO().RetrieveAll();
        }

        public bool checkInstitution(Institution theInstitution, out string message)
        {
            string msg = ""; bool result = false;
            IList<Institution> ins = new InstitutionDAO().RetrieveAll();
            foreach (Institution institute in ins)
            {                
                if (theInstitution.InstitutionName == institute.InstitutionName)
                {
                    msg = "Institution with Name: "  + theInstitution.InstitutionName + " already exists.";
                    result = true;
                }
                else 
                {
                    result = false;                    
                }
            }
            message = msg;
            return result;
        }

        public bool checkInstitutionForUpdate(Institution theInstitution, out string message)
        {
            string msg = ""; bool result = false;
            IList<Institution> ins = new InstitutionDAO().GetInstitutionByCode(theInstitution);
            foreach (Institution institute in ins)
            {
                if (theInstitution.InstitutionName == institute.InstitutionName)
                {
                    result = false;
                }
                else
                {
                    msg = "Institution with Name: " + theInstitution.InstitutionName + " already exists.";
                    result = true;
                }
            }
            message = msg;
            return result;
        }

        public Institution LastInstitution()
        {
            return new InstitutionDAO().getLastInstitution();
        }

        public Institution GetInstitutionByCode(Int32 code)
        {
            return new InstitutionDAO().getInstitutionByCode(code);
        }

        public IList<Institution> SelectInstitution(Institution TheInstitution)
        {
            return new InstitutionDAO().GetInstitution(TheInstitution);
        }

        public IList<Institution> SelectInstitution(Institution TheInstitution, int start, int limit, out int numOfResults)
        {
            return new InstitutionDAO().GetInstitution(TheInstitution, start, limit, out numOfResults);
        }

        public IList<Institution> GetInstitutionByID(Institution TheInstitution)
        {
            return new InstitutionDAO().GetInstitutionByID(TheInstitution);
        } 
    }
}
