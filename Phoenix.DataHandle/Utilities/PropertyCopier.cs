using System;
using System.Linq;
using System.Reflection;

namespace Phoenix.DataHandle.Utilities
{
    public static class PropertyCopier
    {
        public static void CopyFromBase<TTo, TFrom>(TTo to, TFrom from)
            where TTo : TFrom
        {
            if (to is null)
                throw new ArgumentNullException(nameof(to));
            if (from is null)
                throw new ArgumentNullException(nameof(from));

            var toProps = typeof(TTo).GetProperties();
            var fromProps = typeof(TFrom).GetProperties();

            PropertyInfo? toProp;
            foreach (var fromProp in fromProps)
            {
                toProp = toProps.FirstOrDefault(
                    p => p.Name == fromProp.Name && p.PropertyType == fromProp.PropertyType);
                if (toProp is null || !toProp.CanWrite)
                    continue;

                toProp.SetValue(to, fromProp.GetValue(from));
            }
        }
    }
}
