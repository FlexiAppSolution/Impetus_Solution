using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base.Entities;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class LicenseGenerationMap : DataObjectMap<LicenseGeneration>
    {
        public LicenseGenerationMap()
        {
            Map(x => x.NoOfLicense);
            Map(x => x.RequestEmailSent);
            References<User>(x => x.RequestingUser).Column("RequestingUserID");
            References<User>(x => x.ApprovingUser).Column("ApprovingUserID");
            References<Institution>(x => x.TheInstitution).Column("InstitutionID");
            Map(x => x.DateRequested);
            Map(x => x.DateApproved);
            Map(x => x.ApprovedEmailSent);
            Map(x => x.NoOfLicenseApproved);
            Map(x => x.TheLicenseType);
            Map(x => x.Approval);
            Map(x => x.LicenseDuration);
            Map(x => x.NoOfUsedLicense);

        }
    }
}
