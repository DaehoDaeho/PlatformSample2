// Assets/Scripts/Camera/CameraFollowDeadZone.cs
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowDeadZone : MonoBehaviour
{
    [Header("타겟")]
    public Transform target;                 // 보통 Player

    [Header("데드존")]
    public Vector2 deadZoneSize = new Vector2(4f, 3f);   // 가로x세로(월드 단위)
    public Vector2 deadZoneOffset = Vector2.zero;        // 카메라 중심에서의 오프셋

    [Header("추적 속도")]
    public float followSpeed = 6f;           // 클수록 더 빨리 붙음

    [Header("경계(선택)")]
    public BoxCollider2D levelBounds;        // 스테이지 전체를 감싸는 박스

    // 내부
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();

        // target이 비어있으면 Tag=Player를 자동 탐색
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

        // 1) 카메라 반가로/반세로 계산
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;

        // 2) 현재 카메라 위치와 데드존 중심
        Vector3 camPos = transform.position;
        Vector2 deadCenter = new Vector2(camPos.x + deadZoneOffset.x, camPos.y + deadZoneOffset.y);

        // 3) 데드존 절반 크기
        float dzHalfW = deadZoneSize.x * 0.5f;
        float dzHalfH = deadZoneSize.y * 0.5f;

        // 4) 목표 위치 계산(플레이어가 데드존을 벗어난 축만큼)
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

        // 5) 부드러운 이동 (MoveTowards: 프레임 독립)
        float maxStep = followSpeed * Time.deltaTime;
        Vector3 nextPos = Vector3.MoveTowards(camPos, desiredPos, maxStep);

        // 6) 경계 클램프(선택)
        if (levelBounds != null)
        {
            Bounds b = levelBounds.bounds;

            float minX = b.min.x + halfW;
            float maxX = b.max.x - halfW;
            float minY = b.min.y + halfH;
            float maxY = b.max.y - halfH;

            // 레벨이 카메라보다 작을 수도 있으니, 안전 처리
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

        // 7) 최종 적용
        transform.position = nextPos;
    }

    // 에디터에서 데드존 시각화
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
