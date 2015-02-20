using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace PANE.Framework.Functions.DTO.Mapping
{
    public class UserRoleFunctionMap : DataObjectMap<UserRoleFunction>
    {
        public UserRoleFunctionMap()
        {
            References<Function>(x => x.TheFunction);
            References<UserRole>(x => x.TheUserRole);
            HasMany<UserRoleFunctionSubRole>(x => x.SubUserRoles).Cascade.AllDeleteOrphan().Inverse().BatchSize(50).Fetch.Join();
        }
    }
}
