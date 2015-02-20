using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Functions.DTO;
using PANE.Framework.DTO;


namespace PANE.Framework.Functions.DTO
{
    [Serializable]
    public class Mail : DataObject
    {
       

        public virtual string  ToAddress { get; set; }
        public virtual string  MailMessage { get; set; }
        public virtual DataType DataType { get; set; }///user or merchant or  transaction
        public virtual long DataID { get; set; }///The ID of the data to be sent
        public virtual int TryCount { get; set; }
        public virtual MailStatus MailStatus { get; set; }
        public virtual DateTime DateCreated{get;set;}
        public virtual DateTime DateSent{get;set;}  
        public virtual string  subject {get;set;}
        public virtual long ValidDays { get; set; }/// any mail tht has existed for longet han this numebr of days will not be sent

      
    }
}

