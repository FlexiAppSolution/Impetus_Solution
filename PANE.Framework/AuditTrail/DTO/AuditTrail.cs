using System;
using PANE.Framework.AuditTrail.Enums;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace PANE.Framework.AuditTrail.DTO
{

    [Serializable]
    public class AuditTrail : DataObject
    {
        private AuditAction m_Action;
        private DateTime m_ActionDate;
        private Byte[] m_DataAfter;
        private Byte[] m_DataBefore;
        private String m_DataType;
        private Int64 m_ObjectID;
        private String m_UserName;
        private Int64 m_UserID;
        private string m_ClientIPAddress;
        private string m_ClientName;

        public AuditTrail()
        {
           
        }

        public AuditTrail(long id)
        {
            ID = id;
        }

        public virtual Object TheObject { get; set; }

        public virtual AuditAction Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        public virtual DateTime ActionDate
        {
            get { return m_ActionDate; }
            set { m_ActionDate = value; }
        }

        public virtual byte[] Data
        { get; set; }
        public virtual Byte[] DataAfter
        {
            get { return m_DataAfter; }
            set { m_DataAfter = value; }
        }

        public virtual Byte[] DataBefore
        {
            get { return m_DataBefore; }
            set { m_DataBefore = value; }
        }

        public virtual String DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        public virtual Int64 ObjectID
        {
            get { return m_ObjectID; }
            set { m_ObjectID = value; }
        }

        public virtual string UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }

        public virtual IUser User
        {
            get ; set ;
        }
        public virtual Int64 UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        public virtual string ClientIPAddress
        {
            get
            {
                return m_ClientIPAddress;
            }
            set
            {
                m_ClientIPAddress = value;
            }
        }
        public virtual string ClientName
        {
            get
            {
                return m_ClientName;
            }
            set
            {
                m_ClientName = value;
            }
        }

        public virtual string SubjectIdentifier { get; set; }

        public virtual UserCategory UserCategory
        {
            get;
            set;
        }

        public virtual System.Int64 InstitutionID
        {
            get;
            set;
        }
    }
}
