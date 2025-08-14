// Scripts/Effect/BombEffect.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Effects/Bomb Effect")]
public class BombEffect : ItemUseEffect
{
    [SerializeField] private GameObject explosionPrefab; // ���� ������(��ƼŬ+����+DamageArea)
    [SerializeField] private Vector2 spawnOffset = new Vector2(0.5f, 0f);

    public override bool Apply(GameObject user)
    {
        if (explosionPrefab != null)
        {
            Vector3 pos = user.transform.position + (Vector3)spawnOffset;
            Object.Instantiate(explosionPrefab, pos, Quaternion.identity);
        }

        // �׻� true: ��ź�� ����ϸ� �ݵ�� �Ҹ�
        return true;
    }
}
