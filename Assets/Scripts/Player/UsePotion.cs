using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePotion : MonoBehaviour
{
    public PlayerInventory inv;
    public PlayerHealth health;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool used = inv.UsePotion(health);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(health != null)
            {
                health.TakeDamage(20);
            }
        }
    }
}
