using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace PANE.Framework.Functions.DTO.Mapping
{
    public class UserRoleMap : DataObjectMap<UserRole>
    {
        public UserRoleMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.InstitutionID);
            Map(x => x.UserCategory);
            Map(x => x.Scope);
            Map(x => x.Status);
            Map(x => x.IsDefault);
            Map(x => x.AssignedFunctions);
            HasMany<UserRoleFunction>(x => x.TheUserRoleFunctions).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
