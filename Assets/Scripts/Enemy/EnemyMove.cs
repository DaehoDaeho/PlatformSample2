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
    public Sprite[] idleSprites;
    public Sprite[] attackSprites;

    private SpriteRenderer sr;  // 스프라이트 렌더러 컴포넌트.    

    public float attackFrameTime = 0.1f; // 각 프레임 시간(초)
    public int attackDamageFrame = 5;    // 몇 번째 프레임에 대미지? (0부터 시작)
    public int attackDamage = 10;

    private int currentAttackFrame = 0;
    private float attackTimer = 0f;
    private bool damageDealt = false;

    public Transform target;

    private int frame = 0;  // 현재 애니메이션 프레임.
    private float timerFrame = 0.0f; // 다음 동작까지의 시간을 재기 위한 타이머 변수.
    public float frameRate = 0.15f; // 현재 동작에서 다음 동작까지 걸리는 시간.

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
        //Debug.Log("몬스터의 상태 = " + monsterState);
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

        // 다음 공격 애니메이션으로 넘어갈 시간이 됐으면
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

            // 대미지 프레임 체크
            if (currentAttackFrame == attackDamageFrame && !damageDealt)
            {
                DealDamageToPlayer();
                damageDealt = true;
            }
        }
    }

    public void StartAttack()
    {
        // 몬스터가 플레이어 캐릭터를 향하도록 방향을 설정해 주기 위한 코드.
        sr.flipX = (target.position.x < transform.position.x);

        monsterState = MonsterState.Attack;
        currentAttackFrame = 0;
        attackTimer = 0f;
        damageDealt = false;
        if (attackSprites.Length > 0)
        {
            // 스프라이트 렌더러에 첫번째 동작 이미지를 세팅.
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
            // 넉백 방향: 몬스터 → 플레이어
            PlayerMove player = target.gameObject.GetComponent<PlayerMove>();
            if(player != null)
            {
                // 플레이어 캐릭터를 넉백 시켜줄 방향을 계산.
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
