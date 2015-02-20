using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PANE.Framework.Functions.DTO;
using PANE.Framework.Managed.AuditTrail.Attributes;

namespace PANE.Framework.DTO
{
   
    public interface IEntity
    {
        string Name { get;}
    }
    
    public interface IApprovalSubList
    {
        IList SubList { get; }
    }

   
    public interface IAuditable
    {
        string ActionInitiatorUserName { get; set; }
        long ActionInitiatorUserID { get; set; }
        bool IsActionInitiatorSet { get; set; }
        UserCategory TheUserCategory { get; set; }
        long? TheInstitutionID { get; set; }
    }
}
