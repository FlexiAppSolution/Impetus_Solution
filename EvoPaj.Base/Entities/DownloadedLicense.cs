using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.DTO;
using PANE.Framework.Functions.DTO;

namespace EvoPaj.Base
{
    public class DownloadedLicense : DataObject, IEntity
    {
        private User _user = new User();

        private License _license = new License();

        public virtual System.Int64 NumberOfUsedLicense { get; set; }

        public virtual System.DateTime? DateDownloaded { get; set; }

        public virtual User TheUser { set { _user = value; } get { return _user; } }

        public virtual License TheLicense { set { _license = value; } get { return _license; } }
    }
}
