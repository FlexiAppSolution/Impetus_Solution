using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvoPaj.Base.ASMX
{
    public class CodeResolution
    {
        public virtual string ErrorCode { set; get; }
        public virtual string ErrorResolution { set; get; }
        public virtual string ErrorAdminPassword { set; get; }
    }
}
