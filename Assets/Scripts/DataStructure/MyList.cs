using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyList<T>
{
    private T[] items;
    private int count;
    public int Count
    {
        get
        {
            return count;
        }
    }

    public MyList(int capacity = 4) // 디폴트 인자. 디폴트 파라미터 (Default Parameter)
    {
        items = new T[capacity];
        count = 0;
    }

    public T this[int index]
    {
        get
        {
            return items[index];
        }
        set
        {
            items[index] = value;
        }
    }

    public void Add(T item)
    {
        EnsureCapacity(count + 1);
        items[count] = item;
        ++count;
    }

    private void EnsureCapacity(int min)
    {
        if (items.Length >= min)
        {
            return;
        }

        int newCapacity = items.Length * 2;

        if(newCapacity < min)
        {
            newCapacity = min;
        }

        T[] newArray = new T[newCapacity];
        for(int i=0; i<count; ++i)
        {
            newArray[i] = items[i];
        }
        items = newArray;
    }

    public void RemoveAt(int index)
    {
        for(int i = index; i < count - 1; ++i)
        {
            items[i] = items[i + 1];
        }

        items[count - 1] = default(T);
        --count;
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if(index >= 0)
        {
            RemoveAt(index);
            return true;
        }

        return false;
    }

    public int IndexOf(T item)
    {
        EqualityComparer<T> cmp = EqualityComparer<T>.Default;
        for(int i=0; i<count; ++i)
        {
            if (cmp.Equals(items[i], item) == true)
            {
                return i;
            }
        }

        return -1;
    }

    public bool Contains(T item)
    {
        if(IndexOf(item) >= 0)
        {
            return true;
        }

        return false;
    }

    public void Insert(int index, T item)
    {
        EnsureCapacity(count + 1);

        for(int i=count; i>index; --i)
        {
            items[i] = items[i - 1];
        }

        items[index] = item;
        ++count;
    }
}
