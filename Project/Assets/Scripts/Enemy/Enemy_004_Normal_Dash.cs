using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_004_Normal_Dash : Enemy
{
    public float dashPower = 10f; // ���� ��
    float dashTime = 1f; // ���� ���ӽð�
    float cooltime = 10f; // ���� ��Ÿ��
    bool isDash = false; // �������ΰ�
    bool canDash = true; // ���� ���ɿ���


    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;

       damage = 5;


        speed = 3f;
        attackSpeed = 0.2f;
        cooltime = 10f;
        dashPower = 10f;
        isDash = false;
        canDash = true;
    }

    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + nextVec);
    }
    protected override void AttackCustom()
    {
        StartCoroutine(Dash());
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    IEnumerator Dash()
    {

        canKnockBack = false;
        isDash = true;
        canDash = false;

        canMove = false;
        rb.velocity = Vector3.zero;
        //Debug.Log(" 대시 충전");
        yield return new WaitForSeconds(0.5f);

        strongAttack = true;
        damage = 10;

        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = dirVec.normalized * dashPower;
        //Debug.Log("대시!");
        
        yield return new WaitForSeconds(dashTime);
        strongAttack = false;
        damage = 5;
        //Debug.Log("대시끝");
        isDash = false;
        canKnockBack = true;
        canMove = true;
        yield return new WaitForSeconds(cooltime);
        canDash = true;
        

    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "004";
    }
}
