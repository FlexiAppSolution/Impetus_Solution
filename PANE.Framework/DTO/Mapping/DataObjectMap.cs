using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PANE.Framework.Utility;

namespace PANE.Framework.DTO.Mapping
{
    public class DataObjectMap<T> : ClassMap<T> where T : DataObject
    {
        public DataObjectMap()
        {
            if (ConfigurationHelper.ConfigItem<DataObject, bool>("UseAutoIncrement"))
            {
                Id(x => x.ID)
                .GeneratedBy.Increment();
            }
            else
            {
                Id(x => x.ID)
                    .GeneratedBy.Native();
            }
            /**/
        }
    }
}
