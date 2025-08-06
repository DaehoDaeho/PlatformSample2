using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public Monster[] monster;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<monster.Length; ++i)
        {
            monster[i].Attack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
