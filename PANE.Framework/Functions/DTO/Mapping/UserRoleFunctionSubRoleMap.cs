using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace PANE.Framework.Functions.DTO.Mapping
{
    public class UserRoleFunctionSubRoleMap : DataObjectMap<UserRoleFunctionSubRole>
    {
        public UserRoleFunctionSubRoleMap()
        {
            References<UserRoleFunction>(x => x.TheUserRoleFunction).Fetch.Join();
            References<UserRole>(x => x.TheSubUserRole).Fetch.Join();
        }
    }
}
