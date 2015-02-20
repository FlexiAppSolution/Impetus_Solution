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
    [DataContract]
    [Serializable]
    [Loggable]
    public class UserRole: DataObject, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        public UserRole()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public UserRole(long id)
        {
            ID = id;
        }

        ///// <summary>
        ///// Gets or sets the name.
        ///// </summary>
        ///// <value>The name.</value>
        //[DataMember]
        //public virtual string Name { get; set; }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        [Loggable]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        /// <value>The scope.</value>
        [DataMember]
        [Loggable]
        public virtual UserRoleScope Scope { get; set; }

        /// <summary>
        /// Gets or sets the UserCategory.
        /// </summary>
        /// <value>The UserCategory.</value>
        [DataMember]
        [Loggable]
        public virtual UserCategory UserCategory { get; set; }

        [DataMember]
        [Loggable]
        public virtual Int64? InstitutionID { get; set; }

        [DataMember]
        [Loggable]
        public virtual Status Status { get; set; }

        [DataMember]
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the user role functions.
        /// </summary>
        /// <value>The user role functions.</value>
        [XmlIgnore]
        
        public virtual IList<UserRoleFunction> TheUserRoleFunctions { get; set; }
        
        public virtual string TheFunctions
        {
            get
            {
                if (TheUserRoleFunctions != null)
                {
                    StringBuilder builder = new StringBuilder();
                    //foreach (UserRoleFunction urf in TheUserRoleFunctions)
                    //{
                    //    builder.AppendFormat("<br>{0}|",  Functions.DAO.FunctionDAO.Retrieve( urf.TheFunction.ID  ).Name );

                    //}
               foreach (UserRoleFunction urf in TheUserRoleFunctions)
                {
                   builder.AppendFormat("{0}|",(string.IsNullOrEmpty(urf.TheFunction.Name)?urf.Name:urf.TheFunction.Name));
                }
                    return builder.ToString();
                }
                return null;
            }
        }
        [Loggable("The Functions")]
        public virtual  string AssignedFunctions
        {
            get;
            set;
        }
      
    }
}
