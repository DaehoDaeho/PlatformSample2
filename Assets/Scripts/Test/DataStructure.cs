using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure : MonoBehaviour
{
    int[] number;
    List<int> number2;

    Stack<int> stackNumber;
    Queue<int> queueNumber;

    Dictionary<string, string> dicData;

    // Start is called before the first frame update
    void Start()
    {
        MyDictionary<string, int> myDic = new MyDictionary<string, int>();
        Debug.Log("count = " + myDic.Count);
        myDic.Add("one", 1);
        Debug.Log("count = " + myDic.Count);
        myDic.Add("two", 2);
        myDic.Add("three", 3);
        Debug.Log("count = " + myDic.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
