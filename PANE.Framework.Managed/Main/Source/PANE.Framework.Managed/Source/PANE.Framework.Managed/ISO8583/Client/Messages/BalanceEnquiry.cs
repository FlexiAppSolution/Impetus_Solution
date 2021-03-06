using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.ISO8583.DTO;
using PANE.Framework.Managed.ISO8583.Utility;
using PANE.Framework.Managed.ISO8583.Client.Configuration;
using PANE.Framework.Managed.Utility;

namespace PANE.Framework.Managed.ISO8583.Client.Messages
{
    public class BalanceEnquiry : FinancialMessage
    {
        public BalanceEnquiry(CardAcceptor cardAcceptor, Account acct, CardDetails theCard, string transactionID, bool isRepeat)
            : base(cardAcceptor, transactionID, TransactionType.BalanceEnquiry, theCard,acct.Type, "00", new Amount(0, "566", AmountType.AvailableBalance), isRepeat)
        {
            this.Fields.Add(FieldNos.F102_Account1, acct.Number);

        }
    }
}
