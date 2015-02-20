using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.ISO8583.DTO;
using PANE.Framework.Managed.ISO8583.Utility;
using PANE.Framework.Managed.ISO8583.Client.Configuration;
using PANE.Framework.Managed.Utility;

namespace PANE.Framework.Managed.ISO8583.Client.Messages
{
    internal class HotListCard : AdministrationMessage
    {
        public HotListCard(CardAcceptor cardAcceptor, Account acct, CardDetails theCard, string messageReasonCode,string transactionID, bool isRepeat)
            : base(cardAcceptor, transactionID, TransactionType.HotListCard, theCard,acct.Type, AccountType.Default, isRepeat)
        {
            this.Fields.Add(FieldNos.F102_Account1, acct.Number);
            this.Fields.Add(FieldNos.F56_MessageReasonCode, messageReasonCode);

        }
    }
}
