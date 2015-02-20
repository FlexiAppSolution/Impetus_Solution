using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.Managed.DTO;
using System.Runtime.Serialization;
using PANE.Framework.Managed.AuditTrail.Attributes;

namespace PANE.Framework.Managed.Functions.DTO
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/PANE.Framework.Functions.DTO")]
    //[Loggable]
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
        public virtual long TheUserRoleFunctionID { get; set; }


        /// <summary>
        /// Gets or sets the sub user role.
        /// </summary>
        /// <value>The sub user role.</value>
        [DataMember]
        public virtual long TheSubUserRoleID { get; set; }

    }
}
