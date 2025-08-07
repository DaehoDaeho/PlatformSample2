using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// 0 : ��� ����
// 1 : �̵� ����
// 2 : ���� ����
public enum MonsterState
{
    Idle = 0,
    Move = 1,
    Attack = 2
}

public class EnemyMove : MonoBehaviour
{
    public Sprite[] idleSprites;
    public Sprite[] attackSprites;

    private SpriteRenderer sr;  // ��������Ʈ ������ ������Ʈ.    

    public float attackFrameTime = 0.1f; // �� ������ �ð�(��)
    public int attackDamageFrame = 5;    // �� ��° �����ӿ� �����? (0���� ����)
    public int attackDamage = 10;

    private int currentAttackFrame = 0;
    private float attackTimer = 0f;
    private bool damageDealt = false;

    public Transform target;

    private int frame = 0;  // ���� �ִϸ��̼� ������.
    private float timerFrame = 0.0f; // ���� ���۱����� �ð��� ��� ���� Ÿ�̸� ����.
    public float frameRate = 0.15f; // ���� ���ۿ��� ���� ���۱��� �ɸ��� �ð�.

    public float speed = 2.0f;
    public float moveTime = 2.0f;

    private float timer;
    private int dir = 1;    

    MonsterState monsterState = MonsterState.Idle;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMonsterState();
    }

    public void CheckMonsterState()
    {
        //Debug.Log("������ ���� = " + monsterState);
        switch (monsterState)
        {
            case MonsterState.Idle:
                {   
                    Idle();
                }
                break;

            //case MonsterState.Move:
            //    {
            //        Move();
            //    }
            //    break;

            case MonsterState.Attack:
                {
                    HandleAttackAnimation();
                }
                break;
        }
    }

    void Idle()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(myPos, targetPos);
        if (distance <= 2.0f)
        {
            StartAttack();
            Debug.Log("monsterState = " + monsterState);
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= frameRate)
            {
                Sprite[] curArr = idleSprites;
                frame = (frame + 1) % curArr.Length;
                sr.sprite = curArr[frame];
                timer = 0.0f;
            }
        }
    }

    private void Move()
    {
        Vector3 myPos = transform.position;
        Vector3 targetPos = target.position;
        float distance = Vector3.Distance(myPos, targetPos);
        if (distance <= 2.0f)
        {
            StartAttack();
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

    void HandleAttackAnimation()
    {
        attackTimer += Time.deltaTime;

        // ���� ���� �ִϸ��̼����� �Ѿ �ð��� ������
        if (attackTimer >= attackFrameTime)
        {
            attackTimer = 0f;
            currentAttackFrame++;

            if (currentAttackFrame >= attackSprites.Length)
            {
                EndAttack();
                return;
            }

            sr.sprite = attackSprites[currentAttackFrame];

            // ����� ������ üũ
            if (currentAttackFrame == attackDamageFrame && !damageDealt)
            {
                DealDamageToPlayer();
                damageDealt = true;
            }
        }
    }

    public void StartAttack()
    {
        // ���Ͱ� �÷��̾� ĳ���͸� ���ϵ��� ������ ������ �ֱ� ���� �ڵ�.
        sr.flipX = (target.position.x < transform.position.x);

        monsterState = MonsterState.Attack;
        currentAttackFrame = 0;
        attackTimer = 0f;
        damageDealt = false;
        if (attackSprites.Length > 0)
        {
            // ��������Ʈ �������� ù��° ���� �̹����� ����.
            sr.sprite = attackSprites[0];
        }
    }

    void EndAttack()
    {
        currentAttackFrame = 0;
        monsterState = MonsterState.Idle;
        sr.sprite = idleSprites[0];
        timer = 0.0f;
        Debug.Log("monsterState = " + monsterState);
    }

    void DealDamageToPlayer()
    {
        if (target != null)
        {
            // �˹� ����: ���� �� �÷��̾�
            PlayerMove player = target.gameObject.GetComponent<PlayerMove>();
            if(player != null)
            {
                // �÷��̾� ĳ���͸� �˹� ������ ������ ���.
                Vector2 knockDir = (player.transform.position - transform.position).normalized;
                knockDir = (knockDir + Vector2.up * 0.5f).normalized;
                float knockbackForce = 7f;
                float knockbackTime = 0.2f;
                player.TakeDamage(attackDamage, knockDir, knockbackForce, knockbackTime);
            }
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
