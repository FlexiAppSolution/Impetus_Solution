using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Managed.ISO8583.DTO
{
    public enum MessageEntity
    {
        Client = 1,
        Server = 2,
        ExternalEntity = 3,
        Bridge = 4
    }
}
