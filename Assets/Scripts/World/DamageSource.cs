// Assets/Scripts/World/DamageSource.cs
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageSource : MonoBehaviour
{
    public int damage = 1;

    // 넉백 세기(전체 스케일) — 8~12부터 튜닝 추천
    public float knockbackPower = 10f;

    // 가로/세로 비중 — 가로를 크게, 세로는 살짝
    public float horizontalWeight = 1.0f;
    public float verticalWeight = 0.25f;

    // 같은 함정에 연속으로 닿을 때 방향이 섞이는 것 방지
    public float localCooldown = 0.10f;
    private float canHitAt = 0f;

    private Collider2D myCol;

    void Awake()
    {
        myCol = GetComponent<Collider2D>();
    }

    void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < canHitAt) return;
        if (!other.CompareTag("Player")) return;

        var health = other.GetComponent<PlayerHealthSimple>();
        var rb = other.GetComponent<Rigidbody2D>();
        if (health == null || rb == null) return;

        // 1) other(플레이어)와 나(myCol) 사이의 거리/법선
        //    dist.normal = other → this(함정) 방향 벡터 (보통)
        //    함정에서 멀어지게 밀려면 -dist.normal 사용
        ColliderDistance2D dist = Physics2D.Distance(other, myCol);
        Vector2 away = -dist.normal;

        // 2) 가끔 수직만 나오는 경우(경사/위치 정렬) 가로 성분을 약간 보정
        if (Mathf.Abs(away.x) < 0.0001f)
        {
            float side = (other.bounds.center.x >= myCol.bounds.center.x) ? 1f : -1f;
            away.x = side * 0.5f;
        }

        // 3) 비중 적용(정규화 X: 비중을 그대로 살림)
        float x = away.x * horizontalWeight;
        float y = Mathf.Max(0f, away.y) * verticalWeight; // 아래로 강하게 미는 건 지양
        Vector2 dir = new Vector2(x, y);

        // 4) 파워 곱하기
        Vector2 knockback = dir * knockbackPower;

        // 5) 기존 달리기 X속도는 잠깐 0으로 (넉백 상쇄 방지)
        rb.velocity = new Vector2(0f, rb.velocity.y);

        // 6) 실제 데미지/넉백
        health.TakeDamage(damage, knockback);

        // 7) 짧은 쿨다운
        canHitAt = Time.time + localCooldown;

        // (디버그 확인용) 방향이 제대로인지 한눈에 보기
        Debug.DrawRay(other.bounds.center, knockback.normalized * 2f, Color.cyan, 0.5f);
    }
}
