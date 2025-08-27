// Assets/Scripts/World/Checkpoint.cs
using UnityEngine;

/// <summary>
/// 플레이어가 트리거에 들어오면 그 위치를 체크포인트로 저장.
/// - 특정 클래스에 의존하지 않음(메시지 방식)
/// - 플레이어/자식 컴포넌트 중 "SetCheckpoint(Vector2)"를 가진 곳에 전달
/// - 한 번 밟으면 색상 변경(시각 표시)
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Checkpoint : MonoBehaviour
{
    public Color activatedColor = new Color(1f, 1f, 0.6f, 1f);
    private bool isActivated = false;

    void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col.isTrigger == false)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") == true)
        {
            // 컨트롤러 이름과 무관하게 좌표 전달(있으면 호출, 없으면 무시)
            other.SendMessage("SetCheckpoint", (Vector2)transform.position, SendMessageOptions.DontRequireReceiver);

            if (isActivated == false)
            {
                isActivated = true;

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = activatedColor;
                }
            }
        }
    }
}
