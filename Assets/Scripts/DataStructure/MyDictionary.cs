using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDictionary<TKey, TValue>
{
    private class Entry
    {
        public int HashCode;
        public TKey Key;
        public TValue Value;
        public Entry Next;
    }

    private Entry[] buckets;
    private int count;
    private int threshold;
    private const float LoadFactor = 0.75f;
    // const�� �ڷ����� �տ� ������ �� ������ ������ �� �� ����. ���.

    public int Count
    {
        get
        {
            return count;
        }
    }

    private int NextPowerOfTwo(int v)
    {
        --v;
        v |= v >> 1;    // >> ����Ʈ ������. v ���� 1ĭ ���������� �̵�. v�� 2�� 2�� �������� 0010
        v |= v >> 2;    // 10 -> 1010 -> 0010
        v |= v >> 4;
        v |= v >> 8;
        v |= v >> 16;
        ++v;
        return v;
    }

    public MyDictionary(int capacity = 8)
    {
        capacity = NextPowerOfTwo(capacity);    // �ŵ������� ���ϴ� ����.
        buckets = new Entry[capacity];
        count = 0;
        threshold = (int)(capacity * LoadFactor);
    }

    private void Resize(int newSize)
    {
        newSize = NextPowerOfTwo(newSize);
        Entry[] newBuckets = new Entry[newSize];

        for(int i=0; i<buckets.Length; ++i)
        {
            Entry Node = buckets[i];
            while(Node != null)
            {
                Entry next = Node.Next;
                int index = Node.HashCode & (newSize - 1);
                Node.Next = newBuckets[index];
                newBuckets[index] = Node;
                Node = next;
            }
        }

        buckets = newBuckets;
        threshold = (int)(newSize * LoadFactor);
    }

    public void Add(TKey key, TValue value)
    {
        if(count + 1 > threshold)
        {
            Resize(buckets.Length * 2);
        }

        int hash = GetHash(key);
        int index = hash & (buckets.Length - 1);

        Entry node = buckets[index];
        while(node != null)
        {
            node = node.Next;
        }

        Entry newEntry = new Entry();
        newEntry.HashCode = hash;
        newEntry.Key = key;
        newEntry.Value = value;
        newEntry.Next = buckets[index];
        buckets[index] = newEntry;
        ++count;
    }
        
    private int GetHash(TKey key)
    {
        int h = EqualityComparer<TKey>.Default.GetHashCode(key);
        return h & 0x7FFFFFFF;
    }
}
