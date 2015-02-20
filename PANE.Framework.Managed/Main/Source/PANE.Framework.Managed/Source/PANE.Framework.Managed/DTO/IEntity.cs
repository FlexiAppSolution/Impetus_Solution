using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PANE.Framework.Managed.DTO
{
    public interface IEntity
    {
        string Name { get;  }
    }

    public interface IApprovalSubList
    {
        IList SubList { get; }
    }
}
