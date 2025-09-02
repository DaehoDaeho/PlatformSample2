using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyArray<T>
{
    private T[] data;

    public int Length
    {
        // get 속성. 
        get
        {
            return data.Length;
        }
    }

    public MyArray(int length)  // 생성자. (Constructor)
    {
        data = new T[length];
    }

    public T this[int index]    // this 클래스 자신을 의미함.
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
