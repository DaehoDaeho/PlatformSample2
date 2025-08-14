using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coin;

    public int potionCount; // ���� ���� ���� ����.
    public int potionHealAmount = 20;    // ���� 1�� ��� �� ȸ����.

    public event Action<int> OnPotionChanged;

    public void AddCoin(int amount)
    {
        if(amount <= 0)
        {
            return;
        }

        coin += amount;

        CoinUI coinUI = FindObjectOfType<CoinUI>();
        if(coinUI != null)
        {
            coinUI.UpdateText(coin);
        }
    }

    public void AddPotion(int amount)
    {
        potionCount += amount;
        if(OnPotionChanged != null)
        {
            OnPotionChanged.Invoke(potionCount);
        }
    }

    public bool UsePotion(PlayerHealth health)
    {
        if(potionCount <= 0)
        {
            return false;
        }

        bool healed = health.Heal(potionHealAmount);
        if(healed == false)
        {
            return false;
        }

        potionCount--;

        if (OnPotionChanged != null)
        {
            OnPotionChanged.Invoke(potionCount);
        }

        return true;
    }
}
