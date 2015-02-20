using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Data;

namespace EvoPaj.Logic
{
    public class UserSystem : CoreServices<User, long>
    {
        public UserSystem()
        {

        }

        public bool ConfirmPassword(string LogginPassword, string CurrentPassword, string NewPassword, string ConfirmPassword, out string Msg)
        {
            string message = "";
            bool result = true;
            if (LogginPassword != CurrentPassword)
            {
                message += "The current password entered is different from the password used in logging in";
                result = false;
                
            }
            else if (NewPassword != ConfirmPassword)
            {
                if (message == "")
                    message += "The New password entered is different from Confirm password";
                else
                    message += "And the New password entered is different from Confirm password";
                result = false;
            }
            else if (NewPassword == "password")
            {
                message += "The new password has to be different from the default password sent to you.";
                result = false;                
            }
            else
            {

            }            
            Msg = message;
            return result;
        }

        public bool CheckPassword(string LogginPassword, string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            string message = "";
            bool result = true;
            if (LogginPassword != CurrentPassword)
            {
                message = "The current password entered is different from the password used in logging in";
                throw new Exception(message);

            }
            else if (NewPassword != ConfirmPassword)
            {
                message = "The New password entered is different from Confirm password";                
                throw new Exception(message);
            }
            else if (NewPassword.ToLower() == "password")
            {
                message = "The new password has to be different from the default password sent to you.";
                throw new Exception(message);
            }            
            return result;
        }

        public User GetUserByIDAndUsername(long institutionID, string Username, string Password)
        {
            return new UserDAO().GetUserByIDAndUserName(institutionID, Username, Password);
        }
        
        public bool SaveUser(User theUser, out string message)
        {
            message = string.Empty;
            bool result = false;
            try
            {   
                if(theUser != null)
                {
                    User existing = new UserDAO().GetUserByUserName(theUser.UserName);
                    if (existing == null)
                    {
                        new UserDAO().Save(theUser);
                        result = true;
                    }
                    else
                    {
                        message = "Username Already Exist";
                    }
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}",ex.Message));
                result = false;                
            }
            return  result;
        }

