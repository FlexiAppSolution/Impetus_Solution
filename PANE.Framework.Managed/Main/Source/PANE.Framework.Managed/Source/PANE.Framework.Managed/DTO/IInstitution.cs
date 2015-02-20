using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.Utility;
using PANE.Framework.Managed.Functions.DTO;

namespace PANE.Framework.Managed.DTO
{
    public interface IInstitution: IDataObject
    {
        long ID { get; set; }
        int InstitutionCode { get; set; }
        string Name { get; set; }
        //Status Status { get; set; }
        //IUser TheUser { get; set; }

        string Code { get; set; }

        string LocalConnectionString { get; set; }
        
        string RemoteConnectionString { get; set; }
    }
}
