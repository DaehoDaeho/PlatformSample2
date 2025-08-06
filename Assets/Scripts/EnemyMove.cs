using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// 0 : 대기 상태
// 1 : 이동 상태
// 2 : 공격 상태
public enum MonsterState
{
    Idle = 0,
    Move = 1,
    Attack = 2
}

public class EnemyMove : MonoBehaviour
{
    public float speed = 2.0f;
    public float moveTime = 2.0f;

    public Transform target;

    private float timer;
    private int dir = 1;

    MonsterState monsterState = MonsterState.Idle;

    // Update is called once per frame
    void Update()
    {
        CheckMonsterState();
    }

    public void CheckMonsterState()
    {
        //Debug.Log("몬스터의 상태 = " + monsterState);

        switch (monsterState)
        {
            case MonsterState.Idle:
                {   
                    Idle();                    
                }
                break;

            case MonsterState.Move:
                {
                    Move();
                }
                break;

            case MonsterState.Attack:
                {
                    Attack();
                }
                break;
        }
    }

    void Idle()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(myPos, targetPos);
        if (distance <= 1.0f)
        {
            monsterState = MonsterState.Attack;
            Debug.Log("monsterState = " + monsterState);
        }
        else
        {
            monsterState = MonsterState.Move;
            Debug.Log("monsterState = " + monsterState);
        }
    }

    private void Move()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(myPos, targetPos);
        if (distance <= 1.0f)
        {
            monsterState = MonsterState.Attack;
            Debug.Log("monsterState = " + monsterState);
        }
        else
        {
            transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= moveTime)
            {
                dir = dir * -1;
                timer = 0.0f;
            }
        }
    }

    void Attack()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(myPos, targetPos);
        if (distance > 1.0f)
        {
            monsterState = MonsterState.Idle;
            Debug.Log("monsterState = " + monsterState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }
    }
}
