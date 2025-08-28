using UnityEngine;

/// <summary>
/// 플레이어가 닿으면 코인 +1 하고, HUD 갱신 후 자신을 파괴.
/// 이 오브젝트의 Collider2D는 IsTrigger = true 권장.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class CoinCollectible : MonoBehaviour
{
    public int value = 1;

    void Awake()
    {
        // 안전: 트리거 권장
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

            // HUD 갱신(있을 때만)
            if (HUDController.Instance != null)
            {
                HUDController.Instance.SetCoins(GameData.coinCount);
            }

            // 코인 회수
            Destroy(gameObject);
        }
    }
}
