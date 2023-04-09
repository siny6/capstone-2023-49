using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;

    public GameObject prefabBullet;

    public override void InitEnemyStatusCustom()
    {
        hpFull = 15;
        hp = 15;
        speed = 2;
        attackSpeed = 3f;

        firePoint = transform.GetChild(0);
        target = GameManager.gm.player.gameObject;
    }
    protected override void AttackCustom()
    {
        GameObject proj = Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile_Enemy>().SetUp(damage, 5, 1, 1, 0, 0, 3f);
        proj.GetComponent<Projectile_Enemy>().SetDirection(target.transform);
        proj.GetComponent<Projectile_Enemy>().RotateProj();
        proj.GetComponent<Projectile_Enemy>().Action();
    }

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    public override void MoveCustom()
    {

        dirVec = base.target.transform.position - transform.position; // 방향 = 타겟 위치 - 내 위치
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 다음 위치
        rb.MovePosition(transform.position + nextVec);
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정

    }

}
