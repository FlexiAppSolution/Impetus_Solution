using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PANE.Framework.Managed.Functions.DTO;
using System.Runtime.Serialization;

namespace SOA.Framework.MembershipHelper
{
    [DataContract]
    public class MembershipBranchItem : IBranch
    {
        #region IBranch Members

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public Status Status { get; set; }

        [DataMember]
       public long RegionID { get; set; }

        [DataMember]
        public string GefuFilePath { get; set; }

        #endregion



        #region IDataObject Members

        [DataMember]
        public long ID { get; set; }

        [DataMember]
        public string MFBCode { get; set; }

        #endregion
    }
}
