
namespace SOA.Framework.SessionHelper
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
    using System.Web.UI.WebControls.WebParts;
    [ServiceContract(Namespace = ""), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SessionService
    {
        [OperationContract]
        public bool isUserLoggedin(string username)
        {
            bool isLoggedin = false;
            string[] userinfo = username.Split(':');
            if (userinfo.Length >= 3)
            {
                string appkey = userinfo[0] + ":" + userinfo[1];

                if (HttpContext.Current.Application[appkey] != null)
                {
                    if (HttpContext.Current.Application[appkey].ToString() == userinfo[2])
                    {
                        isLoggedin = true;
                    }
                }
            }

            return isLoggedin;
        }

        /// <summary>
        /// Indicates that a user from a new Institution e.g MFB just logged in so we need to clear the session of
        /// the previous Institution and work with the new one.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        public bool IsADifferentInstitution(string username)
        {
            bool isDifferent = false;
            string[] userinfo = username.Split(':');
            if (userinfo.Length >= 3)
            {
                if (HttpContext.Current.Session[SS_MFBCODE] == null)
                {
                    if (HttpContext.Current.Application[SS_MFBCODE].ToString().ToLower() != userinfo[1].ToLower())
                    {
                        isDifferent = true;
                    }
                }
            }

            return isDifferent;
        }

        private const string SS_MFBCODE = "::SS_INSTITUTION_CODE::";
    }
}
