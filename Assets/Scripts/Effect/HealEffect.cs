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

        // 실제로 체력이 올랐을 때만 true(소모)
        bool healed = health.Heal(healAmount);
        return healed;
    }
}
