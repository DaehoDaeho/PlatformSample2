// Assets/Scripts/Enemy/EnemyAIController.cs
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    // --- ���� ---
    private enum State { Patrol, Chase, Attack }
    private State state = State.Patrol;

    // --- �̵�/���� �Ķ���� ---
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 2.5f;
    public float visionRange = 6f;    // �� �Ÿ� ������ ���� �߰� ����
    public float attackRange = 0.8f;  // �� �Ÿ� ���̸� ���� �õ�

    // --- ���� Ÿ�̹� ---
    public float attackActiveTime = 0.15f; // ���� Ʈ���� �ѵδ� �ð�
    public float attackCooldown = 0.50f; // ���� ���ݱ��� ���
    private float attackActiveUntil = 0f;
    private float nextAttackAt = 0f;

    // --- ����(����/��) ---
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f; // �߹� ª��
    public float edgeCheckDistance = 0.3f; // ���� �߳� ª��(���� üũ)
    public float wallCheckDistance = 0.1f; // ���� ª��(�� üũ)

    // --- ���� ---
    public Transform attackZone;            // �ڽ�(��Ʈ�ڽ�) ��Ʈ
    private Collider2D attackTrigger;       // �ڽ��� Trigger �ݶ��̴�
    private Rigidbody2D rb;
    private Transform player;

    // --- ���� ���� ---
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

        // ���� �� ���� Ʈ���Ŵ� ����
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

        // 1) ���� ���� ����
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (state != State.Attack) // ���� �߿��� ���� �ٲ��� ����(ª�� �ð�)
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

        // 2) ���º� �ൿ
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

        // 3) ���� Ʈ���� ON/OFF ����
        if (attackTrigger != null)
        {
            if (Time.time <= attackActiveUntil)
            {
                attackTrigger.enabled = true;
                // ���� �߿��� ��¦ ���߰�(�Ǵ� ������) �ص� ����
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else
            {
                attackTrigger.enabled = false;
            }
        }

        // 4) ����� �ð�ȭ(���Ѵٸ�): ����/���� �ݰ�
        DebugDrawRanges();
    }

    // --- ���� ---
    private void PatrolMove()
    {
        float dir = facingRight ? 1f : -1f;

        // ���� üũ: ���� �߳� �Ʒ��� Ray
        Vector2 originEdge = (Vector2)transform.position + new Vector2(0.3f * dir, -0.15f);
        bool hasGroundAhead = Physics2D.Raycast(originEdge, Vector2.down, edgeCheckDistance, groundMask);

        // �� üũ: �������� Ray
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

    // --- �߰� ---
    private void ChaseMove()
    {
        // �÷��̾� ���� �ٶ󺸱�
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

        // ���� �տ����� ���缭 �������� �ʱ�
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

    // --- ���� ---
    private void DoAttack()
    {
        // ���� ���⸸ �÷��̾� ������ ����
        if (player.position.x > transform.position.x)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
        FaceByVelocity();

        // ���� â ����
        attackActiveUntil = Time.time + attackActiveTime;
        nextAttackAt = Time.time + attackCooldown;

        // ���� ����: �ٷ� Chase/Patrol�� ���ư��� �ΰ�, Ʈ���Ŵ� Ÿ�̸ӷ� ����
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

    // --- �����: �¿� ������ ---
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (facingRight ? 1f : -1f);
        transform.localScale = s;

        // AttackZone�� �ִٸ� �¿� ��ġ�� ���� ������(������/����)
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
        // ����/���� ��
        Debug.DrawLine(transform.position, transform.position + Vector3.right * visionRange, Color.yellow * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * visionRange, Color.yellow * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * attackRange, Color.red * 0.7f);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * attackRange, Color.red * 0.7f);
    }
}
