using System;
using System.Collections.Generic;
using System.Text;

namespace PANE.Framework.ISO8583.Client.DTO
{
    public class HotListCardResponse : MessageResponse
    {
        public HotListCardResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
           
        }
    }
}
