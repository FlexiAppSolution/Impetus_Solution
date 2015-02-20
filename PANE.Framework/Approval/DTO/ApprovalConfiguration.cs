using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;
using System.Runtime.Serialization;

namespace PANE.Framework.Approval.DTO
{
    [System.Runtime.Serialization.DataContract]
    [Serializable]
    public class ApprovalConfiguration : DataObject
    {
        private Function m_Data;
        private UserRole m_ApprovingRole;
        private Boolean m_IsApprovable;
        private Function m_MakerRole;

        [System.Runtime.Serialization.DataMember]
        public virtual Function Data
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
        public virtual Boolean IsApprovable
        {
            get { return m_IsApprovable; }
            set { m_IsApprovable = value; }
        }
        [System.Runtime.Serialization.DataMember]
        public virtual Function MakerRole
        {
            get { return m_MakerRole; }
            set { m_MakerRole = value; }
        }

        [DataMember]
        public virtual long? InstitutionID { get; set; }

        [DataMember]
        public virtual UserRole SubUserRole { get; set; }
    }
}
