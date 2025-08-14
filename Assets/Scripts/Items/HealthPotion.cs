using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class HealthPotion : MonoBehaviour
{
    public bool useOnPickup = false;    // �ٷ� ��� �Ǵ� ���� ���θ� �����ϴ� ����.
    public int amount = 1;
    public int instantHeal = 20;    // ��� ��� �� ȸ����.

    private bool consumed;  // �ߺ� Ʈ���� ����.

    private void Reset()
    {
        var col = GetComponent<CapsuleCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player" || consumed == true)
        {
            return;
        }

        consumed = true;
        var col = GetComponent<CapsuleCollider2D>();
        if(col != null)
        {
            col.enabled = false;
        }

        var inv = collision.GetComponent<PlayerInventory>();
        var health = collision.GetComponent<PlayerHealth>();

        if (useOnPickup == true)
        {
            if(health != null)
            {
                health.Heal(instantHeal);
            }
        }
        else
        {
            if(inv != null)
            {
                inv.AddPotion(amount);
            }
        }

        Destroy(gameObject);
    }
}
