

namespace SOA.Framework.ApprovalConfigHelper
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SOA.Framework.ApprovalConfigHelper.DAO;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using PANE.Framework.Managed.NHibernateManager.Configuration;
    using System.Web.Security;
    using SOA.Framework.FS;
    using PANE.Framework.Managed.Utility;
    using System.Reflection;
    using PANE.Framework.Managed.AuditTrail.DTO;
    using PANE.Framework.Managed.Functions.DAO;
using PANE.Framework.Managed.Functions.DTO;

    [ServiceContract(Namespace = ""), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApprovalConfigService
    {
        [OperationContract]
        public void DeleteApprovalConfiguration(ApprovalConfig approvalConfig)
        {

            ApprovalConfigDAO.Delete(approvalConfig, DatabaseSource.Core);
            ApprovalConfigDAO.CommitChanges(approvalConfig.MFBCode);
        }
        [OperationContract]
        public long UpdateFunctionApprovalConfiguration(FunctionItem function, string makeRoleName)
        {
         //   ApprovalConfigDAO.ClearCurrentSession(function.MFBCode);
            if (function.ApprovalConfigurationUpdate != null)
            {
                function.ApprovalConfigurationUpdate.MFBCode = function.MFBCode;

                if (function.SubUserRoleUpdate != null)
                {
                    function.ApprovalConfigurationUpdate.SubUserRole = function.SubUserRoleUpdate.ID;
                }
                if (function.ApprovalConfigurationUpdate.ID == 0)
                {
                    function.ApprovalConfigurationUpdate.MakerRole = function.ID;
                    function.ApprovalConfigurationUpdate.MakeRoleName = makeRoleName;
                    ApprovalConfigDAO.Save(this.ConvertServiceApprovalConfig(function.ApprovalConfigurationUpdate), DatabaseSource.Core);
                }
                else
                {
                    ApprovalConfigDAO.Update(this.ConvertServiceApprovalConfig(function.ApprovalConfigurationUpdate), DatabaseSource.Core);
                }
            }
            ApprovalConfigDAO.CommitChanges(this.ConvertServiceApprovalConfig(function.ApprovalConfigurationUpdate).MFBCode,DatabaseSource.Core);
           // ApprovalConfigDAO.ClearCurrentSession(function.MFBCode);
            return function.ID;
        }

        [OperationContract]
        public FunctionItem GetFunctionByID(long id,string endpoint)
        {            
            return new FunctionServiceClient(endpoint).GetFunctionByID(id);            
        }

        [OperationContract]
        public List<PANE.Framework.Managed.Utility.TrailItem> GetApprovalObject(Byte[] dataBefore ,Byte[] dataAfter)
        {
            List<PANE.Framework.Managed.Utility.TrailItem> trails = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataBefore, dataAfter, 4);
            return trails;
        }

        
        [OperationContract]
        public IList<ApprovalConfig> RetrieveByMakerRoleID(string mfbCode, long? makerRoleID)
        {
            return ApprovalConfigDAO.RetrieveByMakerRoleID(mfbCode, makerRoleID);
        }


        [OperationContract]
        public bool SaveApprovedObject(Byte[] dataToApprove, string assemblyName, IUser auditableUser)
        {

          Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);
          IAuditable auditable = obj as IAuditable;
          if (auditable != null)
          {
              auditable.AuditableUser = auditableUser;
          }
          Assembly assembly =  System.Reflection.Assembly.Load(assemblyName);
          Approver.Insert(assembly, obj);
            
            return true;
        }

        [OperationContract]
        public bool UpdateApprovedObject(Byte[] dataToApprove, string assemblyName, IUser auditableUser)
        {

            Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);
            IAuditable auditable = obj as IAuditable;
            if (auditable != null)
            {
                auditable.AuditableUser = auditableUser;
            }
            Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
            Approver.Update(assembly, obj);

            return true;
        }

        [OperationContract]
        public bool DeleteApprovedObject(Byte[] dataToApprove, string assemblyName, IUser auditableUser)
        {

            Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);
            IAuditable auditable = obj as IAuditable;
            if (auditable != null)
            {
                auditable.AuditableUser = auditableUser;
            }
            Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
            Approver.Delete(assembly, obj);

            return true;
        }

        private ApprovalConfig ConvertServiceApprovalConfig(SOA.Framework.FS.ApprovalConfig config) {

            ApprovalConfig appconfig = new ApprovalConfig();

            appconfig.ApprovingRoleID = config.ApprovingRoleID;
            appconfig.Data = config.Data;
            appconfig.IsApprovable = config.IsApprovable;
            appconfig.MakeRoleName = config.MakeRoleName;
            appconfig.MakerRole = config.MakerRole;
            appconfig.MFBCode = config.MFBCode;
            appconfig.SubUserRole = config.SubUserRole;


            return appconfig;
        }
        
    }
}
