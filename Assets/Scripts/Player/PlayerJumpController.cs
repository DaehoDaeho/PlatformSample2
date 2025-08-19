// Assets/Scripts/Player/PlayerJumpController.cs
using UnityEngine;

/// <summary>
/// �ڿ��� Ÿ�� + ���� ���� + Better Jump(�߰� �߷�) + �ִ� ���ϼӵ�.
/// �Է��� Update���� "�ð�"���� ���, ���� ���� ������ FixedUpdate���� ó��.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck2D))]
public class PlayerJumpController : MonoBehaviour
{
    [Header("�Է�")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("���� �Ķ����")]
    [SerializeField, Tooltip("���� ���� �� �����ӵ�(velocity.y)")]
    private float jumpVelocity = 12f;

    [SerializeField, Tooltip("���� ��Ż �� �� �ð� ������ ���� ���")]
    private float coyoteTime = 0.10f;

    [SerializeField, Tooltip("�ʹ� ���� ������ �� �ð� ������ �Է� �������� ��� ����")]
    private float jumpBufferTime = 0.10f;

    [Header("����/��� Ʃ��")]
    [SerializeField, Tooltip("�ϰ� �� �߰� �߷� ���(>1 ����)")]
    private float fallGravityMultiplier = 1.8f;

    [SerializeField, Tooltip("��� �� ����Ű�� ���� �� ���� �߷��� ����(���� ����)")]
    private float lowJumpGravityMultiplier = 2.0f;

    [SerializeField, Tooltip("�ִ� ���� �ӵ�(����)")]
    private float maxFallSpeed = 20f;

    private Rigidbody2D rb;
    private GroundCheck2D ground;

    private float lastGroundedTime = -999f;
    private float lastJumpPressedTime = -999f;
    private bool jumpRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<GroundCheck2D>();
    }

    void Update()
    {
        // 1) ���� �ð� ����
        if (ground.IsGrounded)
            lastGroundedTime = Time.time;

        // 2) ���� �Է� �ð� ���
        if (Input.GetKeyDown(jumpKey))
            lastJumpPressedTime = Time.time;

        // 3) �ڿ���/���� ���� Ȯ�� �� ���� ����
        bool canCoyote = Time.time - lastGroundedTime <= coyoteTime;
        bool inBuffer = Time.time - lastJumpPressedTime <= jumpBufferTime;

        if (inBuffer && canCoyote)
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        // ����� ���� ����
        if (jumpRequested)
        {
            jumpRequested = false;
            ExecuteJump();
        }

        // �߰� �߷�/���� ����
        ApplyBetterJumpGravity();

        // �ִ� ���ϼӵ� ����
        LimitFallSpeed();
    }

    private void ExecuteJump()
    {
        // ���� �����ӵ��� 0���� �����ϸ� �������� �Ƿ�����
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += Vector2.up * jumpVelocity;

        // ����/�ڿ��� �Ҹ�
        lastJumpPressedTime = -999f;
        lastGroundedTime = -999f;
    }

    private void ApplyBetterJumpGravity()
    {
        // Physics2D.gravity.y�� ����. rb.gravityScale�� ���� �⺻ �߷� ������ ��´�.
        float baseG = Physics2D.gravity.y * rb.gravityScale;

        // ��� ��
        if (rb.velocity.y > 0f)
        {
            bool holding = Input.GetKey(jumpKey); // Ű�� ������ ������ �� ����
            float mult = holding ? 1f : lowJumpGravityMultiplier;
            // �߰� ������ velocity�� ���� ����
            rb.velocity += Vector2.up * (baseG * (mult - 1f)) * Time.fixedDeltaTime;
        }
        // �ϰ� ��
        else if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * (baseG * (fallGravityMultiplier - 1f)) * Time.fixedDeltaTime;
        }
    }

    private void LimitFallSpeed()
    {
        if (rb.velocity.y < -maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
    }
}
