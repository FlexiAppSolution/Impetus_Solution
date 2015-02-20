using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.ISO8583.DTO;
using PANE.Framework.ISO8583.Utility;
using PANE.Framework.ISO8583.Client.Configuration;
using PANE.Framework.Utility;

namespace PANE.Framework.ISO8583.Client.Messages
{
    public class FundsTransfer : FinancialMessage
    {
        public FundsTransfer(CardAcceptor cardAcceptor, Account fromAccount, Account toAccount, Amount transferAmount, CardDetails theCard, string transactionID, bool isRepeat)
            : base(cardAcceptor, transactionID, TransactionType.AccountsTransfer, theCard, fromAccount.Type, toAccount.Type, transferAmount, isRepeat)
        {
            this.Fields.Add(FieldNos.F102_Account1, fromAccount.Number);
            this.Fields.Add(FieldNos.F103_Account2, toAccount.Number);
        }
    }
}
