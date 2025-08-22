// Assets/Scripts/Enemy/EnemyAIController.cs
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    // --- 상태 ---
    private enum State { Patrol, Chase, Attack }
    private State state = State.Patrol;

    // --- 이동/감지 파라미터 ---
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 2.5f;
    public float visionRange = 6f;    // 이 거리 안으로 오면 추격 시작
    public float attackRange = 0.8f;  // 이 거리 안이면 공격 시도

    // --- 공격 타이밍 ---
    public float attackActiveTime = 0.15f; // 공격 트리거 켜두는 시간
    public float attackCooldown = 0.50f; // 다음 공격까지 대기
    private float attackActiveUntil = 0f;
    private float nextAttackAt = 0f;

    // --- 센서(절벽/벽) ---
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f; // 발밑 짧게
    public float edgeCheckDistance = 0.3f; // 앞쪽 발끝 짧게(절벽 체크)
    public float wallCheckDistance = 0.1f; // 정면 짧게(벽 체크)

    // --- 참조 ---
    public Transform attackZone;            // 자식(히트박스) 루트
    private Collider2D attackTrigger;       // 자식의 Trigger 콜라이더
    private Rigidbody2D rb;
    private Transform player;

    // --- 내부 상태 ---
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (attackZone != null)
        {
            attackTrigger = attackZone.GetComponent<Collider2D>();
        }

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        // 시작 시 공격 트리거는 꺼둠
        if (attackTrigger != null)
        {
            attackTrigger.enabled = false;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // 1) 상태 전이 결정
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (state != State.Attack) // 공격 중에는 상태 바꾸지 않음(짧은 시간)
        {
            if (distToPlayer <= attackRange && Time.time >= nextAttackAt)
            {
                state = State.Attack;
            }
            else if (distToPlayer <= visionRange)
            {
                state = State.Chase;
            }
            else
            {
                state = State.Patrol;
            }
        }

        // 2) 상태별 행동
        if (state == State.Patrol)
        {
            PatrolMove();
        }
        else if (state == State.Chase)
        {
            ChaseMove();
        }
        else if (state == State.Attack)
        {
            DoAttack();
        }

        // 3) 공격 트리거 ON/OFF 유지
        if (attackTrigger != null)
        {
            if (Time.time <= attackActiveUntil)
            {
                attackTrigger.enabled = true;
                // 공격 중에는 살짝 멈추게(또는 느리게) 해도 좋다
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else
            {
                attackTrigger.enabled = false;
            }
        }

        // 4) 디버그 시각화(원한다면): 감지/공격 반경
        DebugDrawRanges();
    }

    // --- 순찰 ---
    private void PatrolMove()
    {
        float dir = facingRight ? 1f : -1f;

        // 절벽 체크: 앞쪽 발끝 아래로 Ray
        Vector2 originEdge = (Vector2)transform.position + new Vector2(0.3f * dir, -0.15f);
        bool hasGroundAhead = Physics2D.Raycast(originEdge, Vector2.down, edgeCheckDistance, groundMask);

        // 벽 체크: 정면으로 Ray
        Vector2 originFront = (Vector2)transform.position + new Vector2(0.3f * dir, 0f);
        bool hasWall = Physics2D.Raycast(originFront, new Vector2(dir, 0f), wallCheckDistance, groundMask);

        if (!hasGroundAhead || hasWall)
        {
            Flip();
            dir = facingRight ? 1f : -1f;
        }

        rb.velocity = new Vector2(patrolSpeed * dir, rb.velocity.y);
        FaceByVelocity();
    }

    // --- 추격 ---
    private void ChaseMove()
    {
        // 플레이어 쪽을 바라보기
        if (player.position.x > transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (facingRight ? 1f : -1f);
        transform.localScale = s;

        // 절벽 앞에서는 멈춰서 떨어지지 않기
        float dir = facingRight ? 1f : -1f;
        Vector2 originEdge = (Vector2)transform.position + new Vector2(0.3f * dir, -0.15f);
        bool hasGroundAhead = Physics2D.Raycast(originEdge, Vector2.down, edgeCheckDistance, groundMask);
        
        if (hasGroundAhead)
        {
            rb.velocity = new Vector2(chaseSpeed * dir, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        FaceByVelocity();
    }

    // --- 공격 ---
    private void DoAttack()
    {
        // 공격 방향만 플레이어 쪽으로 정렬
        if (player.position.x > transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
        FaceByVelocity();

        // 공격 창 열기
        attackActiveUntil = Time.time + attackActiveTime;
        nextAttackAt = Time.time + attackCooldown;

        // 상태 종료: 바로 Chase/Patrol로 돌아가게 두고, 트리거는 타이머로 꺼짐
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer <= visionRange)
        {
            state = State.Chase;
        }
        else
        {
            state = State.Patrol;
        }
    }

    // --- 도우미: 좌우 뒤집기 ---
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (facingRight ? 1f : -1f);
        transform.localScale = s;

        // AttackZone이 있다면 좌우 위치도 같이 뒤집기(오른쪽/왼쪽)
        //if (attackZone != null)
        //{
        //    Vector3 p = attackZone.localPosition;
        //    p.x = Mathf.Abs(p.x) * (facingRight ? 1f : -1f);
        //    attackZone.localPosition = p;
        //}
    }

    private void FaceByVelocity()
    {
        if (rb.velocity.x > 0.05f)
        {
            if (!facingRight) Flip();
        }
        else if (rb.velocity.x < -0.05f)
        {
            if (facingRight) Flip();
        }
    }

    private void DebugDrawRanges()
    {
        // 감지/공격 원
        Debug.DrawLine(transform.position, transform.position + Vector3.right * visionRange, Color.yellow * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * visionRange, Color.yellow * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * attackRange, Color.red * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * attackRange, Color.red * 0.7f);
    }
}
