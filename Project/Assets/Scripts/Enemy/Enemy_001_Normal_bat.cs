using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_001_Normal_bat : Enemy
{
    public override void InitEnemyStatusCustom()
    {
        hpFull =10;
        hp = 10;
        damage = 3;
        speed = 2;
        attackSpeed = 0.1f; // *****************************************************

        itemProb = 1;
        manaValue = 3;
    }

    protected override void AttackCustom()
    {
        
    }
    public override void MoveCustom()
    {
        //Vector3 dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        //Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        //rb.MovePosition(transform.position + nextVec);
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
        
        rb.velocity = dirVec.normalized * speed;
    }

    public override void DieCustom()  // *****************************************************
    {
        // obj.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // base.hp = 10;                // *****************************************************
        // base.DropItem();
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "001";
    }
}
