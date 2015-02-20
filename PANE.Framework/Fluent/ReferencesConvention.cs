using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace PANE.Framework.Fluent
{
    public class ReferencesConvention : IReferenceConvention
    {
  #region IConvention<IManyToOneInspector,IManyToOneInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IManyToOneInstance instance)
        {
            instance.Column((instance.Property.Name.StartsWith("The")?instance.Property.Name.Remove(0,3):instance.Property.Name)+"ID");
        }

        #endregion
    }

}
