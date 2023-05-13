using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_007_groupBat : Enemy
{
    // 목표
    // 1. 맵 바깥에서 생성되어 플레이어 방향으로 돌진
    // 움직임은 같이 하되 피해 입는 것은 각각
    public float distance;
    Vector3 dirVec;

    float dashPower;
    //float dashTime;
    float coolTime;

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
        attackSpeed = 0.2f;
        speed = 3f;
        dashPower = 10f;
        //dashTime = 1f;
        coolTime = 15f;
        canDash = true;
        isDash = false;
        canKnockBack = true;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "007";
    }

    public override void MoveCustom()
    {
        // 플레이어 위치
        distance = Vector3.Distance(transform.position, base.target.transform.position);
        dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        //base.rb.velocity = base.target.transform.position * dashPower;
        //base.rb.velocity = dirVec * dashPower;
    }

    protected override void AttackCustom()
    {
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
        //yield return new WaitForSeconds(0.5f);


        Vector3 dirVec = base.target.transform.position - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = dirVec.normalized * dashPower;
        //Debug.Log("대시!");

        //yield return new WaitForSeconds(dashTime);

        //Debug.Log("대시끝");
        //isDash = false;
        //canKnockBack = true;
        //canMove = true;
        yield return new WaitForSeconds(coolTime);
        //canDash = true;
        // 체력이 0이 되면 풀에 반납 // 맵 밖으로 나가면 체력 
        // 맵 크기를 벗어나면 풀에 반납
        // 풀에서 가져올 때 spawnPos 새롭게
    }
}
