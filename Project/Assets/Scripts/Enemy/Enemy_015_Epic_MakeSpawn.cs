using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_015_Epic_MakeSpawn : Enemy
{
    
    // �ൿƯ¡
    // 1. �÷��̾�Լ� ��������.
    // 2. �����ð����� �⺻ ���� 3������ ��ȯ�Ѵ�.

    // ����
    // �÷��̾� ���� �־������� �� �� �������� ����(collider)�� �մ´�.
    // body type : kinematic --> dynamic ���� �ٲٴϱ� ��� �������� ���� ������ �ո��� �ϴµ� �� �ȶ���.
    // idea : collision ������ �� �������� ���� ������ �ڷ� �������� �ڵ�����
    //EnemyPoolManager epm;
    public float distance;
    Vector3 dirVec;

    float timer;
    float spawnRate;
    Vector2 spawnPos;

    public override void DieCustom()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        hp = hpFull;
    }

    public override void InitEnemyStatusCustom()
    {
        hpFull = 40;
        hp = 40;
        damage = 3;
        attackSpeed = 0.1f;
        speed = 3f;
        spawnRate = 7;
        timer = 0;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "015";
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

    protected override void AttackCustom()
    {
        // �÷��̾ ������ �÷��̾��� ü���� ����
        timer += Time.deltaTime;
        if (timer > spawnRate && isDead == false)
        {
            timer = 0;
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                //GameObject enemy = GameManager.gm.pool.Get(0);
                Enemy enemy = EnemyPoolManager.epm.SpawnEnemy("001", 1, 0);
                enemy.myTransform.position = myTransform.position;
            }
        }
    }


}
