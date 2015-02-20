using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class CaseMap :DataObjectMap<Case>
    {
        public CaseMap()
        {
            Map(x => x.ErrorCode);
            Map(x => x.ErrorResolution);
            Map(x => x.Name);
            Map(x => x.CaseNumber);
            Map(x => x.Description);            
            Map(x => x.DateLogged);
            Map(x => x.DateResolved);
            Map(x => x.TheCaseType);
            References<User>(x => x.LoggingUser);
            References<Institution>(x => x.LoggingInstition);
            Map(x => x.Resolution);           
        }
    }
}
