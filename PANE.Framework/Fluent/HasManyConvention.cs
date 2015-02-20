using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace PANE.Framework.Fluent
{
    public class HasManyConvention : IHasManyConvention
    {
#region IConvention<IOneToManyCollectionInspector,IOneToManyCollectionInstance> Members

public void  Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
{
    instance.Key.Column((instance.EntityType.Name.StartsWith("The") ? instance.EntityType.Name.Remove(0, 3) : instance.EntityType.Name) + "ID");
            //target.Cascade.None();//.AllDeleteOrphan();
            //target.AsBag();
            //target.SetAttribute("insert", "false");
            //target.SetAttribute("update", "false");
            instance.Inverse();
}

#endregion
}
}
