namespace SOA.Framework.FunctionHelper
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Web.Security;
    using System.Collections.Generic;
    using PANE.Framework.Managed.Functions.DTO;
    using PANE.Framework.Managed.Functions.DAO;
    using PANE.Framework.Managed.Approval.DAO;
    //using SOA.Framework.MS;
    using PANE.Framework.Managed.Approval.DTO;
    using System.Linq;
    using SOA.Framework.FunctionHelper.DAO;
    using PANE.Framework.Managed.NHibernateManager.Configuration;

    [ServiceContract(Namespace = ""), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FunctionService
    {
        [OperationContract]
        public List<FunctionItem> GetFunctionsForApprovalConfiguration(UserCategory userCategory)
        {
            List<FunctionItem> functions = SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.RetrieveFunctionsForApprovalConfiguration(null, userCategory);
            return functions;
        }

        [OperationContract]
        public List<FunctionItem> RetrieveAll(UserCategory userCategory)
        {
            List<FunctionItem> functions = SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.RetrieveFunctions(null, userCategory);
            return functions;
        }

        [OperationContract(Name = "GetFunctionsForApprovalConfiguration Per Institution")]
        public List<FunctionItem> GetFunctionsForApprovalConfiguration(long? instiutionID, UserCategory userCategory)
        {
            List<FunctionItem> functions = SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.RetrieveFunctionsForApprovalConfiguration(instiutionID, userCategory);
            return functions;
        }

        [OperationContract(Name = "RetrieveAll Per Institution")]
        public List<FunctionItem> RetrieveAll(long? instiutionID, UserCategory userCategory)
        {
            List<FunctionItem> functions = SOA.Framework.FunctionHelper.DAO.FunctionItemDAO.RetrieveFunctions(instiutionID,userCategory);
            return functions;
        }

        /// <summary>
        /// Usually going to be used on the User Role page where a User Role
        /// </summary>
        /// <param name="functionItems"></param>
        /// <returns></returns>
        [OperationContract]
        public bool SaveThUserRoleFunctionItems(string mfbCode, List<UserRoleFunctionItem> userRoleFunctionItems)
        {
            //Save all the UserRole FunctionItems and their corresponding SubRoles
            foreach (UserRoleFunctionItem urfi in userRoleFunctionItems)
            {
                urfi.MFBCode = mfbCode;
                UserRoleFunctionItemDAO.Save(urfi);

                if (urfi.SubUserRoleItems != null)
                {
                    foreach (UserRoleFunctionSubRoleItem urfsr in urfi.SubUserRoleItems)
                    {
                        urfsr.MFBCode = mfbCode;
                        urfsr.TheUserRoleFunctionItem = urfi;
                        UserRoleFunctionSubRoleItemDAO.Save(urfsr);
                    }
                }
            }
            UserRoleFunctionSubRoleDAO.CommitChanges(mfbCode);
            return true;
        }

        [OperationContract]
        public bool UpdateThUserRoleFunctionItems(string mfbCode, List<UserRoleFunctionItem> userRoleFunctionItems, long id)
        {
            bool updated = false;
            List<UserRoleFunctionItem> userRoleFunctionItemsOld = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, id);
            foreach (UserRoleFunctionItem userRoleFunctionItem in userRoleFunctionItemsOld)
            {
                userRoleFunctionItem.MFBCode = mfbCode;
                UserRoleFunctionItemDAO.Delete(userRoleFunctionItem);

            }

            updated = SaveThUserRoleFunctionItems(mfbCode, userRoleFunctionItems);
            return updated;
        }

        [OperationContract]
        public FunctionItem ConvertFunction(Function function)
        {
            return new FunctionItem(function);
        }

        [OperationContract]
        public FunctionItem GetFunctionByID(long id)
        {

            return FunctionItemDAO.RetreiveByID(id);

        }

        [OperationContract]
        public FunctionItem GetFunctionByRoleName(string  rolename, UserCategory userCategory)
        {
            return FunctionItemDAO.RetrieveByRoleName(rolename, userCategory);
        }

        [OperationContract]
        public List<FunctionItem> GetFunctionsForUserRole(string mfbCode,long id) {
            List<long> ids = new List<long>();

            List<UserRoleFunctionItem> userRoleFunctionItems = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, id);
            foreach (UserRoleFunctionItem userRoleFunctionItem in userRoleFunctionItems)
            {

                ids.Add(userRoleFunctionItem.TheFunctionItemID);
            }

            long? institutionID = null;
            if (!String.IsNullOrEmpty(mfbCode))
            {
                PANE.Framework.Managed.MfbServiceRef.Mfb institution = null;
                using (PANE.Framework.Managed.MfbServiceRef.MfbServiceClient institutionClient = new PANE.Framework.Managed.MfbServiceRef.MfbServiceClient())
                {
                    institution = institutionClient.GetByCode(mfbCode);
                    institutionID = institution.ID;
                }
            }

            return FunctionItemDAO.RetreiveByIDs(institutionID, ids.ToArray());
        }

        [OperationContract]
        public List<FunctionItem> GetDependencyFunctionsForUserRole(string mfbCode, long id)
        {
            List<long> ids = new List<long>();

            List<UserRoleFunctionItem> userRoleFunctionItems = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, id);

            foreach (UserRoleFunctionItem userRoleFunctionItem in userRoleFunctionItems)
            {
                ids.Add(userRoleFunctionItem.TheFunctionItemID);
            }

            long? institutionID = null;
            if (!String.IsNullOrEmpty(mfbCode))
            {
                PANE.Framework.Managed.MfbServiceRef.Mfb institution = null;
                using (PANE.Framework.Managed.MfbServiceRef.MfbServiceClient institutionClient = new PANE.Framework.Managed.MfbServiceRef.MfbServiceClient())
                {
                    institution = institutionClient.GetByCode(mfbCode);
                    institutionID = institution.ID;
                }
            }
            //List<FunctionItem> mfy = FunctionItemDAO.RetreiveByIDs(ids.ToArray());
            return FunctionItemDAO.RetreiveByIDs(institutionID, ids.ToArray()).Where(f => f.Dependency == true).ToList<FunctionItem>();
        }

        [OperationContract]
        public List<FunctionItem> RetrieveFunctionItemsForUserRoleID(string mfbCode, long userRoleID)
        {
            //Get the attached UserRole FunctionItems.
            List<UserRoleFunctionItem> urfs = UserRoleFunctionItemDAO.RetrieveByUserRoleID(mfbCode, userRoleID);

            //Use the functionItem IDs and retreive the corresponding FunctionItems.
            long[] functionItemIDs = urfs.Select(f => f.TheFunctionItemID).Distinct().ToArray();

            long? institutionID = null;
            if (!String.IsNullOrEmpty(mfbCode))
            {
                PANE.Framework.Managed.MfbServiceRef.Mfb institution = null;
                using (PANE.Framework.Managed.MfbServiceRef.MfbServiceClient institutionClient = new PANE.Framework.Managed.MfbServiceRef.MfbServiceClient())
                {
                    institution = institutionClient.GetByCode(mfbCode);
                    institutionID = institution.ID;
                }
            }
            List<FunctionItem> functionItemsForCurrentUser = FunctionItemDAO.RetreiveByIDs(institutionID, functionItemIDs);
            return functionItemsForCurrentUser;
        }

    }
}
