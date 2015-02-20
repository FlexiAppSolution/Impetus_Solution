using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class RoleMap : DataObjectMap<Role>
    {
        public RoleMap()
        {
            Map(x => x.Name);                       
        }
    }
}
