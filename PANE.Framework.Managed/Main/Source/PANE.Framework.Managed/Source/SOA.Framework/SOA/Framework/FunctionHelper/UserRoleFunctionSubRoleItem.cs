using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;
using System.Runtime.Serialization;

namespace SOA.Framework.FunctionHelper
{
    [DataContract]
    public class UserRoleFunctionSubRoleItem: DataObject
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionSubRoleItem"/> class.
        /// </summary>
        public UserRoleFunctionSubRoleItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionSubRoleItem"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public UserRoleFunctionSubRoleItem(long id)
        {
            ID = id;
        }


        /// <summary>
        /// Gets or sets the user role function item.
        /// </summary>
        /// <value>The user role function item.</value>
        [DataMember]
        public virtual UserRoleFunctionItem TheUserRoleFunctionItem { get; set; }
        //public virtual long TheUserRoleFunctionItemID { get; set; }


        /// <summary>
        /// Gets or sets the sub user role.
        /// </summary>
        /// <value>The sub user role id.</value>
        [DataMember]
        public virtual long TheSubUserRoleID { get; set; }

    }
}
