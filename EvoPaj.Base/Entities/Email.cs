using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base.Utility;
using PANE.Framework.DTO;

namespace EvoPaj.Base
{
    public class Email : DataObject
    {
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string ToAddress { get; set; }
        public virtual EmailStatus Status { get; set; }
        public virtual string Comments { get; set; }

    }
}
