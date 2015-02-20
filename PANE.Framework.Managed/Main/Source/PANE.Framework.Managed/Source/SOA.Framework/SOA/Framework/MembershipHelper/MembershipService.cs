namespace SOA.Framework.MembershipHelper
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Web.Security;

    [ServiceContract(Namespace=""), AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class MembershipService
    {
        [OperationContract]
        public MembershipUserItem GetUser(string username, bool userIsOnline)
        {
            PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser theUser = Membership.GetUser(username, userIsOnline) as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
            if (theUser == null)
            {
                return null;
            }
            return new MembershipUserItem(theUser);
        }

        [OperationContract]
        public MembershipUserItem GetUserByKey(object providerUserKey, bool userIsOnline)
        {
            PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser theUser = Membership.GetUser(providerUserKey, userIsOnline) as PANE.Framework.Managed.Functions.DTO.FunctionsMembershipUser;
            if (theUser == null)
            {
                return null;
            }
            return new MembershipUserItem(theUser);
        }

        [OperationContract]
        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }
    }
}

