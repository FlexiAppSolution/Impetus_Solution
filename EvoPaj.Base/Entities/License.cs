using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace EvoPaj.Base
{
    public class License : DataObject, IEntity
    {
        private Institution _institution = new Institution();        

        public License(){}

        public virtual System.String FileName { get; set; }

        public virtual System.String FilePath { get; set; }

        public virtual System.Int64 NumberOfProposedLicense { get; set; }        

        public virtual Institution TheInstitution{ set { _institution = value; } get { return _institution; }}

        public virtual System.String Token { get; set; }

        public virtual System.String ServiceCode { get; set; }

        public virtual System.String ErrorCode { get; set; }

        public virtual System.String RightCode { get; set; }

        public virtual System.String LicenseField { get; set; }

        public virtual System.String AdminPassword { get; set; }
    }
}
