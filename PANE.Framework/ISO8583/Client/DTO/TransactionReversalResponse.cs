using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.ISO8583.Client.Utility;
using PANE.Framework.Utility;
using PANE.Framework.ISO8583.DTO;
using PANE.Framework.ISO8583.Utility;

namespace PANE.Framework.ISO8583.Client.DTO
{
    public class TransactionReversalResponse : MessageResponse
    {
        public TransactionReversalResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
            if (responseMessage.Fields.Contains(FieldNos.F4_TransAmount))
            {
                this._amount = new Amount(Convert.ToInt64(responseMessage.Fields[FieldNos.F4_TransAmount].Value), responseMessage.Fields[FieldNos.F49_TransCurrencyCode].Value.ToString(), AmountType.Approved);
            }
            if (responseMessage.Fields.Contains(FieldNos.F37_RetrievalReference))
            {
                this._RetreivalReference = responseMessage.Fields[FieldNos.F37_RetrievalReference].Value.ToString();
            }
        }

        private string _RetreivalReference;

        public string RetreivalReference
        {
            get { return _RetreivalReference; }
            set { _RetreivalReference = value; }
        }

        private Amount _amount;

        public Amount Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
