using System;
using System.Collections.Generic;
using System.Text;
using PANE.Framework.Managed.ISO8583.Client.Utility;
using PANE.Framework.Managed.ISO8583.DTO;
using PANE.Framework.Managed.ISO8583.Utility;

namespace PANE.Framework.Managed.ISO8583.Client.DTO
{
    public class FundsTransferResponse : MessageResponse
    {
        public FundsTransferResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
            if (responseMessage.Fields.Contains(4))
            {
                this._amount = new Amount(Convert.ToInt64(responseMessage.Fields[4].Value), responseMessage.Fields[49].Value.ToString(), AmountType.Approved);
            }
        }

        private Amount _amount;

        public Amount Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
