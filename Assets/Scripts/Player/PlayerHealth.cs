using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 100;

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("�÷��̾ ������� ����!!!");
        if(hp <= 0)
        {
            Debug.Log("���!!!!!!!");
        }
    }
}
