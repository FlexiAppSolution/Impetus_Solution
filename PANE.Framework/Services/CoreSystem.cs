using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DAO;

namespace PANE.Framework.Services
{
    public class CoreSystem<T, idT> where T:class
    {
        /// <summary>
        /// Retrieves the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public virtual T Retrieve(idT id)
        {
            return CoreDAO<T, idT>.Retrieve(id);
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> RetrieveAll()
        {
            return CoreDAO<T, idT>.RetrieveAll();
        }

        public virtual List<T> RetrieveAllSorted(string columnName)
        {
            return CoreDAO<T, idT>.RetrieveAllSorted(columnName);
        }

        /// <summary>
        /// Retrieves all.
        /// </summary>
        /// <param name="withDeleted">if set to <c>true</c> [with deleted].</param>
        /// <returns></returns>
        public virtual List<T> RetrieveAll(bool withDeleted)
        {
            return CoreDAO<T, idT>.RetrieveAll(withDeleted);
        }
    }
}
