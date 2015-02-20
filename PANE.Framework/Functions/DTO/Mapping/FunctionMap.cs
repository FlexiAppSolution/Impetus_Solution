using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;
using PANE.Framework.Approval.DTO;

namespace PANE.Framework.Functions.DTO.Mapping
{
    public class FunctionMap : DataObjectMap<Function>
    {
        public FunctionMap()
        {
            Map(x => x.Name);
            Map(x => x.UserCategory);
            Map(x => x.Description);
            Map(x => x.RoleName);
            Map(x => x.HasSubRoles);
            Map(x => x.Type);
            References<Function>(x => x.ParentFunction);
            References<Help>(x => x.Help);
            Map(x => x.IsDefault);
            Map(x => x.IsApprovable);
            HasMany<ApprovalConfiguration>(x => x.TheApprovalConfigurations).KeyColumns.Add("MakerRoleID");
            HasMany<Function>(x => x.TheFunctions).KeyColumns.Add("ParentFunctionID");
            Map(x => x.Status);
        }
    }
}
