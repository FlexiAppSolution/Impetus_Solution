using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using EvoPaj.Base.Utility;

namespace EvoPaj.Base.Entities
{
    public class LicenseGeneration : DataObject
    {
        public LicenseGeneration()
        {
        }
        public virtual int NoOfLicense { set; get; }
        public virtual string LicenseDuration { set; get; }
        public virtual string NoOfLicenseApproved { set; get; }
        public virtual int NoOfUsedLicense { set; get; }
        public virtual LicenseType TheLicenseType { set; get; }
        public virtual User RequestingUser { set; get; }
        public virtual User ApprovingUser { set; get; }
        public virtual DateTime? DateRequested { set; get; }
        public virtual DateTime ? DateApproved {set; get; }
        public virtual bool RequestEmailSent { set; get; }
        public virtual bool ApprovedEmailSent { set; get; }
        public virtual LicenseGenerationApproval Approval { set; get; }
        public virtual Institution TheInstitution { set; get; }
        
        
    }
}
