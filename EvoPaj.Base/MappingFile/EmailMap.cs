using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class EmailMap : DataObjectMap<Email>
    {
        public EmailMap()
        {
            Map(x => x.ToAddress);
            Map(x => x.Title);
            Map(x => x.Body);
            Map(x => x.Status);
            Map(x => x.Comments);          
        }
    }
}
