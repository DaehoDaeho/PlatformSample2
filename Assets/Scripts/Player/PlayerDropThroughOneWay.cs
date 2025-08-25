// Assets/Scripts/Player/PlayerDropThroughOneWay.cs
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDropThroughOneWay : MonoBehaviour
{
    public KeyCode downKey = KeyCode.S;          // 또는 KeyCode.DownArrow
    public KeyCode jumpKey = KeyCode.LeftControl;
    public float ignoreTime = 0.25f;             // 이 시간 동안 충돌 무시
    public LayerMask oneWayMask;                 // OneWayPlatform 레이어
    public GameObject groundCheck;

    private Collider2D playerCol;

    // (플랫폼 콜라이더, 복원 시각) 목록
    private List<Collider2D> ignoredCols = new List<Collider2D>();
    private List<float> ignoredUntil = new List<float>();

    void Awake()
    {
        playerCol = GetComponent<Collider2D>();
        if (oneWayMask.value == 0)
        {
            oneWayMask = LayerMask.GetMask("OneWayPlatform");
        }
    }

    void Update()
    {
        // 1) 복원 타이머 처리
        for (int i = ignoredCols.Count - 1; i >= 0; i--)
        {
            if (Time.time >= ignoredUntil[i])
            {
                var col = ignoredCols[i];
                if (col != null)
                {
                    Physics2D.IgnoreCollision(playerCol, col, false);
                }

                ignoredCols.RemoveAt(i);
                ignoredUntil.RemoveAt(i);
            }
        }

        // 2) 입력 체크: 아래+점프
        //if (Input.GetKey(downKey) == true && Input.GetKeyDown(jumpKey) == true)
        if (Input.GetKeyDown(downKey) == true)
        {
            TryDrop();
        }
    }

    private void TryDrop()
    {
        // 발밑으로 아주 짧게 Raycast 해서 "현재 서있는" OneWayPlatform 찾기
        //Vector2 origin = (Vector2)transform.position + Vector2.down * 0.1f;
        Vector2 origin = (Vector2)groundCheck.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.2f, oneWayMask);

        if (hit.collider == null)
        {
            return; // 발밑에 일방통행 없으면 종료
        }

        // 충돌 잠깐 무시
        Physics2D.IgnoreCollision(playerCol, hit.collider, true);
        ignoredCols.Add(hit.collider);
        ignoredUntil.Add(Time.time + ignoreTime);

        // 점프키와 함께 썼으니, 점프는 기존 점프 스크립트가 처리
        // (만약 점프를 강제로 낮추고 싶다면 여기서 플레이어 속도 y를 살짝 -로 초기화할 수도 있음)
    }
}
