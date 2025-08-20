using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적의 체력을 관리하고 실제 대미지/넉백/사망 처리까지 담당.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 3;
    public int hp;
    public float hitInvincibleTime = 0.10f;

    private bool invincible;
    private float invincibleUntil;
    private Rigidbody2D rb;

    public event Action<EnemyHealth> OnDied;

    private void Awake()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(DamageInfo info)
    {
        if(invincible == true && Time.time < invincibleUntil)
        {
            return;
        }

        hp = Mathf.Max(0, hp - info.damage);

        rb.AddForce(info.knockback, ForceMode2D.Impulse);

        invincible = true;
        invincibleUntil = Time.time + hitInvincibleTime;

        if(hp == 0)
        {
            OnDied?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(invincible == true && Time.time >= invincibleUntil)
        {
            invincible = false;
        }
    }
}
