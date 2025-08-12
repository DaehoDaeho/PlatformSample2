using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int coin;

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
}
