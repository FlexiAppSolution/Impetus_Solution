using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj;
using PANE.Framework.DTO;
using EvoPaj.Base.Utility;


namespace EvoPaj.Base
{
    public class Case: DataObject
    {
        public Case()
        {

        }
        private string _incidentNumber = "";
        
        public virtual long CaseNumber { get; set; }
        public virtual string IncidentNumber { set { _incidentNumber = value; } get { return CaseNumber.ToString().PadLeft(6, '0'); } }
        public virtual string Name { set; get; }
        public virtual string Description { set; get; }
        public virtual string Resolution { set; get; }
        public virtual string ErrorCode { set; get; }
        public virtual string ErrorResolution { set; get; }        
        public virtual DateTime? DateLogged { get; set; }
        public virtual DateTime ?DateResolved { set; get; }
        public virtual Institution LoggingInstition { set; get; }
        public virtual User LoggingUser { set; get; }
        public virtual CaseType TheCaseType { set; get; }


              


    }
}
