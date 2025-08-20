using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public int damage;
    public Vector2 knockback;
    public Team attackTeam;
    public Vector2 hitPoint;

    public DamageInfo(int dmg, Vector2 kb, Team team, Vector2 point)
    {
        damage = dmg;
        knockback = kb;
        attackTeam = team;
        hitPoint = point;
    }
}
