using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.Functions.DTO;

namespace PANE.Framework.Managed.AuditTrail.DTO
{
    public interface IAuditable
    {
        IUser AuditableUser { get; set; }
    }
}
