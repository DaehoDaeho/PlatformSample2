// Assets/Scripts/Player/PlayerHealthSimple.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealthSimple : MonoBehaviour
{
    [Header("ü��")]
    public int maxHp = 5;
    public int hp = 5;

    [Header("����(i-frames)")]
    public float invincibleTime = 0.25f;   // �°� ���� �� �ð� ���� ����
    public bool invincible = false;
    public float invincibleEndTime = 0f;

    [Header("�ǵ��")]
    public SpriteRenderer sprite;          // ������ ��
    public float blinkInterval = 0.08f;    // �����̴� ����(��)
    private float blinkTimer = 0f;
    private bool blinkVisible = true;

    [Header("����")]
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
        // ���� �ð� ����
        if (invincible)
        {
            if (Time.time >= invincibleEndTime)
            {
                invincible = false;
                SetSpriteVisible(true);
            }
            else
            {
                // ������
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

    // ������ ���� �� ȣ��(����/�� ���ݿ��� �� �޼��带 ���� �θ�)
    public void TakeDamage(int amount, Vector2 knockback)
    {
        if (amount <= 0) return;
        if (isDead) return;
        if (invincible) return; // �����̸� ����

        hp = Mathf.Max(0, hp - amount);

        // �˹�: ��¦ �з�����
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.AddForce(knockback, ForceMode2D.Impulse);

        // ���� ����
        invincible = true;
        invincibleEndTime = Time.time + invincibleTime;
        blinkTimer = 0f;
        blinkVisible = false; // �ٷ� ��¦ ������� ����
        SetSpriteVisible(false);

        // ��� ó��
        if (hp == 0)
        {
            isDead = true;
            // ������ ��ũ��Ʈ�� �ִٸ� PlayerRespawnController�� ���� �ſ���.

            if (hp <= 0)
            {
                // 1) ü�� ȸ��(�ʿ� ��)
                hp = maxHp;

                // 2) Respawn ȣ��
                PlayerRespawnController resp = GetComponent<PlayerRespawnController>();
                if (resp != null)
                {
                    resp.RespawnNow();
                }

                // 3) (����) ���� �ð�
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
            c.a = v ? 1f : 0.3f; // ��¦ ��ġ��
            sprite.color = c;
        }
    }
}
