using UnityEngine;

/// <summary>
/// �÷��̾ ������ ���� +1 �ϰ�, HUD ���� �� �ڽ��� �ı�.
/// �� ������Ʈ�� Collider2D�� IsTrigger = true ����.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class CoinCollectible : MonoBehaviour
{
    public int value = 1;

    void Awake()
    {
        // ����: Ʈ���� ����
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
            GameData.coinCount += value;

            // HUD ����(���� ����)
            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetCoins(GameData.coinCount);
            }

            // ���� ȸ��
            Destroy(gameObject);
        }
    }
}
