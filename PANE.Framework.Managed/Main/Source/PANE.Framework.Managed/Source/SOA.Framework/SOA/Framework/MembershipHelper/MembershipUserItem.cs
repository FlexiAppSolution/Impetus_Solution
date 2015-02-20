namespace SOA.Framework.MembershipHelper
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Web.Security;
using PANE.Framework.Managed.Functions.DTO;
    using System.Reflection;
using SOA.Framework.MembershipHelper;

    [DataContract]
    public class MembershipUserItem : IUser
    {
        public MembershipUserItem(PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser theUser)
        {
            if (theUser == null)
            {
                throw new ArgumentNullException("theUser");
            }
            

            foreach (PropertyInfo pInfo in typeof(IUser).GetProperties())
            {
                if (pInfo.CanWrite)
                {
                    pInfo.SetValue(this, pInfo.GetValue(theUser.UserDetails, null), null);
                    //theUser.UserDetails
                }
            }
            this.UserName = theUser.UserName;
            this.Key = theUser.ProviderUserKey;
            this.Comment = theUser.Comment;
            this.Email = theUser.Email;
            this.IsApproved = theUser.IsApproved;
            this.IsLockedOut = theUser.IsLockedOut;
            this.IsOnline = theUser.IsOnline;
            // this.UserDetails = theUser.UserDetails;
            this.ID = theUser.UserDetails.ID;
            this.MFBCode = theUser.UserDetails.MFBCode;
            this.UserID = theUser.UserDetails.UserID;
            this.Role = theUser.UserDetails.Role;
            this.BranchID = theUser.UserDetails.BranchID;
            this.IsSuperUser = theUser.UserDetails.IsSuperUser;
            //theUser.UserDetails.Branch.Name;

            MembershipBranchItem branchItem = new MembershipBranchItem();
            branchItem.Address = theUser.UserDetails.Branch.Address;
            branchItem.Code = theUser.UserDetails.Branch.Code;
            branchItem.ID = theUser.UserDetails.Branch.ID;
            branchItem.MFBCode = theUser.UserDetails.Branch.MFBCode;
            branchItem.Name = theUser.UserDetails.Branch.Name;
           // branchItem.GefuFilePath = theUser.UserDetails.Branch.GefuFilePath;
            branchItem.Status = theUser.UserDetails.Branch.Status;
            
            this.MembershipBranchItem = branchItem;
            //foreach (PropertyInfo pInfo in typeof(IBranch).GetProperties())
            //{
            //    if (pInfo.CanWrite)
            //    {
            //        pInfo.SetValue(this.MembershipBranchItem, pInfo.GetValue(theUser.UserDetails.Branch, null), null);
            //    }
            //}


            //this.MembershipBranchItem.Address = theUser.UserDetails.Branch.Address;

        }
        //[DataMember]
       // public IUser UserDetails { get; set; }
        [DataMember]
        public long BranchID { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public MembershipBranchItem MembershipBranchItem { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }

        [DataMember]
        public bool IsLockedOut { get; set; }

        [DataMember]
        public bool IsOnline { get; set; }

        [DataMember]
        public object Key { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public bool IsSuperUser { get; set; }


        #region IUser Members
        [System.Xml.Serialization.XmlIgnore]
        public IBranch Branch { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public UserRole Role { get; set; }

        [DataMember]
        public UserStatus Status { get; set; }

        [DataMember]
        public DateTime LastLoginDate { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public int FailedPasswordAttemptCount { get; set; }

        [DataMember]
        public bool DoesntNeedAudit { get; set; }

        #endregion

        #region IDataObject Members
        [DataMember]
        public long ID
        {
            get;
            set;
        }
        [DataMember]
        public string MFBCode
        {
            get;
            set;
        }

        #endregion
    }
}

