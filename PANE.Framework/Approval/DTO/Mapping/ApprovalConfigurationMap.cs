using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.Approval.DTO.Mapping
{
    public class ApprovalConfigurationMap : DataObjectMap<ApprovalConfiguration>
    {
        public ApprovalConfigurationMap()
        {
            Map(x => x.IsApprovable);
            Map(x => x.InstitutionID);
            References<Function>(x => x.Data);
            References<Function>(x => x.MakerRole);
            References<UserRole>(x => x.ApprovingRole);
            References<UserRole>(x => x.SubUserRole);
        }
    }
}
