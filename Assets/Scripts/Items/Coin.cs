using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int amount = 1;
    public AudioClip pickupSfx;
    public GameObject pickupVfxPrefab;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == false)
        {
            return;
        }

        PlayerInventory player = collision.GetComponent<PlayerInventory>();
        if(player == null)
        {
            return;
        }

        player.AddCoin(amount);

        Destroy(gameObject);
    }
}
