using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    public EnemyHealth owner;

    private void Awake()
    {
        if(owner == null)
        {
            GetComponent<EnemyHealth>();
        }

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    public void ApplyDamage(DamageInfo info)
    {
        if(owner != null)
        {
            owner.TakeDamage(info);
        }
    }
}
