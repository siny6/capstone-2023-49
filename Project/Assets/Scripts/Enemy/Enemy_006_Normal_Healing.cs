using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_006_Normal_Healing : Enemy
{
    public GameObject heal_zone; // ��ƼŬ�̳� ������ �� �����ֱ�
    public float radius = 5; // heal_zone ������
    public float heal_hp = 3; // �󸶳� ȸ��
    public float distance;
    Vector3 dirVec;

    public override void InitEnemyStatusCustom()
    {
        hpFull = 20;
        hp = 20;
        damage = 1;
        attackSpeed = 0.1f;
        speed = 3f;

        //lastAttackTime = -8f;
    }

    public override void MoveCustom()
    {
        // �������� : �÷��̾ ���������� �ֺ� ���� ����� �Ϲݸ��� or ���� ���ִ� �Ϲ� ���� ���󰡼� �ӹ�����

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

    protected override void AttackCustom()
    {
        StartCoroutine(HealEnemy());
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    IEnumerator HealEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        //var hitCollider = Physics2D.OverlapCircleAll(transform.position, radius);
        //Debug.Log("주변 몬스터 체력회복");
        //foreach (var hit in hitCollider)
        //{
        //    var ply = hit.GetComponent<Enemy>();
        //    if (ply)
        //    {
        //        var closePoint = hit.ClosestPoint(transform.position);
        //        var dis = Vector3.Distance(closePoint, transform.position);

        //        if(ply.hp + heal_hp >= ply.hpFull)
        //        {
        //            ply.hp = ply.hpFull;
        //        }

        //    }
        //}
        Projectile_Enemy proj = EnemyProjPoolManager.eppm.GetFromPool("002");
        proj.SetUp(5, 0, 1, -99, 0, 5);
        proj.myTransform.position = myTransform.position;
        proj.caster = myTransform;
        proj.Action();
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "006";
    }
}