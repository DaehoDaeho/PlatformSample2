// Scripts/Effect/BombEffect.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Effects/Bomb Effect")]
public class BombEffect : ItemUseEffect
{
    [SerializeField] private GameObject explosionPrefab; // 폭발 프리팹(파티클+사운드+DamageArea)
    [SerializeField] private Vector2 spawnOffset = new Vector2(0.5f, 0f);

    public override bool Apply(GameObject user)
    {
        if (explosionPrefab != null)
        {
            Vector3 pos = user.transform.position + (Vector3)spawnOffset;
            Object.Instantiate(explosionPrefab, pos, Quaternion.identity);
        }

        // 항상 true: 폭탄은 사용하면 반드시 소모
        return true;
    }
}
