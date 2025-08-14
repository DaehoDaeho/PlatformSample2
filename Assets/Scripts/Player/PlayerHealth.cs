// Scripts/Player/PlayerHealth.cs
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int hp;
    public int Hp => hp;
    public int MaxHp => maxHp;

    public event Action<int, int> OnHpChanged; // (old,new)

    void Awake() => hp = maxHp;

    public bool Heal(int amount)
    {
        if (amount <= 0) return false;
        int old = hp;
        hp = Mathf.Min(maxHp, hp + amount);
        if (hp != old) { OnHpChanged?.Invoke(old, hp); return true; }
        return false;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        int old = hp;
        hp = Mathf.Max(0, hp - amount);
        if (hp != old) OnHpChanged?.Invoke(old, hp);
    }
}
