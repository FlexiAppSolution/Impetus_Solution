using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PANE.Framework.ISO8583.Client.DTO
{
    [DataContract]
    public class ReversalResponse : MessageResponse
    {
        public ReversalResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {

        }
    }
}
