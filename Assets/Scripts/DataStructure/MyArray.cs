using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArray<T>
{
    private T[] data;

    public int Length
    {
        // get �Ӽ�. 
        get
        {
            return data.Length;
        }
    }

    public MyArray(int length)  // ������. (Constructor)
    {
        data = new T[length];
    }

    public T this[int index]    // this Ŭ���� �ڽ��� �ǹ���.
    {
        get
        {
            return data[index];
        }
        set
        {
            data[index] = value;
        }
    }
}
