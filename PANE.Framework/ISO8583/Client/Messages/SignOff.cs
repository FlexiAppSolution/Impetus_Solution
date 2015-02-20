using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.ISO8583.DTO;
using PANE.Framework.ISO8583.Utility;
using PANE.Framework.Utility;

namespace PANE.Framework.ISO8583.Client.Messages
{
    public class SignOff : Message
    {
        public SignOff(CardAcceptor terminal, string transactionID, bool isRepeat)
            : base(800, transactionID, isRepeat)
        {
            this.Fields.Add(FieldNos.F70_NetworkMgtInfoCode, "002");
        }
    }
}
