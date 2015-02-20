using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Approval.DTO
{
    public enum ApprovalStatus
    {
        Pending = 1,
        Cancelled = 2,
        Declined = 3,
        Approved = 4,
        Consumated = 5
    }
}
