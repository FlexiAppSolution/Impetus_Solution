using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvoPaj.Base
{
    public class LicenseData
    {
        public LicenseData()
        {
        }
        public string errorCode { set; get; }
        public string AdminPassword { set; get; }
        public string token { set; get; }
        public string rightCode { set; get; }
        public string wrongserviceCode { set; get; }
        public string rightserviceCode { set; get; }
        public string LicenseField { set; get; }
    }
}
