using System;
using System.Reflection;

namespace AyxMVVM.Helper
{
    public class ReflectionHelper
    {
        public static object CreateInstance(Type type,object[] parameters=null)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes);
            return constructor.Invoke(parameters);
        }

        public static T CreateInstance<T>(object[] parameters=null)
        {
            var type = typeof(T);
            return (T)CreateInstance(type);
        }
    }
}
