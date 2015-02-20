using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.DTO.Mapping
{
    public class HelpMap : DataObjectMap<Help>
    {
        public HelpMap()
        {
            
            Map(x => x.DemoUrl );
            Map(x => x.Description );
            Map(x => x.Name );
            Map(x => x.UserGuideUrl );
            
        }
    }
}
