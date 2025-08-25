// Assets/Scripts/Camera/CameraFollowDeadZone.cs
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowDeadZone : MonoBehaviour
{
    [Header("Ÿ��")]
    public Transform target;                 // ���� Player

    [Header("������")]
    public Vector2 deadZoneSize = new Vector2(4f, 3f);   // ����x����(���� ����)
    public Vector2 deadZoneOffset = Vector2.zero;        // ī�޶� �߽ɿ����� ������

    [Header("���� �ӵ�")]
    public float followSpeed = 6f;           // Ŭ���� �� ���� ����

    [Header("���(����)")]
    public BoxCollider2D levelBounds;        // �������� ��ü�� ���δ� �ڽ�

    // ����
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();

        // target�� ��������� Tag=Player�� �ڵ� Ž��
        if (target == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                target = p.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // 1) ī�޶� �ݰ���/�ݼ��� ���
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        // 2) ���� ī�޶� ��ġ�� ������ �߽�
        Vector3 camPos = transform.position;
        Vector2 deadCenter = new Vector2(camPos.x + deadZoneOffset.x, camPos.y + deadZoneOffset.y);

        // 3) ������ ���� ũ��
        float dzHalfW = deadZoneSize.x * 0.5f;
        float dzHalfH = deadZoneSize.y * 0.5f;

        // 4) ��ǥ ��ġ ���(�÷��̾ �������� ��� �ุŭ)
        float desiredX = camPos.x;
        float desiredY = camPos.y;

        if (target.position.x < deadCenter.x - dzHalfW)
        {
            desiredX = target.position.x + dzHalfW - deadZoneOffset.x;
        }
        else
        {
            if (target.position.x > deadCenter.x + dzHalfW)
            {
                desiredX = target.position.x - dzHalfW - deadZoneOffset.x;
            }
        }

        if (target.position.y < deadCenter.y - dzHalfH)
        {
            desiredY = target.position.y + dzHalfH - deadZoneOffset.y;
        }
        else
        {
            if (target.position.y > deadCenter.y + dzHalfH)
            {
                desiredY = target.position.y - dzHalfH - deadZoneOffset.y;
            }
        }

        Vector3 desiredPos = new Vector3(desiredX, desiredY, camPos.z);

        // 5) �ε巯�� �̵� (MoveTowards: ������ ����)
        float maxStep = followSpeed * Time.deltaTime;
        Vector3 nextPos = Vector3.MoveTowards(camPos, desiredPos, maxStep);

        // 6) ��� Ŭ����(����)
        if (levelBounds != null)
        {
            Bounds b = levelBounds.bounds;

            float minX = b.min.x + halfW;
            float maxX = b.max.x - halfW;
            float minY = b.min.y + halfH;
            float maxY = b.max.y - halfH;

            // ������ ī�޶󺸴� ���� ���� ������, ���� ó��
            if (minX > maxX)
            {
                float midX = (b.min.x + b.max.x) * 0.5f;
                minX = midX;
                maxX = midX;
            }
            if (minY > maxY)
            {
                float midY = (b.min.y + b.max.y) * 0.5f;
                minY = midY;
                maxY = midY;
            }

            nextPos.x = Mathf.Clamp(nextPos.x, minX, maxX);
            nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);
        }

        // 7) ���� ����
        transform.position = nextPos;
    }

    // �����Ϳ��� ������ �ð�ȭ
    void OnDrawGizmosSelected()
    {
        Camera c = GetComponent<Camera>();
        if (c == null)
        {
            return;
        }

        Vector3 camPos = transform.position;
        Vector3 center = new Vector3(camPos.x + deadZoneOffset.x, camPos.y + deadZoneOffset.y, 0f);

        Gizmos.color = new Color(1f, 0.8f, 0f, 0.4f);
        Gizmos.DrawCube(center, new Vector3(deadZoneSize.x, deadZoneSize.y, 0.1f));
        Gizmos.color = new Color(1f, 0.6f, 0f, 1f);
        Gizmos.DrawWireCube(center, new Vector3(deadZoneSize.x, deadZoneSize.y, 0.1f));
    }
}
