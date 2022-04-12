using System;
using System.Reflection;

namespace OneDay.Core.Inject
{
    public static class Utils
    {
        public static PropertyInfo[] ListOfPropertiesFromInstance(object InstanceOfAType)
        {
            if (InstanceOfAType == null)
                return null;
            Type TheType = InstanceOfAType.GetType();
            return TheType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}
