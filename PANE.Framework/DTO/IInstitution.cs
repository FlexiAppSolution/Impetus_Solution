using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Utility;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.DTO
{
    public interface IInstitution
    {
        long ID { get; set; }
        string InstitutionCode { get; set; }
        string Name { get; set; }
        Status Status { get; set; }
        IUser TheUser { get; set; }
        UserCategory TheUserCategory { get; }
        bool IsDefault { get; set; }
    }
}
