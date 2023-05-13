using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_002_Normal_Ranger : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;

    

    public override void InitEnemyStatusCustom()
    {
        hpFull = 15;
        damage = 3;
        hp = 15;
        speed = 2;
        attackSpeed = 1f;

        itemProb = 5;
        manaValue = 3;

        firePoint = transform.Find("FirePoint");
    }
    protected override void AttackCustom()
    {
        //GameObject proj = Instantiate(prefabBullet, firePoint.position, Quaternion.identity);

        
        Projectile_Enemy proj = EnemyProjPoolManager.eppm.GetFromPool("000");
        proj.SetUp(damage, 5, 1, 0, 0, 2f);
        proj.transform.position = firePoint.position;
        //proj.RotateProj(Projectile_Enemy.ProjDir.up);
       
        proj.SetDirection(target.transform);
        proj.RotateProj();
        float extraAngle = Random.Range(-5f,5f);
        proj.RotateProj(extraAngle);
        proj.Action();
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    public override void MoveCustom()
    {

        distance = Vector3.Distance(transform.position, base.target.transform.position);
        dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ

        if (distance >= 10)
        {   
            rb.MovePosition(transform.position + nextVec);
        }
        else
        {
            rb.MovePosition(transform.position - nextVec);
        }
        

    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "002";
    }
}
