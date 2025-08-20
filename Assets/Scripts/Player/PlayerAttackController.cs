using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.H;
    public float attackCooldown = 0.35f;

    public AttackHitbox hitBox;
    Rigidbody2D rb;

    private float lastAttackTime = -999.0f;
    private bool facingRight = true;

    private void Awake()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }
        
    // Update is called once per frame
    void Update()
    {
        if(rb != null)
        {
            if(rb.velocity.x < -0.05f)
            {
                facingRight = false;
            }
            else if(rb.velocity.x > 0.05f)
            {
                facingRight = true;
            }
        }
        else
        {
            if(transform.localScale.x > 0.0f)
            {
                facingRight = true;
            }
            else if(transform.localScale.x < 0.0f)
            {
                facingRight = false;
            }
        }

        if(Input.GetKeyDown(attackKey) == true)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if(Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        if(hitBox == null)
        {
            return;
        }

        lastAttackTime = Time.time;

        hitBox.BeginSwing(facingRight);
    }
}
