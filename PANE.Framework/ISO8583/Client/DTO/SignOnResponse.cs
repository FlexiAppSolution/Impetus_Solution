using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PANE.Framework.ISO8583.Client.DTO
{
    [DataContract]
    public class SignOnResponse : MessageResponse
    {
        public SignOnResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
           
        }
    }
}
