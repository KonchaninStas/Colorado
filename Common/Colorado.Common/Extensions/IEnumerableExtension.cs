using System;
using System.Collections.Generic;

namespace Colorado.Common.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            foreach (T item in sequence)
            {
                action(item);
            }
        }
    }
}
