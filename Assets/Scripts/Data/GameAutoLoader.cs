// Assets/Scripts/Systems/GameAutoLoader.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAutoLoader : MonoBehaviour
{
    [Tooltip("저장된 씬과 현재 씬이 같을 때, 시작하자마자 체크포인트 위치로 이동할지 여부")]
    public bool warpToCheckpointOnSameScene = true;

    void Start()
    {
        SaveData data = SaveSystem.Load();
        if (data == null)
        {
            // 저장이 없으면 코인 0으로 초기화하고 종료
            GameData.coinCount = 0;
            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetCoins(GameData.coinCount);
            }
            return;
        }

        // 1) 코인 복원 + HUD 갱신
        GameData.coinCount = data.coinCount;
        if (HUDController.Instance != null)
        {
            HUDController.Instance.SetCoins(GameData.coinCount);
        }

        // 2) 체크포인트 복원(같은 씬일 때만 위치 이동 선택)
        string current = SceneManager.GetActiveScene().name;
        if (current == data.stageName)
        {
            if (data.hasCheckpoint == true)
            {
                // 플레이어 찾기
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    PlayerRespawnController resp = player.GetComponent<PlayerRespawnController>();
                    if (resp != null)
                    {
                        Vector2 cp = new Vector2(data.checkpointX, data.checkpointY);
                        resp.SetCheckpoint(cp);

                        if (warpToCheckpointOnSameScene == true)
                        {
                            // 즉시 그 위치로 이동
                            player.transform.position = cp;

                            // 물리 속도 초기화
                            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                            if (rb != null)
                            {
                                rb.velocity = Vector2.zero;
                                rb.angularVelocity = 0f;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // 다른 씬이면 위치는 바꾸지 않고 코인/상태만 복원
            Debug.Log("저장된 씬과 현재 씬이 다릅니다. 코인만 복원합니다.");
        }
    }
}
