using System;
using System.Collections.Generic;
using System.Text;

namespace TestSources.Extensions;

public static class TypeExtensions
{

    /// <summary>
    /// Returns the list of inherited types.
    /// </summary>
    /// <param name="type">The current object type.</param>
    /// <returns>The list of all inherited types.</returns>
    public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
    {
        while (type != null)
        {
            yield return type;
            type = type.BaseType;
        }
    }
}
