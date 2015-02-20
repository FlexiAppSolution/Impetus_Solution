using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.Functions.DAO;
using PANE.Framework.Managed.Functions.DTO;

namespace PANE.Framework.Managed.Functions
{
    public class FunctionsEngine
    {
        public static List<UserRole> GetRoles(string mfbCode)
        {
            return UserRoleDAO.RetrieveAll(mfbCode);
        }
    }
}
