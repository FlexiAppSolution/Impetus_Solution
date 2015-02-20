using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using PANE.Framework.Utility;
using PANE.Framework.Functions.DAO;
using PANE.Framework.Functions.DTO;
using PANE.Framework.DTO;
using System.Web;

namespace PANE.Framework.Functions
{
    public class FunctionsMembershipProvider : MembershipProvider
    {
        private bool _isOnline;
        private string _Name;
        private int _MaxLoginAttempts = 3;
        private int _MaxOnlineUsers = -1;
        private int _MinPasswordLength = 0;
        private int _MinRequiredNonAlphanumericCharacters = 0;
        private string _ApplicationName;
        private const string SS_USER_INFO = "::APP_USER_INFO::";


        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            _Name = name;

            try
            {
                if (config["minPasswordLength"] != null)
                    _MinPasswordLength = Convert.ToInt32(config["minPasswordLength"]);
                if (config["applicationName"] != null)
                    _ApplicationName = config["applicationName"];
                if (config["maxOnlineUsers"] != null)
                    _MaxOnlineUsers = Convert.ToInt32(config["maxOnlineUsers"]);
                if (config["minRequiredNonAlphanumericCharacters"] != null)
                    _MinRequiredNonAlphanumericCharacters = Convert.ToInt32(config["minRequiredNonAlphanumericCharacters"]);
            }
            catch (Exception Ex)
            {
                throw new System.Configuration.ConfigurationErrorsException("There was an error reading the membership configuration settings", Ex);
            }
        }

        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool isSuccessful = false;
            oldPassword = new MD5Password().CreateSecurePassword(oldPassword);
            IUser usr = UserDAO.RetrieveByUsername(username);
            if (usr != null && usr.Password == oldPassword)
            {
                newPassword = new MD5Password().CreateSecurePassword(newPassword);
                if (usr.Password != newPassword)
                {
                    usr.Password = newPassword;
                    //UserDAO.ClearCurrentSession();
                    UserDAO.Merge(usr);
                    UserDAO.CommitChanges();
                    isSuccessful = true;
                }
            }
            return isSuccessful;
        }
        public bool ChangePassword(long userID, string oldPassword, string newPassword)
        {
            bool isSuccessful = false;
            oldPassword = new MD5Password().CreateSecurePassword(oldPassword);
            
            IUser usr = UserDAO.Retrieve(userID) ;
            
            if (usr != null && usr.Password == oldPassword)
            {
                newPassword = new MD5Password().CreateSecurePassword(newPassword);
                if (usr.Password != newPassword)
                {
                    usr.Password = newPassword;
                    usr.IsFirstLogin = false;
                    UserDAO.Update(usr);
                    UserDAO.CommitChanges();
                    isSuccessful = true;
                }
            }
            else
            {
                throw new Exception("Invalid Current Password");
            }
            return isSuccessful;
        }
        public override string Name
        {
            get
            {
                return _Name;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser newUser = null;
            IUser userInfo = null;
            status = MembershipCreateStatus.UserRejected;
            try
            {
                userInfo.UserName = username;
                userInfo.Password = password;
               
                UserDAO.Save(userInfo);
            }
            catch (Exception Ex)
            {
                status = MembershipCreateStatus.ProviderError;
                newUser = null;
            }
            return newUser as MembershipUser;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EnablePasswordReset
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            //ArrayList allUsers = (ArrayList)new UserSystem().RetrieveAllUsers();
            //MembershipUserCollection col = new MembershipUserCollection();
            //foreach (User user in allUsers)
            //{
            //    try
            //    {
            //        PortalUsers xchangeUser = new PortalUsers(this.Name, user.UserName, user.ID, user.UserName, "", "", user.Status == CMS.Common.Utility.Status.Active, user.LastLoginDate, user.LastLoginDate, user.LastLoginDate, user.LastLoginDate);
            //        col.Add(xchangeUser as MembershipUser);
            //    }
            //    catch { }
            //}
            //totalRecords = col.Count;
            //return col;
            throw new Exception("The method or operation is not implemented."); 
       
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection muc = new MembershipUserCollection();
            foreach (IUser user in UserDAO.RetrieveAll())
            {
                FunctionsMembershipUser theUser = new FunctionsMembershipUser(this.Name, user.UserName, (user as DataObject).ID, user.UserName, "", "", user.Status == UserStatus.Active, user.CreationDate, user.LastLoginDate, user.LastLoginDate, user.CreationDate);
                theUser.UserDetails = user;
                muc.Add(theUser);
            }
            totalRecords = muc.Count;
            return muc;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetPassword(string username, string answer)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            string[] usernames = username.Split(':');

            IUser user = null;
            if (HttpContext.Current.User.Identity.Name == username)
            {
                user = ContextCurrentUser;
                if (user == null)
                {
                    user = UserDAO.RetrieveBy(usernames[0], usernames.Length > 1 ? usernames[1] : null);
                    ContextCurrentUser = user;
                }
            }
            else
            {
                user = UserDAO.RetrieveBy(usernames[0], usernames.Length > 1 ? usernames[1] : null);
            }
            if (user != null)
            {
                FunctionsMembershipUser theUser = new FunctionsMembershipUser(this.Name, user.UserName, (user as DataObject).ID, user.UserName, "", "", user.Status == UserStatus.Active, user.CreationDate, user.LastLoginDate, user.LastLoginDate, user.CreationDate);
                theUser.UserDetails = user;
                return theUser as MembershipUser;
            }
            else
                return null;
        }
        private IUser ContextCurrentUser
        {
            get
            {
                return (IUser)HttpContext.Current.Items[SS_USER_INFO];
            }
            set
            {
                HttpContext.Current.Items[SS_USER_INFO] = value;
            }
        }
        

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            IUser user = UserDAO.Retrieve(Convert.ToInt64(providerUserKey));
            if (user != null)
            {
                FunctionsMembershipUser theUser = new FunctionsMembershipUser(this.Name, user.UserName, (user as DataObject).ID, user.UserName, "", "", user.Status == UserStatus.Active, user.CreationDate, user.LastLoginDate, user.LastLoginDate, user.CreationDate);
                theUser.UserDetails = user;
                return theUser as MembershipUser;
            }
            else
                return null;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxLoginAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinPasswordLength; }
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
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new Exception("The method or operation is not implemented."); }
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
            get { return true; }
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
            password = new MD5Password().CreateSecurePassword(password);
            IUser user = UserDAO.RetrieveBy(username, null);//UserDAO.AuthenticateUser(username, password, UserCategory.ServiceProvider ,null);
            return (user != null && user.Status == UserStatus.Active ? true : false);
        }

        public int MaxOnlineUsers
        {
            get { return _MaxOnlineUsers; }
        }

    }
}
