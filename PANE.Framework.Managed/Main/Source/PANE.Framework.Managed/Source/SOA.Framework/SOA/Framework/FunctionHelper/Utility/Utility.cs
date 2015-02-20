using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOA.Framework.FunctionHelper
{
    public static class Utility
    {
        public static FS.UserCategory ConvertCategoryFrom(PANE.Framework.Managed.Functions.DTO.UserCategory userCategory)
        {
            switch (userCategory)
            {
                case PANE.Framework.Managed.Functions.DTO.UserCategory.Customer:
                    return FS.UserCategory.Customer;
                case PANE.Framework.Managed.Functions.DTO.UserCategory.Mfb:
                    return FS.UserCategory.Mfb;
                case PANE.Framework.Managed.Functions.DTO.UserCategory.CorrespondentBank:
                //case PANE.Framework.Managed.Functions.DTO.UserCategory.SchemeOwner:
                    return FS.UserCategory.CorrespondentBank;
                case PANE.Framework.Managed.Functions.DTO.UserCategory.ServiceProvider:
                    return FS.UserCategory.ServiceProvider;

                default:
                    return FS.UserCategory.Customer;
            }
        }
    }
}
