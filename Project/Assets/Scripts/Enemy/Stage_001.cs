using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_001 : Stage
{
    Vector3 spawnPos;
    // Vector3 spawnRange;
    int cnt = 1;
    int weight = 2;
    int location;
    float newX;
    float newY;
    int spawn_cnt = 3;

    public override void InitStageInfo_custom()
    {
        id_stage = "001";

        num_stage = 1;

        routineOnGoing = true;
    }

    public override void StartStageRoutine_custom()
    {
        //var n = StartCoroutine(spawn_Enemy("001", cnt + weight, 0f, 0, 40f));
        //var eNormal = StartCoroutine(spawn_Enemy("011", 1, 15f, 0, 300f));

        //var nDash = StartCoroutine(spawn_Enemy("004", cnt + weight, 20f, 0, 30f));
        //var eDash = StartCoroutine(spawn_Enemy("014", cnt + weight, 30f, 0, 50f));

        //var nRanger = StartCoroutine(spawn_Enemy("002", cnt + weight, 45f, 0, 70f));
        //var eRanger = StartCoroutine(spawn_Enemy("012", cnt + weight, 55f, 0, 300f));

        var nGroup = StartCoroutine(spawn_Group());

        //var nBomb = StartCoroutine(spawn_Enemy("003", cnt + weight, 50f, 0, 80f));
        
        //var nHeal = StartCoroutine(spawn_Enemy("006", cnt + weight, 100f, 0, 300f));

        //var boss = StartCoroutine(spawn_Enemy("b_001", 1, 5f, 0, ));
        //BossSpawnManager.bsm.SpawnBoss();
        //EnemyPoolManager.epm.SpawnBoss("b_001");
        //var boss = StartCoroutine(spawn_Boss());

        //while (routineOnGoing)
        //{
        //    if (StageManager.sm.currStageTimer > 10f)
        //    {
        //        StopCoroutine(n);
        //    }
        //}
        //StartCoroutine(spawn_Boss());

    }


    // 001:�⺻, 002:Ranger, 003:Bomb, 004:Dash, 005:makeSpawn, 006:Healer

    public IEnumerator spawn_Enemy(string id, int num, float spawnTime, int flag, float stopTime)
    {
        // id : enemy_id | num : enemy count | spawnTime : start time spawn | flag : ...;; | stopTime : stop time spawn
        yield return new WaitForSeconds(spawnTime);
        while (routineOnGoing)
        {
            if(StageManager.sm.currStageTimer > stopTime)
            {
                num = 0;
                yield break;
            }
            //spawnPos = GetRandomSpawnPos_spawnRange();
            Enemy enemy = EnemyPoolManager.epm.SpawnEnemy(id, num + weight, flag);
            enemy.InitEnemyStatus();
            //StartCoroutine(add_weight());
            yield return new WaitForSeconds(3f);

        }
        //Enemy enemy = EnemyPoolManager.epm.SpawnEnemy(id, num + weight, flag);
        //enemy.InitEnemyStatus();

    }

    IEnumerator spawn_Box()
    {
        while (routineOnGoing)
        {
            // ������ Ȯ���� ���� ������ ���� �ð����� ���� ������ �ʰ� ������ ����
            spawnPos = GetRandomSpawnPos_spawnRange();
            Enemy box = EnemyPoolManager.epm.SpawnEnemy("000", cnt + weight, 0);
            box.InitEnemyStatus();
            //StartCoroutine(add_weight());
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator spawn_Normal()
    {
        while (routineOnGoing)
        {
            spawnPos = GetRandomSpawnPos_spawnRange();
            Enemy enemy = EnemyPoolManager.epm.GetFromPool("001");
            enemy.InitEnemyStatus();
            enemy.transform.position = spawnPos;

            yield return new WaitForSeconds(2f);
        }
        //yield return 0;
    }

    IEnumerator spawn_Group()
    {
        //yield return new WaitForSeconds(Random.Range(45, 110));
        //yield return new WaitForSeconds(10f);
        while ( routineOnGoing)
        {
            location = Random.Range(0, 4);
            select_Location(location);

            //if (location == 2 || location == 3)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j = 0; j < 4; j++)
            //        {
            //            //j,i
            //            Enemy enemy = EnemyPoolManager.epm.GetFromPool("007");
            //            enemy.InitEnemyStatus();

            //            enemy.myTransform.position = new Vector3(newX, newY + (j));
            //        }
            //        newX += 1;
            //    }

            //}
            //else
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        for (int j = 0; j < 4; j++)
            //        {
            //            //j,i
            //            Enemy enemy = EnemyPoolManager.epm.GetFromPool("007");
            //            enemy.InitEnemyStatus();

            //            enemy.myTransform.position = new Vector3(newX + (j), newY);
            //        }
            //        newY += 1f;
            //    }
            //}
            //spawn_cnt--;

            //Debug.Log("생성");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //j,i
                    Enemy enemy = EnemyPoolManager.epm.GetFromPool("007");
                    enemy.InitEnemyStatus();

                    //enemy.myTransform.position = new Vector3(i + 1 + (0.5f * j), newY);
                    enemy.myTransform.position = new Vector3(newX, newY + (j));
                }
                newX++;
            }

            //Enemy enemy = EnemyPoolManager.epm.GetFromPool("007");
            //enemy.InitEnemyStatus();
            //enemy.myTransform.position = new Vector3(newX, newY);

            //Debug.Log(" 5초");
            yield return new WaitForSeconds(5f);
            //yield return new WaitForSeconds(Random.Range(10f, 30f));

        }
    }

    IEnumerator spawn_Boss()
    {
        //spawnPos = GetRandomSpawnPos_spawnRange();
        //Enemy enemy = EnemyPoolManager.epm.GetFromPool("b_001");
        //enemy.InitEnemyStatus();
        //enemy.transform.position = spawnPos;

        //yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(15f);
        BossSpawnManager.bsm.SpawnBoss();

        //EnemyPoolManager.epm.SpawnBoss("b_001");

    }


    // ����ġ ���� �ڷ�ƾ �߰� 
    IEnumerator add_weight()
    {
        weight++;
        yield return new WaitForSeconds(0.0001f);
    }

    void select_Location(int num)
    {
        newX = 0;
        newY = 0;
        if(num == 0)
        {
            newX = Random.Range(-30, 30);
            newY = 30;
        }
        else if(num == 1)
        {
            newX = Random.Range(-30, 30);
            newY = -30;
        }
        else if(num == 2)
        {
            newX = 30;
            newY = Random.Range(-30, 30);
        }
        else
        {
            newX = -30;
            newY = Random.Range(-30, 30);
        }
    }
}
