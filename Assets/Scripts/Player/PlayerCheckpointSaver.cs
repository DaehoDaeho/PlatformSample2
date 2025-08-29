using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 플레이어가 체크포인트를 밟았다고 메시지를 받으면,
/// - PlayerRespawnController에 전달(리스폰 위치 갱신)
/// - SaveSystem.SaveCurrent(...) 호출해서 즉시 저장
/// </summary>
[RequireComponent(typeof(PlayerRespawnController))]
public class PlayerCheckpointSaver : MonoBehaviour
{
    private PlayerRespawnController respawn;

    void Awake()
    {
        respawn = GetComponent<PlayerRespawnController>();
    }

    // Checkpoint.cs 가 SendMessage("SetCheckpoint", pos) 로 호출
    public void SetCheckpoint(Vector2 pos)
    {
        // 1) 리스폰 컨트롤러에 기록
        if (respawn != null)
        {
            respawn.SetCheckpoint(pos);
        }

        // 2) 현재 상태 저장(씬/코인/체크포인트)
        string sceneName = SceneManager.GetActiveScene().name;
        int coins = GameData.coinCount;
        bool hasCP = true;

        SaveSystem.SaveCurrent(sceneName, coins, hasCP, pos);
    }
}
