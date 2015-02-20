using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;


namespace EvoPaj.Base
{
    public class Role : DataObject
    {
        public Role()
        {
        }
        public Role(long id)
        {
            ID = id;
        }
        public virtual string Name { set; get; }
    }
}
