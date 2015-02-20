

namespace SOA.Framework.ApprovalHelper
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
    using PANE.Framework.Managed.Functions.DTO;
    using System.Web;
    using SOA.Framework.MembershipHelper;

    [ServiceContract(Namespace = ""), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApprovalService
    {
        [OperationContract]
        public List<PANE.Framework.Managed.Utility.TrailItem> GetApprovalObject(Byte[] dataBefore, Byte[] dataAfter, string username)
        {
            Utility.SetCurrentUser(username);
            List<PANE.Framework.Managed.Utility.TrailItem> trails = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataBefore, dataAfter, 4);
            return trails;
        }

        [OperationContract]
        [FaultContract(typeof(ApproverWCFException))]
        public ApprovalResponse SaveApprovedObject(Byte[] dataToApprove, string assemblyName, string username)
        {
            Utility.SetCurrentUser(username);
            Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);
            try
            {
                Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
                return Approver.Insert(assembly, obj);
            }
            catch (ApproverException ex)
            {
                throw new FaultException<ApproverWCFException>(new ApproverWCFException(ex.Message), new FaultReason(ex.Message));
            }
        }

        [OperationContract]
        [FaultContract(typeof(ApproverWCFException))]
        public ApprovalResponse UpdateApprovedObject(Byte[] dataToApprove, string assemblyName, string username)
        {
            Utility.SetCurrentUser(username);
            try
            {
                Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);

                Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
                return Approver.Update(assembly, obj);
            }
            catch (ApproverException ex)
            {
                throw new FaultException<ApproverWCFException>(new ApproverWCFException(ex.Message), new FaultReason(ex.Message));
            }
        }

        [OperationContract]
        [FaultContract(typeof(ApproverWCFException))]
        public ApprovalResponse DeleteApprovedObject(Byte[] dataToApprove, string assemblyName, string username)
        {
            Utility.SetCurrentUser(username);
            try
            {
                Object obj = PANE.Framework.Managed.Utility.BinarySerializer.DeSerializeObject(dataToApprove);

                Assembly assembly = System.Reflection.Assembly.Load(assemblyName);
                return Approver.Delete(assembly, obj);
            }
            catch (ApproverException ex)
            {
                throw new FaultException<ApproverWCFException>(new ApproverWCFException(ex.Message), new FaultReason(ex.Message));
            }
        }
    }
}