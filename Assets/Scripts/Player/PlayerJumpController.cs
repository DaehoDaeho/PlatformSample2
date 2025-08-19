// Assets/Scripts/Player/PlayerJumpController.cs
using UnityEngine;

/// <summary>
/// 코요테 타임 + 점프 버퍼 + Better Jump(추가 중력) + 최대 낙하속도.
/// 입력은 Update에서 "시각"으로 기록, 실제 물리 변경은 FixedUpdate에서 처리.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck2D))]
public class PlayerJumpController : MonoBehaviour
{
    [Header("입력")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("점프 파라미터")]
    [SerializeField, Tooltip("점프 시작 시 수직속도(velocity.y)")]
    private float jumpVelocity = 12f;

    [SerializeField, Tooltip("지면 이탈 후 이 시간 동안은 점프 허용")]
    private float coyoteTime = 0.10f;

    [SerializeField, Tooltip("너무 일찍 눌러도 이 시간 동안은 입력 기억→착지 즉시 점프")]
    private float jumpBufferTime = 0.10f;

    [Header("낙하/상승 튜닝")]
    [SerializeField, Tooltip("하강 시 추가 중력 배수(>1 권장)")]
    private float fallGravityMultiplier = 1.8f;

    [SerializeField, Tooltip("상승 중 점프키를 떼면 더 강한 중력을 적용(가변 점프)")]
    private float lowJumpGravityMultiplier = 2.0f;

    [SerializeField, Tooltip("최대 낙하 속도(절댓값)")]
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
        // 1) 지면 시각 갱신
        if (ground.IsGrounded)
            lastGroundedTime = Time.time;

        // 2) 점프 입력 시각 기록
        if (Input.GetKeyDown(jumpKey))
            lastJumpPressedTime = Time.time;

        // 3) 코요테/버퍼 조건 확인 → 점프 예약
        bool canCoyote = Time.time - lastGroundedTime <= coyoteTime;
        bool inBuffer = Time.time - lastJumpPressedTime <= jumpBufferTime;

        if (inBuffer && canCoyote)
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        // 예약된 점프 실행
        if (jumpRequested)
        {
            jumpRequested = false;
            ExecuteJump();
        }

        // 추가 중력/가변 점프
        ApplyBetterJumpGravity();

        // 최대 낙하속도 제한
        LimitFallSpeed();
    }

    private void ExecuteJump()
    {
        // 기존 수직속도를 0으로 리셋하면 반응성이 또렷해짐
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.velocity += Vector2.up * jumpVelocity;

        // 버퍼/코요테 소모
        lastJumpPressedTime = -999f;
        lastGroundedTime = -999f;
    }

    private void ApplyBetterJumpGravity()
    {
        // Physics2D.gravity.y는 음수. rb.gravityScale을 곱해 기본 중력 가속을 얻는다.
        float baseG = Physics2D.gravity.y * rb.gravityScale;

        // 상승 중
        if (rb.velocity.y > 0f)
        {
            bool holding = Input.GetKey(jumpKey); // 키를 누르고 있으면 더 높이
            float mult = holding ? 1f : lowJumpGravityMultiplier;
            // 추가 가속을 velocity에 직접 누적
            rb.velocity += Vector2.up * (baseG * (mult - 1f)) * Time.fixedDeltaTime;
        }
        // 하강 중
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
