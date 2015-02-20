using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Functions.DTO
{
   
    
       public enum MailSendStatus
       {
           Successful = 1,
           Failed = 2
       }
       public enum MailStatus
       {
           Sent,
           NotSent,
           Failed

       }
       public enum DataType
       {
           Card,
           Approval,
           Entity
       }
}
