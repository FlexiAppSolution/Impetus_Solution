﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Managed.AuditTrail.Exceptions
{
    public class AuditTrailIdentifierException : Exception
    {
        public override string Message
        {
            get
            {
                return "The Specified class does not have an Audit Trail Identifier.";
            }
        }   
    }
}
