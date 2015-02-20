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
    public class Help : DataObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        public Help()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Help(long id)
        {
            ID = id;
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
        /// Gets or sets the UserGuideUrl.
        /// </summary>
        /// <value>The UserGuideUrl.</value>
        [DataMember]
        public virtual string UserGuideUrl { get; set; }

        /// <summary>
        /// Gets or sets the DemoUrl.
        /// </summary>
        /// <value>The DemoUrl.</value>
        [DataMember]
        public virtual string DemoUrl { get; set; }
    }
}
