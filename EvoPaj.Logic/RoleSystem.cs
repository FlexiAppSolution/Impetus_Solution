using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Data;

namespace EvoPaj.Logic
{
    public class RoleSystem : CoreServices<Role, long>
    {
        public RoleSystem()
        {

        }

        public bool SaveRole(Role theRole)
        {
            bool result = false;
            try
            {
                new RoleDAO().Save(theRole);
                result = true;
            }
            catch(Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}",ex.Message));
                result = false;
                new RoleDAO().RollbackChanges();
            }
            return  result;
        }

        public bool UpdateRole(Role theRole)
        {
            bool result = false;
            try
            {
                new RoleDAO().Update(theRole);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool DeleteRole(Role theRole)
        {
            bool result = false;
            try
            {
                new RoleDAO().Delete(theRole);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool UpdateRole(string query)
        {
            bool result = false;
            try
            {
                new RoleDAO().RunQuery(query);
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;
        }
        
        public bool CommitChanges()
        {
            bool result = false;
            try
            {
                new RoleDAO().CommitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                new PANE.ERRORLOG.Error().LogInfo(string.Format("{0}", ex.Message));
                result = false;
            }
            return result;

        }

        public List<Role> GetRoles()
        {
            return new RoleDAO().RetrieveAll();                       
        }

    }
}
