using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.Managed.DTO;
using PANE.Framework.Managed.Functions.DTO;

namespace PANE.Framework.Managed.Approval.DTO
{
    [System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/PANE.Framework.Approval.DTO")]
    public class ApprovalConfiguration: DataObject
    {
        private long m_Data;
        private UserRole m_ApprovingRole;
        private Boolean m_IsApprovable;
        private long m_MakerRole    ;
        [System.Runtime.Serialization.DataMember]
        public virtual long Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
        [System.Runtime.Serialization.DataMember]
        public virtual UserRole ApprovingRole
        {
            get { return m_ApprovingRole; }
            set { m_ApprovingRole = value; }
        }
        [System.Runtime.Serialization.DataMember]
        public virtual long ApprovingRoleID
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public virtual Boolean IsApprovable
        {
            get { return m_IsApprovable; }
            set { m_IsApprovable = value; }
        }

        /// <summary>
        /// function that the Approval config is linked 2
        /// </summary>
        [System.Runtime.Serialization.DataMember]
        public virtual long MakerRole
        {
            get { return m_MakerRole; }
            set { m_MakerRole = value; }
        }

        [System.Runtime.Serialization.DataMember]
        public virtual string MakeRoleName
        {
            get;
            set;
        }

        [System.Runtime.Serialization.DataMember]
        public virtual string ApplicationName
        {
            get;
            set;
        }

        public virtual UserRole SubUserRole { get; set; }
    }
}
