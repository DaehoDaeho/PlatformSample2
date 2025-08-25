// Assets/Scripts/World/MovingPlatformSimple.cs
using UnityEngine;

/// <summary>
/// 두 위치 사이를 왕복 + 도착 시 잠깐 대기.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatformSimple : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 0.5f;      // 끝점에서 잠깐 쉬는 시간

    private Rigidbody2D rb;
    private Transform target;          // 현재 향하는 목적지
    private float waitUntil = 0f;

    void Awake()
    {
        pointA.SetParent(null);
        pointB.SetParent(null);

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // 초기 목적지: 더 가까운 쪽
        if (pointA == null || pointB == null)
        {
            Debug.LogError("MovingPlatformSimple: pointA/pointB를 지정하세요.");
            enabled = false; return;
        }
        target = (Vector2.Distance(transform.position, pointA.position) <
                  Vector2.Distance(transform.position, pointB.position))
                 ? pointB : pointA;
    }

    void FixedUpdate()
    {
        if (Time.time < waitUntil)
        {
            return;
        }

        Vector2 pos = rb.position;
        Vector2 t = target.position;
        Vector2 dir = (t - pos);

        float dist = dir.magnitude;
        if (dist < 0.01f)
        {
            // 도착: 대기 후 목적지 전환
            waitUntil = Time.time + waitTime;
            target = (target == pointA) ? pointB : pointA;
            return;
        }

        dir = dir.normalized;
        Vector2 next = pos + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(next);
    }
}
