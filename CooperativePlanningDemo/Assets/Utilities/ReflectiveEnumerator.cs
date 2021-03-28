using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
  public static class ReflectiveEnumerator
  {
    static ReflectiveEnumerator() { }

    public static IEnumerable<Type> DerivedTypes<T>(params object[] constructorArgs) where T : class
    {
      return Assembly.GetAssembly(typeof(T)).GetTypes()
        .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));
    }
  }
}