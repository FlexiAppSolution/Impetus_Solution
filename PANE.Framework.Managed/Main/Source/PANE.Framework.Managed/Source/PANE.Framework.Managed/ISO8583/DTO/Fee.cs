using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;

namespace PANE.Framework.Managed.ISO8583.DTO
{
    public class Fee : TransAmount
    {
        public string FeeDescription { get; set; }
    }
}