        public bool SaveUser(User theUser)
        {            
            bool result = false;
            try
            {
                if (theUser != null)
                {
                    User existing = new UserDAO().GetUserByUserName(theUser.UserName);
                    if (existing == null)
                    {
                        //new UserDAO().Save(theUser);
                        //result = true;
                        int outcome = InsertUser(theUser);
                        if (outcome > 0)
                            result = true;
                    }
                    else
                    {
                        throw new Exception("User with the Username Already Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateUser(User theUser)
        {
            bool result = false;
            try
            {
                //new UserDAO().Update(theUser);
                //result = true;
                int outcome = ModifyUser(theUser);
                if (outcome > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool DeleteUser(User theUser)
        {
            bool result = false;
            try
            {
                new UserDAO().Delete(theUser);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool UpdateUser(string query)
        {
            bool result = false;
            try
            {
                new UserDAO().RunQuery(query);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool CommitChanges()
        {
            bool result = false;
            try
            {
                new UserDAO().CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;

        }
        
        public static int InsertUser(User somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);
      
            db.AddParamAndValue("@Lastname", somebody.LastName.ToUpper());
            db.AddParamAndValue("@Firstname", somebody.FirstName.ToUpper());
            db.AddParamAndValue("@EmployeeNumber", somebody.EmployeeNumber);
            db.AddParamAndValue("@PhoneNumber", somebody.PhoneNumber);
            db.AddParamAndValue("@Email", somebody.Email);
            db.AddParamAndValue("@InstitutionID", somebody.TheInstitution.ID);
            db.AddParamAndValue("@UserInstitutionID", somebody.UserInstitutionID);
            db.AddParamAndValue("@RoleID", somebody.TheRole.ID);
            db.AddParamAndValue("@Username", somebody.UserName);
            db.AddParamAndValue("@Password", somebody.Password);
            db.AddParamAndValue("@FirstLogin", somebody.FirstLogin);
            db.AddParamAndValue("@Status", somebody.Status);

            int status = db.ExecuteNonQuery("sp_insert_user", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public static int ModifyUser(User somebody)
        {

            MSSQLDataAccess db = new MSSQLDataAccess(false);

            db.AddParamAndValue("@Lastname", somebody.LastName.ToUpper());
            db.AddParamAndValue("@Firstname", somebody.FirstName.ToUpper());
            db.AddParamAndValue("@EmployeeNumber", somebody.EmployeeNumber);
            db.AddParamAndValue("@PhoneNumber", somebody.PhoneNumber);
            db.AddParamAndValue("@Email", somebody.Email);
            db.AddParamAndValue("@InstitutionID", somebody.TheInstitution.ID);
            db.AddParamAndValue("@UserInstitutionID", somebody.UserInstitutionID);
            db.AddParamAndValue("@RoleID", somebody.TheRole.ID);
            db.AddParamAndValue("@Username", somebody.UserName);
            db.AddParamAndValue("@Password", somebody.Password);
            db.AddParamAndValue("@FirstLogin", somebody.FirstLogin);
            db.AddParamAndValue("@Status", somebody.Status);
            db.AddParamAndValue("@ID", somebody.ID);

            int status = db.ExecuteNonQuery("sp_update_user", CommandType.StoredProcedure);

            db.ClosedbConnection();
            return status;
        }
        
        public User GetUserByUserName(string userName)
        {
            return new UserDAO().GetUserByUserName(userName);
        }

        public User GetUserByUserName(string userName, string password)
        {
            return new UserDAO().GetUser(userName, password);
        }

        public bool AuthenticateUsers(User TheUser, int code, out string TheRole, out byte[] TheLogo, out string usr, out string institutionName, out string message, out bool firstLogin, out User selectedUser, out Int64 insID)
        {
            bool result = false;
            string checkresult = "";
            string role = ""; 
            byte[] logo = new byte[1024]; 
            string Usr = ""; 
            string institution = ""; 
            string msg = ""; 
            bool first = false; 
            User theuser = new User(); Int64 institutionID=0;       
            try
            {                
                IList<User> users = new UserDAO().RetrieveAll();

                if (System.Configuration.ConfigurationManager.AppSettings["DemoMode"] == "true")
                {


                    role = users[0].TheRole.Name;                    
                    Usr = users[0].LastName + " " + users[0].FirstName;
                    institution = users[0].TheInstitution.InstitutionName;
                    first = users[0].FirstLogin;
                    theuser = users[0];
                    institutionID = users[0].TheInstitution.ID;
                    selectedUser = users[0]; 
                    result = true;
                }
                else
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].TheInstitution.Status == PANE.Framework.Functions.DTO.Status.Active)
                        {
                            if (users[i].Status == PANE.Framework.Functions.DTO.UserStatus.Active)
                            {
                                if (TheUser.UserName == users[i].UserName && TheUser.Password == users[i].Password && code == users[i].TheInstitution.Code)
                                {
                                    role = users[i].TheRole.Name;                                    
                                    Usr = users[i].LastName + " " + users[i].FirstName;
                                    institution = users[i].TheInstitution.InstitutionName;
                                    first = users[i].FirstLogin;
                                    theuser = users[i];
                                    institutionID = users[i].TheInstitution.ID;
                                    result = true;
                                    checkresult = "";
                                    break;
                                }
                                else
                                {
                                    result = false;
                                    checkresult = "Incorrect";
                                }
                            }
                            else
                            {
                                msg = "Your User Profile is In Active. See your Administrator.";
                                checkresult = "";
                                result = false;
                            }
                        }
                        else
                        {
                            msg = "Your Institution Profile is In Active. See your Administrator.";
                            checkresult = "";
                            result = false;
                        }
                    }
                }
                if (checkresult == "Incorrect")
                {
                    msg = "Login was Not Successful. Check your Username and Password Combination.";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            TheRole = role;
            TheLogo = logo;
            usr = Usr;
            institutionName = institution;
            message = msg;
            firstLogin = first;
            selectedUser = theuser;
            insID = institutionID;
            return result;
        }

        public bool AuthenticateAdminUsers(User TheUser,out string message,out User selectedUser)
        {
            bool result = false; string msg = ""; User theuser = new User();
            try
            {
                IList<User> users = new UserDAO().RetrieveAll();
                string theInstitution = System.Configuration.ConfigurationManager.AppSettings["Institution"];
                if (System.Configuration.ConfigurationManager.AppSettings["DemoMode"] == "true")
                {
                    theuser = users[0];
                    result = true;
                }
                else
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].TheInstitution.Status == PANE.Framework.Functions.DTO.Status.Active)
                        {
                            if (users[i].Status == PANE.Framework.Functions.DTO.UserStatus.Active)
                            {
                                if (TheUser.UserName == users[i].UserName && TheUser.Password == users[i].Password && users[i].TheInstitution.InstitutionName == theInstitution)
                                {                                    
                                    theuser = users[i];                                    
                                    result = true;
                                    break;
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                msg = "Your User Profile is In-Active. See you Administrator.";
                                result = false;
                            }
                        }
                        else
                        {
                            msg = "Your Institution Profile is In-Active. See you Administrator.";
                            result = false;
                        }
                    }
                }
                if (!msg.Contains("Your"))
                    msg = "Login was Not Successful. Check your Username/Password Combination.";
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }           
            message = msg;           
            selectedUser = theuser;            
            return result;
        }

        public IList<User> GetUser(User TheUser)
        {
            return new UserDAO().GetUser(TheUser);
        }

        public IList<User> GetUsers(string LastName, long ID, int start, int limit, out int numOfResults)
        {
            return new UserDAO().GetUser(LastName, ID, start, limit, out numOfResults);
        } 
    }
}
