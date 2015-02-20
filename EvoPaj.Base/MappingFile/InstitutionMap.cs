using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class InstitutionMap : DataObjectMap <Institution>
    {

        public InstitutionMap()
        {
            Map(x => x.Name);
            Map(x => x.Website);
            Map(x => x.InstitutionName);
            Map(x => x.Code);
            Map(x => x.Address);
            Map(x => x.ContactEmail);
            Map(x => x.ContactName);
            Map(x => x.Logo);
            Map(x => x.ContactPhoneNumber);            
            Map(x => x.Status);
            
        }
    }
}
