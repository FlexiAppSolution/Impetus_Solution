using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace PANE.Framework.Fluent
{
    public class ClassMappingConvention : IClassConvention
    {

     
        public static string PluralOf(string text)
        {
            var pluralString = text;
            var lastCharacter = pluralString.Substring(pluralString.Length - 1).ToLower();

            // y’s become ies (such as Category to Categories)
            if (string.Equals(lastCharacter, "y", StringComparison.InvariantCultureIgnoreCase) && !pluralString.EndsWith("ay", StringComparison.InvariantCultureIgnoreCase))
            {
                pluralString = pluralString.Remove(pluralString.Length - 1);
                pluralString += "ie";
            }

            // ch’s become ches (such as Pirch to Pirches)
            if (string.Equals(pluralString.Substring(pluralString.Length - 2), "ch",
                              StringComparison.InvariantCultureIgnoreCase))
            {
                pluralString += "e";
            }

            switch (lastCharacter)
            {
                case "s":
                    return pluralString + "es";

                default:
                    return pluralString + "s";

            }

        }


        #region IConvention<IClassInspector,IClassInstance> Members

        public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
        {
            instance.Table(PluralOf(instance.EntityType.Name));
        }

        #endregion
    }


}
