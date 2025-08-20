using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackHitbox : MonoBehaviour
{
    public Team attackTeam = Team.Player;
    public int damage = 1;
    public float knockbackForce = 6.0f;
    public  Vector2 knockbackDir = new Vector2(1.0f, 0.4f);

    public float activeStart = 0.10f;
    public float activeEnd = 0.25f;

    private Collider2D col;
    private bool swinging;
    private float swingStartTime;
    private HashSet<Collider2D> hitSet = new();

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(swinging == false)
        {
            return;
        }

        float t = Time.time - swingStartTime;

        bool shouldEnable = (t >= activeStart && t <= activeEnd);
        if(col.enabled != shouldEnable)
        {
            col.enabled = shouldEnable;
        }

        if(t > activeEnd)
        {
            swinging = false;
            col.enabled = false;
            hitSet.Clear();
        }
    }

    public void BeginSwing(bool facingRight)
    {
        swinging = true;
        swingStartTime = Time.time;
        hitSet.Clear();

        knockbackDir.x = Mathf.Abs(knockbackDir.x) * (facingRight ? 1.0f : -1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(col.enabled == false)
        {
            return;
        }

        if(hitSet.Contains(collision) == true)
        {
            return;
        }

        var hurt = collision.GetComponent<Hurtbox>();
        if(hurt == null)
        {
            return;
        }

        hitSet.Add(collision);

        Vector2 hitPoint = collision.bounds.center;
        Vector2 kb = knockbackDir.normalized * knockbackForce;

        var info = new DamageInfo(damage, kb, attackTeam, hitPoint);

        hurt.ApplyDamage(info);
    }
}
