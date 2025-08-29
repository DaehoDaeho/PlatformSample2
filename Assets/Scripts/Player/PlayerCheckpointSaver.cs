using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �÷��̾ üũ����Ʈ�� ��Ҵٰ� �޽����� ������,
/// - PlayerRespawnController�� ����(������ ��ġ ����)
/// - SaveSystem.SaveCurrent(...) ȣ���ؼ� ��� ����
/// </summary>
[RequireComponent(typeof(PlayerRespawnController))]
public class PlayerCheckpointSaver : MonoBehaviour
{
    private PlayerRespawnController respawn;

    void Awake()
    {
        respawn = GetComponent<PlayerRespawnController>();
    }

    // Checkpoint.cs �� SendMessage("SetCheckpoint", pos) �� ȣ��
    public void SetCheckpoint(Vector2 pos)
    {
        // 1) ������ ��Ʈ�ѷ��� ���
        if (respawn != null)
        {
            respawn.SetCheckpoint(pos);
        }

        // 2) ���� ���� ����(��/����/üũ����Ʈ)
        string sceneName = SceneManager.GetActiveScene().name;
        int coins = GameData.coinCount;
        bool hasCP = true;

        SaveSystem.SaveCurrent(sceneName, coins, hasCP, pos);
    }
}
