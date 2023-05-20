using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_007_groupBat : Enemy
{
    public float distance;
    Vector3 dirVec;

    float dashPower;
    float dashTime;
    float coolTime;

    float lifetime;

    bool canDash;
    bool isDash;
    //bool canKnockBack;

    public override void DieCustom()
    {
        
    }

    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;
        damage = 1;
        attackSpeed = 3f;
        speed = 3f;
        dashPower = 15f;
        dashTime = 5f;
        coolTime = 10f;
        canDash = true;
        isDash = false;
        canKnockBack = true;
        strongAttack = true;
        oneShot = true;
        shot = false;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "007";
    }

    public override void MoveCustom()
    {
        // 플레이어 위치
        //distance = Vector3.Distance(transform.position, base.target.transform.position);
        //dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        //StartCoroutine(Dash());
    }

    protected override void AttackCustom()
    {
        shot = true;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        canKnockBack = false;
        isDash = true;
        canDash = false;
        canMove = false;
        rb.velocity = Vector3.zero;
        //Debug.Log(" 대시 충전");
        
        yield return new WaitForSeconds(0.1f);


        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        //Vector3 dirVec = base.target.transform.position - transform.position;
        rb.velocity = dirVec.normalized * dashPower;
        //Debug.Log("대시!");

        yield return new WaitForSeconds(dashTime);

        Debug.Log("대시끝");
        isDash = false;
        canKnockBack = true;
        canMove = true;

        CleanDeath();
        //yield return new WaitForSeconds(coolTime);
    }

}
