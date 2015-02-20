using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base.Entities;
using EvoPaj.Data;
using System.Data;

namespace EvoPaj.Logic
{
    public class IncidentNumberSystem : CoreServices<IncidentNumber, long>
    {
        public long getIncidentNumber()
        {
            long Number = 1;
            IncidentNumber theNumber = new IncidentNumber();
            theNumber = new IncidentNumberDAO().Retrieve(1);

            Number = theNumber.Number + 1;
            theNumber.Number = Number;

            ModifyIncidentNumber(theNumber);         

            return Number;
        }

        public static int ModifyIncidentNumber(IncidentNumber somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@Number", somebody.Number);           
            db.AddParamAndValue("@ID", somebody.ID);


            int status = db.ExecuteNonQuery("sp_update_incident_number", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
    }
}
