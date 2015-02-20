using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.Managed.DTO;
using SOA.Framework.FunctionHelper;

namespace SOA.Framework.ApprovalConfigHelper
{
    [System.Runtime.Serialization.DataContract]
    [Serializable]
    public class ApprovalConfig : DataObject
    {
        private long m_Data;
        private long m_ApprovingRole;
        private Boolean m_IsApprovable;
        private long m_MakerRole;
        [System.Runtime.Serialization.DataMember]
        public virtual long Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
       
        [System.Runtime.Serialization.DataMember]
        public virtual long ApprovingRoleID
        {
            get { return m_ApprovingRole; }
            set { m_ApprovingRole = value; }
        }

        [System.Runtime.Serialization.DataMember]
        public virtual Boolean IsApprovable
        {
            get { return m_IsApprovable; }
            set { m_IsApprovable = value; }
        }

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
        public virtual long SubUserRole { get; set; }
    }
}
