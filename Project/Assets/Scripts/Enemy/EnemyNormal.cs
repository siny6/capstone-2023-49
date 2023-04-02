using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Enemy
{
    public override void InitEnemyStatus()
    {
        hp = 10;        // *****************************************************

        damage = 3;
        speed = 2;
        attackSpeed = 0; // *****************************************************
    }

    protected override void AttackCustom()
    {
        
    }
    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rb.MovePosition(transform.position + nextVec);
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
    }

    public override void DieCustom(GameObject obj)  // *****************************************************
    {
        obj.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        base.hp = 10;                // *****************************************************
        base.DropItem();
    }

}
