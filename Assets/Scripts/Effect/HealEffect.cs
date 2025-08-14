// Scripts/Effect/HealEffect.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item Effects/Heal Effect")]
public class HealEffect : ItemUseEffect
{
    [SerializeField] private int healAmount = 20;

    public override bool Apply(GameObject user)
    {
        var health = user.GetComponent<PlayerHealth>();
        if (health == null) return false;

        // ������ ü���� �ö��� ���� true(�Ҹ�)
        bool healed = health.Heal(healAmount);
        return healed;
    }
}
