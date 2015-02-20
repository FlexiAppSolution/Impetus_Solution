using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;

namespace PANE.Framework.Managed.ISO8583.DTO
{
    public class TransAmount : DataObject
    {
        public string Amount { get; set; }
        public string Sign { get; set; }
        public string CurrencyCode { get; set; }
    }
}
