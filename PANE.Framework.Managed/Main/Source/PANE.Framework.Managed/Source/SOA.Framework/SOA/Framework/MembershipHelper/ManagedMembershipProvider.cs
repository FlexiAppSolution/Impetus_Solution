namespace SOA.Framework.MembershipHelper
{
    using SOA.Framework.MS;
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Runtime.InteropServices;
    using System.Web;
    using System.Web.Security;
    using PANE.Framework.Managed.Functions.DTO;

    public class ManagedMembershipProvider : MembershipProvider
    {
        private string _ApplicationName;
        private bool _isOnline;
        private int _MaxLoginAttempts = 3;
        private int _MaxOnlineUsers = -1;
        private int _MinPasswordLength = 0;
        private int _MinRequiredNonAlphanumericCharacters = 0;
        private string _Name;
        private const string SS_INSTITUTION_CODE = "::SS_INSTITUTION_CODE::";
        internal const string HC_SERVICE_USER = "::HC_SERVICE_USER::";

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser ConvertFrom(SOA.Framework.MS.MembershipUserItem theUserItem)
        {
            if (theUserItem == null)
            {
                return null;
            }
            MembershipUser mu = new MembershipUser(this.Name, theUserItem.UserName, theUserItem.UserID, theUserItem.Email, "", theUserItem.Comment, theUserItem.IsApproved, theUserItem.IsLockedOut, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
            return new PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser(mu) { UserDetails = theUserItem as IUser };
        }

        private SOA.Framework.MS.MembershipUserItem ConvertFrom(MembershipUser theUser)
        {
            if (theUser == null)
            {
                return null;
            }
            return new SOA.Framework.MS.MembershipUserItem { UserName = theUser.UserName, Key = theUser.ProviderUserKey, Email = theUser.Email, Comment = theUser.Comment, IsLockedOut = theUser.IsLockedOut, IsApproved = theUser.IsApproved, IsOnline = theUser.IsOnline };
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            using (MembershipServiceClient client = new MembershipServiceClient())
            {
                MembershipUser user = this.ConvertFrom(client.GetUserByKey(providerUserKey, userIsOnline));
                //client.Close();
                return user;
            }
        }
        public MembershipUser ContextCurrentUser
        {
            get
            {
                return (MembershipUser)HttpContext.Current.Items["::CURRENT_USER::"];
            }
            set
            {
                HttpContext.Current.Items["::CURRENT_USER::"] = value;
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            string[] usernames = username.Split(':');
            string institutionCode = usernames.Length > 1 ? usernames[1] : null;
            string impersonateCode = institutionCode;
            if (usernames.Length > 3 && usernames[3] == "*")
            {
                institutionCode = "";
            }

            if (String.IsNullOrEmpty(username) && HttpContext.Current.Items[HC_SERVICE_USER] != null)
            {
                username = Convert.ToString(HttpContext.Current.Items[HC_SERVICE_USER]);
            }

            MembershipUser user = null;

            if (HttpContext.Current.User.Identity.Name == username)
            {
                user = ContextCurrentUser;
                if (user == null)
                {
                    using (MembershipServiceClient client = new MembershipServiceClient())
                    {
                        user = this.ConvertFrom(client.GetUser(username, userIsOnline));
                        //client.Close();
                    }
                    ContextCurrentUser = user;
                }
            }
            else
            {
                using (MembershipServiceClient client = new MembershipServiceClient())
                {
                    user = this.ConvertFrom(client.GetUser(username, userIsOnline));
                    //client.Close();
                }
            }
            return user;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            this._Name = name;
            try
            {
                if (config["minPasswordLength"] != null)
                {
                    this._MinPasswordLength = Convert.ToInt32(config["minPasswordLength"]);
                }
                if (config["applicationName"] != null)
                {
                    this._ApplicationName = config["applicationName"];
                }
                if (config["maxOnlineUsers"] != null)
                {
                    this._MaxOnlineUsers = Convert.ToInt32(config["maxOnlineUsers"]);
                }
                if (config["minRequiredNonAlphanumericCharacters"] != null)
                {
                    this._MinRequiredNonAlphanumericCharacters = Convert.ToInt32(config["minRequiredNonAlphanumericCharacters"]);
                }
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException("There was an error reading the membership configuration settings", exception);
            }
        }

        private bool IsLockedOut(int digit)
        {
            return (digit >= this.MaxInvalidPasswordAttempts);
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool UnlockUser(string userName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ValidateUser(string username, string password)
        {
            using (MembershipServiceClient client = new MembershipServiceClient())
            {
                bool flag = client.ValidateUser(username, password);
                //client.Close();
                return flag;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return this._ApplicationName;
            }
            set
            {
                this._ApplicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public static string InstitutionCode
        {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["::SS_INSTITUTION_CODE::"]);
            }
            set
            {
                HttpContext.Current.Session["::SS_INSTITUTION_CODE::"] = value;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return this._MaxLoginAttempts;
            }
        }

        public int MaxOnlineUsers
        {
            get
            {
                return this._MaxOnlineUsers;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return this._MinRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return this._MinPasswordLength;
            }
        }

        public override string Name
        {
            get
            {
                return this._Name;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return 1;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return true;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return true;
            }
        }
    }
}

