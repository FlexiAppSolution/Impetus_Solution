using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.AuditTrail.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public sealed class LoggableAttribute : Attribute
    {
        private string _loggedName = string.Empty;
        public LoggableAttribute()
        {

        }

        public LoggableAttribute(string logName)
        {
            _loggedName = logName;
        }

        public string LoggedName
        {
            get
            {
                return this._loggedName;
            }
        }

        //Should be used only on class properties that need you to retrieve only its name property.
        public bool NeedOnlyNameProperty { get; set; }

        //Is only needed if the NeedOnlyNameProperty is set to true.
        public string ClassNameIdentifier { get; set; }

        public bool IncludeInApprovalDetailList { get; set; }



    }
}
