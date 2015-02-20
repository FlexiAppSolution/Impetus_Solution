using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace PANE.Framework.ISO8583.Client.DTO
{
    [DataContract]
    public class KeyExchangeResponse : MessageResponse
    {
        private string _SessionKey;
        private Byte[] _SessionKeyBytes;
        
        public KeyExchangeResponse(Trx.Messaging.Message responseMessage)
            : base(responseMessage)
        {
            if (responseMessage.Fields.Contains(53))
            {
                _SessionKey = responseMessage.Fields[53].Value.ToString().Substring(1,16);
                _SessionKeyBytes = (byte[])responseMessage.Fields[53].Value;
            }
        }

        [DataMember]
        public string SessionKey
        {
            get { return _SessionKey; }
            set { _SessionKey = value; }
        }

        [DataMember]
        public Byte[] SessionKeyBytes
        {
            get { return _SessionKeyBytes; }
            set { _SessionKeyBytes = value; }
        }


    }
}
