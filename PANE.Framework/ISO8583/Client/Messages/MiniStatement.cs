using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.ISO8583.DTO;
using PANE.Framework.ISO8583.Utility;
using PANE.Framework.ISO8583.Client.Configuration;
using PANE.Framework.Utility;

namespace PANE.Framework.ISO8583.Client.Messages
{
    public class MiniStatement : AuthorizationMessage
    {
        public MiniStatement(CardAcceptor cardAcceptor, Account acct, CardDetails theCard, string transactionID, bool isRepeat)
            : base(cardAcceptor, transactionID, TransactionType.MiniStatementInquiry, theCard,acct.Type, "00", new Amount(0, "566", AmountType.AvailableBalance), isRepeat)
        {
            this.Fields.Add(FieldNos.F102_Account1, acct.Number);

        }
    }
}
