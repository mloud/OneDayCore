using System.Collections.Generic;
using System.Linq;

public static class ContainerExtensions
{
    public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
    {
        int index = UnityEngine.Random.Range(0, enumerable.Count());
        return enumerable.ElementAt(index);
    }
}