using System;
using System.Runtime.Serialization;
using PANE.Framework.DTO;
using System.Collections.Generic;
using PANE.Framework.Functions.DTO;
using System.Reflection;
using System.Web.Security;
using PANE.Framework.DAO;

namespace PANE.Framework.Approval.DTO
{
    [DataContract]
    [Serializable]

    public class Approval : DataObject
    {
        private DateTime? m_ActionDate;
        private DateTime? m_DateRequested;
        private string m_DataType;
        private long m_ObjectID;
        private IUser  m_User;
        private string m_ClientIPAddress;
        private string m_ClientName;
        private byte[] m_DataAfter;
        private byte[] m_DataBefore;
        private byte[] m_data;
        private ApprovalStatus m_Status;
        private string m_EntityName;
        private string m_ApproverLastName;
        private string m_ApproverOtherNames;

        [DataMember]
        public virtual DateTime? ActionDate
        {
            get
            {
                return m_ActionDate ;
            }
            set
            {
                m_ActionDate  = value;
            }
        }

        [DataMember]
        public virtual DateTime? DateRequested
        {
            get
            {
                return m_DateRequested ;
            }
            set
            {
                m_DateRequested = value;
            }
        }
        
        public virtual Function ApprovalFunction { get; set; }

        public virtual long? SubUserRoleID { get; set; }

        [DataMember]
        public virtual string DataType
        {
            get
            {
                return m_DataType ;
            }
            set
            {
               m_DataType  = value;
            }
        }
        [DataMember]
        public virtual long ObjectID
        {
            get
            {
                return m_ObjectID ;
            }
            set
            {
                m_ObjectID  = value;
            }
        }
        [DataMember]
        public virtual long UserID
        {
            get;
            set;

        }
        [DataMember]
        public virtual IUser User
        {
            get;
            set;
        }

        [DataMember]
        public virtual string ClientIPAddress
        {
            get { return m_ClientIPAddress; }
            set { m_ClientIPAddress = value; }
        }
        [DataMember]
        public virtual string ClientName
        {
            get { return m_ClientName; }
            set { m_ClientName = value; }
        }
        [DataMember]
        public virtual byte[] DataAfter
        {
            get { return m_DataAfter; }
            set { m_DataAfter = value; }
        }
        [DataMember]
        public virtual byte[] DataBefore
        {
            get { return m_DataBefore; }
            set { m_DataBefore = value; }
        }

        [DataMember]
        public virtual ApprovalStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        [DataMember]
        public virtual string EntityName
        {
            get { return m_EntityName; }
            set { m_EntityName = value; }
        }
        [DataMember]
        public virtual string ApproverLastName

        {
            get { return m_ApproverLastName; }
            set { m_ApproverLastName = value; }
        }
        [DataMember]
        public virtual string ApproverOtherNames
        {

            get { return m_ApproverOtherNames; }
            set { m_ApproverOtherNames = value; }

        }

        [DataMember]
        public virtual string InitiatorLastName { get; set; }

        [DataMember]
        public virtual string InitiatorOtherNames { get; set; }
        [DataMember]
        public virtual string InitiatorEmail { get; set; }
        private bool _notificationSent;

        public virtual bool ApprovalNotificationStatus
        {
            get { return _notificationSent; }
            set { _notificationSent = value; }
        }

        private static Approval GetDefaults(string dataType, string entityName, long objectID)
        {
            Approval approval = new Approval();
            approval.ClientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            try
            {
                approval.ClientName = System.Net.Dns.GetHostName();
                //approval.ClientName = System.Net.Dns.GetHostEntry(approval.ClientIPAddress).HostName;
            }
            catch
            {
                approval.ClientName = "[Could not resolve Client Name]";
            }
            approval.DateRequested = System.DateTime.Now;
            approval.Status = ApprovalStatus.Pending;
            IUser user = (Membership.GetUser() as FunctionsMembershipUser).UserDetails as IUser;
            approval.UserID = user.ID;
            approval.InitiatorLastName = user.LastName;
            approval.InitiatorOtherNames = user.OtherNames;                      
            approval.DataType = dataType;
            approval.EntityName = entityName;
            approval.ObjectID = objectID;
            approval.ApprovalNotificationStatus = false;
            approval.InitiatorEmail = user.Email;

            return approval;
            
        }
        public static Approval CreateApprovalForEnableDisable(string dataType, string entityName, long objectID, object data)
        {
            Approval approval = Approval.GetDefaults(dataType, entityName, objectID);

            try
            {
                //Serialize first object as DataAfter.
                approval.DataAfter = PANE.Framework.Utility.BinarySerializer.SerializeObject(data);

                //Try and switch the status property and Serialize as DataBefore.
                PropertyInfo statusInfo = data.GetType().GetProperty("Status", typeof(Status));
                PANE.Framework.Functions.DTO.Status status = (PANE.Framework.Functions.DTO.Status)statusInfo.GetValue(data, null);

                status = status == PANE.Framework.Functions.DTO.Status.Active ? PANE.Framework.Functions.DTO.Status.InActive :
                    PANE.Framework.Functions.DTO.Status.Active;
                statusInfo.SetValue(data, status, null);
                approval.DataBefore = PANE.Framework.Utility.BinarySerializer.SerializeObject(data);

                ////Serialize first object as DataAfter.
                //byte[] tempData = PANE.Framework.Utility.BinarySerializer.SerializeObject(data);

                ////Try and switch the status property and Serialize as DataBefore.
                //PropertyInfo statusInfo = data.GetType().GetProperty("Status", typeof(Status));
                //PANE.Framework.Functions.DTO.Status status = (PANE.Framework.Functions.DTO.Status)statusInfo.GetValue(data, null);

                //status = status == PANE.Framework.Functions.DTO.Status.Active ? PANE.Framework.Functions.DTO.Status.InActive :
                //    PANE.Framework.Functions.DTO.Status.Active;
                //statusInfo.SetValue(data, status, null);
                ////approval.DataBefore = PANE.Framework.Utility.BinarySerializer.SerializeObject(data);
                //approval.Data = PANE.Framework.Utility.BinarySerializer.SerializeData(data, 
                //                                PANE.Framework.Utility.BinarySerializer.DeSerializeObject(tempData));

            }
            catch { }

            return approval;
        }

        public static Approval CreateApproval(string dataType, string entityName, long objectID, object dataBefore, object dataAfter, Function approvalFunction,long? subUserRoleID)
        {
            try
            {
                Approval approval = Approval.GetDefaults(dataType, entityName, objectID);
                approval.SubUserRoleID = subUserRoleID;
                approval.ApprovalFunction = approvalFunction;

                //Create a deep copy of the current entity.
                if (dataAfter != null)
                {
                    approval.DataAfter = PANE.Framework.Utility.BinarySerializer.SerializeObject(dataAfter);

                }
                //object newObject = PANE.Framework.Utility.BinarySerializer.DeSerializeObject(approval.DataAfter);



                if (dataBefore != null)
                {
                    //Get the old copy.
                    CoreDAO<Approval, long>.BuildSession().Refresh(dataBefore);
                    approval.DataBefore = PANE.Framework.Utility.BinarySerializer.SerializeObject(dataBefore);
                }
                return approval;
            }
            catch
            {
            }
            return new PANE.Framework.Approval.DTO.Approval();
            //approval.Data = PANE.Framework.Utility.BinarySerializer.SerializeData(dataBefore, dataAfter);
           

        }

    }
}

