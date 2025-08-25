// Assets/Scripts/World/MovingPlatformSimple.cs
using UnityEngine;

/// <summary>
/// �� ��ġ ���̸� �պ� + ���� �� ��� ���.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatformSimple : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 0.5f;      // �������� ��� ���� �ð�

    private Rigidbody2D rb;
    private Transform target;          // ���� ���ϴ� ������
    private float waitUntil = 0f;

    void Awake()
    {
        pointA.SetParent(null);
        pointB.SetParent(null);

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // �ʱ� ������: �� ����� ��
        if (pointA == null || pointB == null)
        {
            Debug.LogError("MovingPlatformSimple: pointA/pointB�� �����ϼ���.");
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
            // ����: ��� �� ������ ��ȯ
            waitUntil = Time.time + waitTime;
            target = (target == pointA) ? pointB : pointA;
            return;
        }

        dir = dir.normalized;
        Vector2 next = pos + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(next);
    }
}
