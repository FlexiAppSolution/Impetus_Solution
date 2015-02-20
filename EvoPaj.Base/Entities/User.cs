using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace EvoPaj.Base
{
    public class User :DataObject, IEntity
    {
        private Institution _institution = new Institution();
        public User()
        {

        }
        public virtual System.String FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }        
        public virtual System.String LastName
        {
            set;
            get;
        }
        public virtual System.String FirstName
        {
            set;
            get;
        }
        public virtual System.String EmployeeNumber
        {
            set;
            get;
        }               
        public virtual System.String UserName
        {
            set;
            get;
        }
        public virtual System.String Password
        {
            set;
            get;
        }        
        public virtual System.DateTime? LastLoginDate
        {
            set;
            get;
        }        
        public virtual System.DateTime? CreationDate
        {
            set;
            get;
        }       
        public virtual System.String Email
        {
            set;
            get;
        }       
        public virtual System.String PhoneNumber
        {
            set;
            get;
        }
        public virtual Institution TheInstitution
        {
            set { _institution = value; }
            get { return _institution; }
        }
        public virtual long UserInstitutionID
        {
            set;
            get;
        }
        public virtual Role TheRole
        {
            set;
            get;
        }    
        public virtual System.String Address
        {
            set;
            get;
        }
        public virtual UserRole UserRole
        {
            set;
            get;
        }
        public virtual DateTime ? PasswordExpirationDate
        {
            get;
            set;
        }
        public virtual bool FirstLogin { set; get; }
        #region IUser Members
        public virtual UserRole Role
        {
            get;
            set;
        } 
        public virtual UserStatus Status
        {
            get;
            set;

        }
        public virtual int AllocatedLicense
        {
            set;
            get;
        }
        public virtual int AllocatedLicenseDuration
        {
            set;
            get;
        }
        public virtual int UsedLicense
        {
            set;
            get;
        }




        #endregion

        #region IEntity Members
        public virtual string AllName
        {
            get
            {
                return this.FullName;
            }
        }
        #endregion
    }
}
