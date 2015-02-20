using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SOA.Framework.MembershipHelper;

namespace SOA.Framework
{
    internal static class Utility
    {
        public static void SetCurrentUser(string username)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[ManagedMembershipProvider.HC_SERVICE_USER] = username;
            }
        }
    }
}
