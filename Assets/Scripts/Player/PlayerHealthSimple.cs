// Assets/Scripts/Player/PlayerHealthSimple.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealthSimple : MonoBehaviour
{
    [Header("체력")]
    public int maxHp = 5;
    public int hp = 5;

    [Header("무적(i-frames)")]
    public float invincibleTime = 0.25f;   // 맞고 나서 이 시간 동안 무적
    public bool invincible = false;
    public float invincibleEndTime = 0f;

    [Header("피드백")]
    public SpriteRenderer sprite;          // 깜빡임 용
    public float blinkInterval = 0.08f;    // 깜빡이는 간격(초)
    private float blinkTimer = 0f;
    private bool blinkVisible = true;

    [Header("상태")]
    public bool isDead = false;

    private Rigidbody2D rb;

    public float hitStunTime = 0.2f;
    public float hitStunEndTime = 0.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
        hp = Mathf.Clamp(hp, 0, maxHp);
    }

    void Update()
    {
        // 무적 시간 관리
        if (invincible)
        {
            if (Time.time >= invincibleEndTime)
            {
                invincible = false;
                SetSpriteVisible(true);
            }
            else
            {
                // 깜빡임
                blinkTimer += Time.deltaTime;
                if (blinkTimer >= blinkInterval)
                {
                    blinkTimer = 0f;
                    blinkVisible = !blinkVisible;
                    SetSpriteVisible(blinkVisible);
                }
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || isDead) return;
        hp = Mathf.Min(maxHp, hp + amount);
    }

    // 실제로 맞을 때 호출(함정/적 공격에서 이 메서드를 직접 부름)
    public void TakeDamage(int amount, Vector2 knockback)
    {
        if (amount <= 0) return;
        if (isDead) return;
        if (invincible) return; // 무적이면 무시

        hp = Mathf.Max(0, hp - amount);

        // 넉백: 살짝 밀려나게
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.AddForce(knockback, ForceMode2D.Impulse);

        // 무적 시작
        invincible = true;
        invincibleEndTime = Time.time + invincibleTime;
        blinkTimer = 0f;
        blinkVisible = false; // 바로 살짝 사라지게 시작
        SetSpriteVisible(false);

        // 사망 처리
        if (hp == 0)
        {
            isDead = true;
            // 움직임 스크립트가 있다면 PlayerRespawnController가 꺼줄 거예요.

            if (hp <= 0)
            {
                // 1) 체력 회복(필요 시)
                hp = maxHp;

                // 2) Respawn 호출
                PlayerRespawnController resp = GetComponent<PlayerRespawnController>();
                if (resp != null)
                {
                    resp.RespawnNow();
                }

                // 3) (선택) 무적 시간
                invincible = true;
                invincibleEndTime = Time.time + 0.8f;
            }
        }

        hitStunEndTime = Time.time + hitStunTime;
    }

    public bool IsInHitStun()
    {
        return Time.time < hitStunEndTime;
    }

    public void FullRestore()
    {
        hp = maxHp;
        isDead = false;
        invincible = false;
        SetSpriteVisible(true);
    }

    public void SetSpriteVisible(bool v)
    {
        if (sprite != null)
        {
            Color c = sprite.color;
            c.a = v ? 1f : 0.3f; // 살짝 비치게
            sprite.color = c;
        }
    }
}
