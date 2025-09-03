using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQueue<T>
{
    private T[] items;
    private int head;
    private int tail;
    private int count;
    public int Count
    {
        get
        {
            return count;
        }
    }

    public MyQueue(int capacity = 4)
    {
        items = new T[capacity];
        head = 0;
        tail = 0;
        count = 0;
    }

    private void EnsureCapacity(int min)
    {
        int newcapacity = items.Length * 2;
        T[] newArray = new T[newcapacity];
        for (int i = 0; i < count; ++i)
        {
            newArray[i] = items[(head + i) % items.Length];
        }

        items = newArray;
        head = 0;
        tail = count;
    }

    public void Enqueue(T item)
    {
        EnsureCapacity(count + 1);
        items[tail] = item;
        tail = (tail + 1) % items.Length;
        ++count;
    }

    public T Dequeue()
    {
        T Value = items[head];
        items[head] = default(T);
        head = (head + 1) % items.Length;

        --count;

        return Value;
    }

    public T Peek()
    {
        return items[head];
    }
}
