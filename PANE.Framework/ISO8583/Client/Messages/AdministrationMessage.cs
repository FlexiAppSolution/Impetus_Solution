using System;
using System.Collections.Generic;
using System.Text;
using Trx.Messaging.Iso8583;
using Trx.Messaging;
using PANE.Framework.ISO8583.DTO;
using PANE.Framework.ISO8583.Utility;
using PANE.Framework.ISO8583.Client.Configuration;
using PANE.Framework.Utility;

namespace PANE.Framework.ISO8583.Client.Messages
{
    internal abstract class AdministrationMessage : CardBasedMessage
    {

        public AdministrationMessage(CardAcceptor cardAcceptor, string transactionID, TransactionType transType, CardDetails theCard, string fromAccountType, string toAccountType, bool isRepeat)
            : base(600, cardAcceptor, theCard, transactionID, isRepeat)
        {
            this.Fields.Add(FieldNos.F3_ProcCode, string.Format("{0}{1}{2}", Convert.ToString((int)transType).PadLeft(2, '0'), fromAccountType, toAccountType));
        }

    }
}
