using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.ISO8583.DTO;
using PANE.Framework.Managed.ISO8583.Utility;
using PANE.Framework.Managed.Utility;

namespace PANE.Framework.Managed.ISO8583.Client.Messages
{
    public class KeyExchange : Message
    {
        public KeyExchange(CardAcceptor terminal, string transactionID, bool isRepeat)
            : base(800, transactionID, isRepeat)
        {
            this.Fields.Add(FieldNos.F70_NetworkMgtInfoCode, "101");
        }
    }
}
