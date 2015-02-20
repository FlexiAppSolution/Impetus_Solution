using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;

namespace PANE.Framework.Managed.Functions.DTO
{
    public interface IBranch : IDataObject
    {
        System.String Name { get; set; }

        int Code { get; set; }

        string Address { get; set; }

        Status Status { get; set; }

        
        long RegionID { get; set; }

       

    }
}
