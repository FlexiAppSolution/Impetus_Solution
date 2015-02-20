using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Security;
using PANE.Framework.Managed.Functions.DTO;
using System.Reflection;
using PANE.Framework.Managed.DTO;
using PANE.Framework.Managed.Approval.DTO;

namespace SOA.Framework.FunctionHelper
{
    [DataContract]
    public class FunctionItem : DataObject
    {
        public FunctionItem()
        {
        }

        public FunctionItem(Function function) {
            this.Description = function.Description;
            this.HasSubRoles = function.HasSubRoles;
            this.ID = function.ID;
            this.IsApprovable = function.IsApprovable;
            this.IsDeleted = function.IsDeleted;
            this.MFBCode = function.MFBCode;
            this.Name = function.Name;
            this.ParentFunctionItem = new FunctionItem(function.ParentFunction);
            this.RoleName = function.RoleName;
            this.InstitutionID = function.InstitutionID;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public virtual string Description { get; set; }


        /// <summary>
        /// Gets or sets the parent function item.
        /// </summary>
        /// <value>The parent function.</value>
        [DataMember]
        public virtual FunctionItem ParentFunctionItem { get; set; }


        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>The name of the role.</value>
        [DataMember]
        public virtual string RoleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has sub roles.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has sub roles; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public virtual Boolean HasSubRoles { get; set; }

        [DataMember]
        public virtual IList<SOA.Framework.ApprovalConfigHelper.ApprovalConfig> TheApprovalConfigurations { get; set; }

        [DataMember]
        public virtual SOA.Framework.ApprovalConfigHelper.ApprovalConfig ApprovalConfigurationUpdate { get; set; }

        [DataMember]
        public virtual UserRole SubUserRoleUpdate { get; set; }

        [DataMember]
        public virtual bool Dependency { get; set; }

        [DataMember]
        public virtual bool IsApprovable { get; set; }

        [DataMember]
        public virtual UserCategory UserCategory { get; set; }

        [DataMember]
        public virtual bool IsDefault { get; set; }
        
        [DataMember]
        public virtual long? InstitutionID { get; set; }
    }
}
