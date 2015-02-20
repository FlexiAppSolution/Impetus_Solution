using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PANE.Framework.DTO.Mapping;

namespace PANE.Framework.Functions.DTO.Mapping
{
    public class MailMap : DataObjectMap<Mail>
    {
        public MailMap()
        {
            Map(x => x.ToAddress  );
            Map(x => x.MailMessage );
            Map(x => x.DataType );
            Map(x => x.DataID);
            Map(x => x.MailStatus);
            Map(x => x.TryCount);
            Map(x => x.DateCreated);
            Map(x => x.DateSent);
            Map(x => x.subject);
            Map(x => x.ValidDays);
        }
    }
}
