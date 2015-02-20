using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Functions.DTO;
using PANE.Framework.Functions.DAO;
using PANE.Framework.Approval.DAO;
using PANE.Framework.Approval.DTO;
using PANE.Framework.DTO;
using System.Web.Security;
using System.Net.Mail;
using PANE.Framework.Services;
using PANE.Framework.Data;
using PANE.Framework.Utility;

namespace PANE.Framework.Approval
{
    public class ApprovalEngine
    {

        public ApprovalEngine()
        {
        }

        public List<Function> RetrieveFunctionsForApprovalConfiguration(UserCategory theUserCategory, long? institutionID)
        {
            return ApprovalConfigurationDAO.RetrieveFunctionsForApprovalConfiguration(theUserCategory, institutionID).OrderBy(p => p.Name).ToList<Function>(); 
        }

        public List<Function> RetrieveFunctionsForApproval(UserRole theUserRole, UserCategory theUserCategory, long? institutionID)
        {
            return ApprovalConfigurationDAO.RetrieveFunctionsForApproval(theUserRole, theUserCategory, institutionID);
        }

        public static Object SaveApproval(PANE.Framework.Utility.Action approvalAction, object oldEntity, object newEntity, string functionRoleName, long? subUserRoleID, UserCategory theUserCategory, long? institutionID)
        {
            ApprovalConfiguration apprConfig = ApprovalConfigurationDAO.RetrieveByMakerRoleName(functionRoleName, subUserRoleID, theUserCategory, institutionID);
            if (apprConfig == null || !apprConfig.IsApprovable)
            {   
                //Save straight away.
               // ApprovalDAO.ClearCurrentSession();
                return PANE.Framework.Utility.Approver.ApproveAction(approvalAction, newEntity);

            }
            else
            {
                try
                {

                    //Create Approval log.
                    PANE.Framework.Approval.DTO.Approval approval = PANE.Framework.Approval.DTO.Approval.CreateApproval(newEntity.GetType().Name, (newEntity as IEntity).Name, (newEntity as DataObject).ID, oldEntity, newEntity, apprConfig.MakerRole, subUserRoleID);
                    //Save for approval.  
                    //if (ConfigurationHelper.ConfigItem<ApprovalEngine, bool>("SendApprovalMails"))
                    //{
                    //    //SaveMailFromApproval(approval);
                    //}
                    
                    ApprovalDAO.Save(approval);
                    ApprovalDAO.CommitChanges();
                    return approval;
                }
                catch
                {
                    throw;
                }
            }
        }

        public static Object SaveInstantApproval(PANE.Framework.Utility.Action approvalAction, object oldEntity, object newEntity, string functionRoleName, long? subUserRoleID, UserCategory theUserCategory, long? institutionID)
        {
            ApprovalConfiguration apprConfig = ApprovalConfigurationDAO.RetrieveByMakerRoleName(functionRoleName, subUserRoleID, theUserCategory, institutionID);
            if (apprConfig == null || !apprConfig.IsApprovable)
            {
                //Save straight away.
                return PANE.Framework.Utility.Approver.ApproveAction(approvalAction, newEntity);
            }
            else
            {
                try
                {
                    //Create Approval log.
                    PANE.Framework.Approval.DTO.Approval approval = PANE.Framework.Approval.DTO.Approval.CreateApproval(newEntity.GetType().Name, (newEntity as IEntity).Name, (newEntity as DataObject).ID, oldEntity, newEntity, apprConfig.MakerRole, subUserRoleID);

                    ApprovalDAO.Save(approval);
                    //ApprovalDAO.CommitChanges();
                    return approval;
                }
                catch
                {
                    throw;
                }
            }
        }

        public long UpdateFunctionApprovalConfiguration(Function function)
        {
           
            //ApprovalConfigurationDAO.ClearCurrentSession();
            if (function.ApprovalConfigurationUpdate != null)
            {
                if (function.SubUserRoleUpdate != null)
                {
                    function.ApprovalConfigurationUpdate.SubUserRole = function.SubUserRoleUpdate;
                }
                if (function.ApprovalConfigurationUpdate.ID == 0)
                {
                    function.ApprovalConfigurationUpdate.MakerRole = function;
                    ApprovalConfigurationDAO.Save(function.ApprovalConfigurationUpdate);
                }
                else
                {
                    ApprovalConfigurationDAO.Update(function.ApprovalConfigurationUpdate);
                    ApprovalConfigurationDAO.CommitChanges();
                }
            }
          //  ApprovalConfigurationDAO.CommitChanges();
           // FunctionDAO.Update(function);
           // FunctionDAO.CommitChanges();
            //FunctionDAO.ClearCurrentSession();
            return function.ID;
        }
        public static void SaveMailFromApproval(Approval.DTO.Approval approval)
        {
           Mail mail;
            string path = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"]+@"/Mails/ApprovalMail.htm";
            string fromEmail = System.Configuration.ConfigurationManager.AppSettings["MailFrom"];
            IList<string> emailAddresses = new List<string>();

            ApprovalConfiguration config = ApprovalConfigurationDAO.RetrieveByFunction(approval.ApprovalFunction, null, null);
            
            IList<IUser> theUsers = new UserDAO().GetByUserRole(config.ApprovingRole);
            Dictionary<string, object> entities = new Dictionary<string, object>();
            entities.Add("InitiatorLastName", approval);
            entities.Add("InitiatorOtherNames", approval);
                
             
            foreach (IUser user in theUsers)
            {
                if (user.ApprovalRight)
                {
                    emailAddresses.Add(user.Email);
                    
                }
            }
             
            mail =  new MailSystem().CreateEmail(path,fromEmail, emailAddresses, entities,approval);
            MailDAO.Save(mail);
        }

    }

}
