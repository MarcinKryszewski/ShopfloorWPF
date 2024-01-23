using System;
using System.Reflection;

namespace Shopfloor.Utilities
{
    internal static class ConstantVariablesCounter
    {
        public static int CountConstantVariablesOfClass<T>()
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            int constantsCount = 0;

            foreach (FieldInfo field in fields)
            {
                if (field.IsLiteral && !field.IsInitOnly)
                {
                    constantsCount++;
                }
            }
            return constantsCount;
        }
    }
}