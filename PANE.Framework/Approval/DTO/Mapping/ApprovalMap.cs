using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.Approval.DTO.Mapping
{
    public class ApprovalMap : DataObjectMap<Approval>
    {
        public ApprovalMap()
        {
            Map(x => x.ActionDate);
            Map(x => x.DateRequested);
            Map(x => x.DataType);
            Map(x => x.ObjectID);
            Map(x => x.UserID);
            Map(x => x.SubUserRoleID);
            Map(x => x.ClientIPAddress);
            Map(x => x.ClientName);
            Map(x => x.DataAfter);
            Map(x => x.DataBefore);
            Map(x => x.Status);
            Map(x => x.EntityName);
            Map(x => x.ApproverLastName);
            Map(x => x.ApproverOtherNames);
            Map(x => x.InitiatorLastName);
            Map(x => x.InitiatorOtherNames);
            Map(x => x.InitiatorEmail);
            Map(x => x.ApprovalNotificationStatus);
            References<Function>(x => x.ApprovalFunction);
        }
    }
}
