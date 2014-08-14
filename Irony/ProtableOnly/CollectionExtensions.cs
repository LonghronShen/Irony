using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{

    public static class CollectionExtensions
    {

        public static int FindIndex<T>(this IList<T> collection, Func<T, bool> filter)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (filter(collection[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static bool ContainsKey<TValue>(this IDictionary<string, TValue> dict, string key, bool ignoreCase)
        {
            if (!ignoreCase)
            {
                return dict.ContainsKey(key);
            }
            else
            {
                return dict.Keys.Count(item => string.Equals(item, key, StringComparison.CurrentCultureIgnoreCase)) > 0;
            }
        }

    }

}
