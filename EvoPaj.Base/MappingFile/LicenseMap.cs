using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class LicenseMap : DataObjectMap<License>
    {
        public LicenseMap()
        {
            Map(x => x.FileName);
            Map(x => x.FilePath);
            Map(x => x.NumberOfProposedLicense);            
            References<Institution>(x => x.TheInstitution).Column("InstitutionID");
        }
    }
}
