using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject[] obj;
    List<GameObject> listObj = new List<GameObject>();

    private void Start()
    {
        for(int i=0; i<obj.Length; ++i)
        {
            GameObject go = Instantiate(obj[i], new Vector3(i * 2, 1, 0), Quaternion.identity);
            listObj.Add(go);
        }

        for(int i=0; i<listObj.Count; ++i)
        {
            Debug.Log("오브젝트 이름 : " + listObj[i].name);
        }
    }
}
