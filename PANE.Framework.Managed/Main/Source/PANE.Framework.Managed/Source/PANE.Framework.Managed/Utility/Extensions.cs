using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PANE.Framework.Managed.Utility
{
    public static class Extensions
    {
        public static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
        
    }
}
