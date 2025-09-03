using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStack<T>
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

    public MyStack(int capacity = 4)
    {
        items = new T[capacity];
        count = 0;
    }

    private void EnsureCapacity(int min)
    {
        int newcapacity = items.Length * 2;
        T[] newArray = new T[newcapacity];
        for(int i=0; i<count; ++i)
        {
            newArray[i] = items[i];
        }

        items = newArray;
    }

    public void Push(T item)
    {
        EnsureCapacity(count + 1);
        items[count] = item;
        ++count;
    }

    public T Pop()
    {
        --count;
        T value = items[count];
        items[count] = default(T);
        return value;
    }

    public T Peek()
    {
        return items[count - 1];
    }
}
