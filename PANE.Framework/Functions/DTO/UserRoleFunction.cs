using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PANE.Framework.DTO;
using System.Runtime.Serialization;
using PANE.Framework.AuditTrail.Attributes;
using System.Xml.Serialization;

namespace PANE.Framework.Functions.DTO
{
    [Serializable]
    [DataContract]
    public class UserRoleFunction: DataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunction"/> class.
        /// </summary>
        public UserRoleFunction()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunction"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public UserRoleFunction(long id)
        {
            ID = id;
        }

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>The function.</value>
        [DataMember]
        public virtual Function TheFunction { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>The user role.</value>
        [DataMember]
        public virtual UserRole TheUserRole { get; set; }

        /// <summary>
        /// Gets or sets the sub user roles.
        /// </summary>
        /// <value>The sub user roles.</value>
        [XmlIgnore]
        public virtual IList<UserRoleFunctionSubRole> SubUserRoles { get; set; }

        public override bool Equals(object obj)
        {
            UserRoleFunction theOther = obj as UserRoleFunction;
            if (theOther != null)
            {
                if (theOther.TheFunction == this.TheFunction && theOther.TheUserRole == this.TheUserRole)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
