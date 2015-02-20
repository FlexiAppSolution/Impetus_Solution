using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Managed.NHibernateManager.Configuration
{
    public enum DatabaseSource
    {
        /// <summary>
        /// Used for Databases on PANE's end.
        /// </summary>
        Local = 0,
        /// <summary>
        /// Used for databases on the institutions' end.
        /// </summary>
        Remote,

        /// <summary>
        /// Another kind of databases on PANEs' end. A special type though.
        /// </summary>
        Core
    }
}
