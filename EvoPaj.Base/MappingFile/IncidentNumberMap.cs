using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;
using EvoPaj.Base.Entities;

namespace EvoPaj.Base.MappingFile
{
    public class IncidentNumberMap : DataObjectMap<IncidentNumber>
    {
        public IncidentNumberMap()
        {
            Map(x => x.Number);            
        }
    }
}
