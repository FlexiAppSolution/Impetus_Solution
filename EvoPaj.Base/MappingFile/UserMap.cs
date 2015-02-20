using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace EvoPaj.Base.MappingFile
{
    public class UserMap : DataObjectMap<User>
    {
        public UserMap()
        {
            Map(x => x.Address);
            Map(x => x.CreationDate);
            Map(x => x.Email);
            Map(x => x.EmployeeNumber);
            Map(x => x.FirstLogin);
            Map(x => x.FirstName);
            Map(x => x.LastLoginDate);
            Map(x => x.LastName);
            Map(x => x.Name);
            Map(x => x.Password);
            Map(x => x.PasswordExpirationDate);
            Map(x => x.PhoneNumber);
            Map(x => x.Status);
            Map(x => x.UserName);
            References<Role>(x => x.TheRole).Column("RoleID");
            References<Institution>(x => x.TheInstitution).Column("InstitutionID");
            Map(x => x.AllocatedLicense);
            Map(x => x.AllocatedLicenseDuration);
            Map(x => x.UsedLicense);
            Map(x => x.UserInstitutionID);
        }

    }
}
