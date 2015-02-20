using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.DTO;
using System.Runtime.Serialization;
using PANE.Framework.AuditTrail.Attributes;

namespace PANE.Framework.Functions.DTO
{
    [Serializable]
    [DataContract]
    public class UserRoleFunctionSubRole: DataObject
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionSubRole"/> class.
        /// </summary>
        public UserRoleFunctionSubRole()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionSubRole"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public UserRoleFunctionSubRole(long id)
        {
            ID = id;
        }


        /// <summary>
        /// Gets or sets the user role function.
        /// </summary>
        /// <value>The user role function.</value>
        [DataMember]
        public virtual UserRoleFunction TheUserRoleFunction { get; set; }


        /// <summary>
        /// Gets or sets the sub user role.
        /// </summary>
        /// <value>The sub user role.</value>
        [DataMember]
        public virtual UserRole TheSubUserRole { get; set; }

    }
}
