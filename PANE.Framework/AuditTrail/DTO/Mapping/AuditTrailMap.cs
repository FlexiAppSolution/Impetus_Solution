using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;
using PANE.Framework.Approval.DTO;

namespace PANE.Framework.AuditTrail.DTO.Mapping
{
    public class AuditTrailMap : DataObjectMap<AuditTrail>
    {
        public AuditTrailMap()
        {
            Map(x => x.Action);
            Map(x => x.ActionDate);
            Map(x => x.DataAfter);
            Map(x => x.DataBefore);
            Map(x => x.DataType);
            Map(x => x.Data);
            Map(x => x.ObjectID);
            Map(x => x.ClientIPAddress);
            Map(x => x.ClientName);
            Map(x => x.SubjectIdentifier);
            Map(x => x.UserCategory);
            Map(x => x.InstitutionID);
            Map(x => x.UserID);
        }
    }
}
