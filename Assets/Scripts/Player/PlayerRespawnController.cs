// Assets/Scripts/Player/PlayerRespawnController.cs
using UnityEngine;

/// <summary>
/// 플레이어 리스폰 담당 최소 구현.
/// - Checkpoint가 SetCheckpoint(Vector2)를 호출
/// - 사망 시 RespawnNow()를 호출
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRespawnController : MonoBehaviour
{
    public Vector2 lastCheckpointPos; // 저장된 위치
    public bool hasCheckpoint = false;

    public void SetCheckpoint(Vector2 pos)
    {
        lastCheckpointPos = pos;
        hasCheckpoint = true;
    }

    public void RespawnNow()
    {
        // 체크포인트가 있어야 이동
        if (hasCheckpoint == true)
        {
            transform.position = lastCheckpointPos;
        }

        // 속도 초기화(튕김/굴림 방지)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
