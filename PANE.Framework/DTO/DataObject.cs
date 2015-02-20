using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using FluentNHibernate.Data;
using PANE.Framework.AuditTrail.Attributes;
using PANE.Framework.Managed.AuditTrail.Attributes;


 //using NHibernate.Mapping.Attributes;

namespace PANE.Framework.DTO
{
    [Serializable]
    [DataContract]    
    public abstract class DataObject : IAuditable, IEntity
    {
        [TrailableIgnore]
        public virtual long ActionInitiatorUserID { get; set; }

        [TrailableIgnore]
        public virtual string ActionInitiatorUserName { get; set; }

         [TrailableIgnore]
        public virtual bool IsActionInitiatorSet { get; set; } 
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [DataMember]
       // [Id(UnsavedValue="0", TypeType=typeof(Int64))]
       /// [Generator(1, Class="native")]
         ///  [TrailableIgnore]
        [TrailableIgnore]
        public virtual Int64 ID { get; set; }
        
        [TrailableIgnore]
        public virtual Int64 HiID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        [TrailableIgnore]
        public virtual System.Boolean IsDeleted { get; set; }
         [TrailableIgnore]
        public virtual System.Boolean LogObject { get; set; }

         //[TrailableIgnore]
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is DataObject)
            {
                if (this.ID == (obj as DataObject).ID)
                {
                    result = true;
                }
            }
            return result;
        }

        [Loggable]
        [DataMember]
        [TrailableIgnore]
        public virtual string Name { get; set; }
         [TrailableIgnore]
        public virtual PANE.Framework.Functions.DTO.UserCategory TheUserCategory { get; set; }
         [TrailableIgnore]
        public virtual long? TheInstitutionID { get; set; }
         [TrailableIgnore]
        public virtual string ApprovalMessage { get; set; }
        // [TrailableIgnore]
        public override string ToString()
        {
            if (String.IsNullOrEmpty(Name))
            {
                return base.ToString();
            }
            return Name;
        }
         [TrailableIgnore]
        public virtual bool OmitApprovalLog
        {
            get;
            set;
        }         
        public DataObject()
        {
            TheUserCategory = PANE.Framework.Functions.DTO.UserCategory.ServiceProvider;
        }
    }

}
