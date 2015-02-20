using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvoPaj.Base.Utility
{
    public enum EmailStatus
    {
        Pending = 0,
        Sent = 1,
        Failed = 2
    }
    public enum LicenseType
    {
        PC=1,
        Printer=2
    }
    public enum LicenseGenerationApproval
    {
        Pending =0,
        Approved=1,
        Declined =2

    }
    public enum CaseType
    {
        New =1,
        Existing =2,
        All=3
    }
}
