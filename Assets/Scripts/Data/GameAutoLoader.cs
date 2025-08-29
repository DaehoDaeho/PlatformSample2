// Assets/Scripts/Systems/GameAutoLoader.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAutoLoader : MonoBehaviour
{
    [Tooltip("����� ���� ���� ���� ���� ��, �������ڸ��� üũ����Ʈ ��ġ�� �̵����� ����")]
    public bool warpToCheckpointOnSameScene = true;

    void Start()
    {
        SaveData data = SaveSystem.Load();
        if (data == null)
        {
            // ������ ������ ���� 0���� �ʱ�ȭ�ϰ� ����
            GameData.coinCount = 0;
            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetCoins(GameData.coinCount);
            }
            return;
        }

        // 1) ���� ���� + HUD ����
        GameData.coinCount = data.coinCount;
        if (HUDController.Instance != null)
        {
            HUDController.Instance.SetCoins(GameData.coinCount);
        }

        // 2) üũ����Ʈ ����(���� ���� ���� ��ġ �̵� ����)
        string current = SceneManager.GetActiveScene().name;
        if (current == data.stageName)
        {
            if (data.hasCheckpoint == true)
            {
                // �÷��̾� ã��
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
                            // ��� �� ��ġ�� �̵�
                            player.transform.position = cp;

                            // ���� �ӵ� �ʱ�ȭ
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
            // �ٸ� ���̸� ��ġ�� �ٲ��� �ʰ� ����/���¸� ����
            Debug.Log("����� ���� ���� ���� �ٸ��ϴ�. ���θ� �����մϴ�.");
        }
    }
}
