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
        number = new int[100];
        //Debug.Log("number count = " + number.Length);
        number[0] = 10;
        number[1] = 20;
        number[99] = 30;
        //number[100] = 30;

        for(int i=0; i<number.Length; ++i)
        {
            if (number[i] != 0)
            {
                //Debug.Log("Number[" + i + "] = " + number[i]);
            }
        }

        number2 = new List<int>();
        //Debug.Log("Size of number2 = " + number2.Count);
        number2.Add(5);
        //Debug.Log("Size of number2 = " + number2.Count);
        number2.Remove(5);
        //Debug.Log("Size of number2 = " + number2.Count);
        number2.Add(1);
        number2.Add(3);
        number2.Add(5);
        number2.Add(7);
        number2.Add(9);
        //Debug.Log("Size of number2 = " + number2.Count);
        //Debug.Log("number2[2] = " + number2[2]);
        number2.RemoveAt(0);
        //Debug.Log("Size of number2 = " + number2.Count);
        //Debug.Log("number2[0] = " + number2[0]);

        stackNumber = new Stack<int>();
        stackNumber.Push(1);
        stackNumber.Push(2);
        stackNumber.Push(3);
        stackNumber.Push(4);

        //Debug.Log("Use First Data = " + stackNumber.Peek());
        //Debug.Log("Use First Data = " + stackNumber.Pop());
        //Debug.Log("Use First Data = " + stackNumber.Peek());

        queueNumber = new Queue<int>();
        queueNumber.Enqueue(1);
        queueNumber.Enqueue(3);
        queueNumber.Enqueue(5);
        queueNumber.Enqueue(7);
        queueNumber.Enqueue(9);

        //Debug.Log("Use First Data = " + queueNumber.Peek());
        //Debug.Log("Use First Data = " + queueNumber.Dequeue());
        //Debug.Log("Use First Data = " + queueNumber.Peek());

        dicData = new Dictionary<string, string>();
        dicData.Add("one", "Data one");
        dicData.Add("two", "Data two");
        dicData.Add("three", "Data three");
        Debug.Log(dicData["three"]);
        dicData["three"] = "data four";
        Debug.Log(dicData["three"]);
        Debug.Log("Dictionary count = " + dicData.Count);
        dicData.Remove("three");
        Debug.Log("Dictionary count = " + dicData.Count);
        string strValue;
        dicData.TryGetValue("two", out strValue);
        Debug.Log("strValue = " + strValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
