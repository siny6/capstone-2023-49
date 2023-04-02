using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] makePoints; // ���� ���� ��ġ�� ��� �迭
    float timer;
    float spawnRate = 1f;

    void Awake()
    {
        makePoints = GetComponentsInChildren<Transform>();
    }

    //============================
    // num만큼 적 생성 
    //============================
    public void SpawnEnemy(int num)
    {
        for (int i=0;i<num;i++)
        {
            GameObject enemy = GameManager.gm.pool.Get(Random.Range(0, 1));
            enemy.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;      
        }

    }
    public void SpawnBomb(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject bomb = GameManager.gm.pool.Get(1);
            bomb.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;
        }

    }



    // 박스생성
    public void SpawnBox(int num)   // *****************************************************
    {
        for (int i=0;i<num;i++)
        {
            GameObject box = GameManager.gm.pool.Get(2);
            
            float newX = Random.Range(-16f, 14f);
            float newY = Random.Range(-8f, 7f );

            Vector3 newPos = new Vector3(newX, newY, 0);

            box.transform.position = newPos;
        }
    }



    void Update()
    {
        // 자동으로 생기는게 아니라 버튼 누르면 생성되도록 함.
        
        
        // timer += Time.deltaTime;

        // if(timer > spawnRate)
        // {
        //     timer = 0;
        //     GameObject enemy = GameManager.gm.pool.Get(Random.Range(0, 1));
        //     enemy.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;
        // }
        
    }
}
