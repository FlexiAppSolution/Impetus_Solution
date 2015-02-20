using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace EvoPaj.Base
{
    public class Institution : DataObject
    {

        public Institution()
        {
        }
        public Institution(long id)
        {
            ID = id;
        }
        public virtual System.String InstitutionName
        {
            get;
            set;
        }       
        public virtual int Code
        {
            get;
            set;
        }

        public virtual byte[] Logo
        {
            get;
            set;
        }   
        public virtual System.String Address
        {
            get;
            set;
        }
        public virtual string ContactName { set; get; }
        public virtual string ContactPhoneNumber { set; get; }
        public virtual string ContactEmail { set; get; }        
        public virtual string Website { set; get; }          
        public virtual PANE.Framework.Functions.DTO.Status Status
        {
            get;
            set;
        }
        public override bool Equals(object obj)
        {
            return ID == ((DataObject)obj).ID;
        }
    }
}
