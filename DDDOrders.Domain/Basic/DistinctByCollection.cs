using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DDDOrders.Domain.Basic;
public class DistinctByCollection<TItem, TKey> : IList<TItem>
{
    private List<TItem> _items = new List<TItem>();
    private Func<TItem, TKey> _predicate;

    public DistinctByCollection(Func<TItem, TKey> keyPredicate)
    {
        _predicate = keyPredicate;
    }

    public TItem this[int index] { get => _items[index]; set => throw new NotSupportedException(); }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(TItem item)
    {
        var dict = _items.ToDictionary(_predicate);
        if (!dict.ContainsKey(_predicate(item)))
        {
            _items.Add(item);
        }
        else
        {
            throw new ArgumentException("Unique key restriction violated");
        }
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(TItem item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(TItem[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<TItem> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(TItem item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, TItem item)
    {
        throw new NotSupportedException();
    }

    public bool Remove(TItem item)
    {
        return _items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.AsEnumerable().GetEnumerator();
    }
}
