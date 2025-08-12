using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("플레이어가 대미지를 입음!!!");
        if(hp <= 0)
        {
            Debug.Log("사망!!!!!!!");
        }
    }
}
