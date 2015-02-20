using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.DTO;
using System.Runtime.Serialization;
using SOA.Framework.FunctionHelper;
using System.Xml.Serialization;

namespace SOA.Framework.FunctionHelper
{
    [DataContract]
    public class UserRoleFunctionItem : DataObject
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionItem"/> class.
        /// </summary>
        public UserRoleFunctionItem()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleFunctionItem"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public UserRoleFunctionItem(long id)
        {
            ID = id;
        }

        /// <summary>
        /// Gets or sets the function item ID.
        /// </summary>
        /// <value>The function item ID.</value>
        [DataMember]
        public virtual long TheFunctionItemID { get; set; }

        /// <summary>
        /// Gets or sets the user role id.
        /// </summary>
        /// <value>The user role id.</value>
        [DataMember]
        public virtual long TheUserRoleID { get; set; }

        //[DataMember]
        //public virtual string Endpoint { get; set; }
        /// <summary>
        /// Gets or sets the sub user roles.
        /// </summary>
        /// <value>The sub user roles.</value>
        [XmlIgnore]
        public virtual IList<UserRoleFunctionSubRoleItem> SubUserRoleItems { get; set; }
    }
}
