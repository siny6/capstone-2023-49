using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dummy : Enemy
{
    public override void DieCustom(GameObject obj)
    {
        DropItem();
        Destroy(gameObject);
    }

    public override void InitEnemyStatus()
    {
        base.hp = 1000;            
        base.speed = 0;
        base.attackSpeed = 0;
    }

    public override void MoveCustom()
    {
        
    }

    protected override void AttackCustom()
    {
        
    }
}
