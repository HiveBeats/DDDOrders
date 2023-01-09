using System;
using System.Collections.Generic;

namespace DDDOrders.Domain.Extensions;
public static class DictionaryExtensions
{
    public static IDictionary<K, List<V>> ToListDictionary<K, V>(this IList<V> items, Func<V, K> keySelector)
    {
        var dict = new Dictionary<K, List<V>>();
        foreach (var item in items)
        {
            if (!dict.ContainsKey(keySelector(item)))
            {
                dict.Add(keySelector(item), new List<V>() { item });
            }
            else
            {
                dict[keySelector(item)].Add(item);
            }
        }
        return dict;
    }
}
