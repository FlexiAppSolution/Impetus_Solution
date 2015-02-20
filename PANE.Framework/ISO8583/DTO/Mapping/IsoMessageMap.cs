using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO.Mapping;

namespace PANE.Framework.ISO8583.DTO.Mapping
{
    public class IsoMessageMap : DataObjectMap<IsoMessage>
    {
        public IsoMessageMap()
        {
            Map(x => x.Type);
            Map(x => x.Sender);
            Map(x => x.Receiver);
            Map(x => x.Message);
            Map(x => x.ResponseMessage);
            Map(x => x.RequestDate);
            Map(x => x.ResponseDate);
            Map(x => x.STAN);
            Map(x => x.SenderIP);
            Map(x => x.ReceiverIP);
            Map(x => x.OriginalDataElements);
            Map(x => x.CardAcceptorID);
            Map(x => x.TerminalID);
            Map(x => x.CardPAN);
            Map(x => x.Amount);
            Map(x => x.TransactionTypeCode);
        }
    }
}
