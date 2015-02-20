using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class DownloadedLicenseMap : DataObjectMap<DownloadedLicense>
    {
        public DownloadedLicenseMap()
        {
            Map(x => x.NumberOfUsedLicense);
            Map(x => x.DateDownloaded);                        
            References<License>(x => x.TheLicense).Column("LicenseID");
            References<User>(x => x.TheUser).Column("UserID");
        }
    }
}
