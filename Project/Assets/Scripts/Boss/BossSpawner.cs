using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] bosses;
    public GameObject boss1, boss2, boss3, boss4, boss5, boss6, boss7;
    public float spawnDelay = 5f;
    public int check = 0;

    // 현재 보스
    GameObject currBoss;


    void Start()  
    {
        bosses = new GameObject[] { boss1, boss2, boss3, boss4, boss5, boss6, boss7};
        // StartCoroutine(BossSpawn());         // 테스트 환경에서는 원할때 보스생성될 수 있도록 잠깐 주석처리
    }

    IEnumerator BossSpawn()  
    {
        int bossIndex = Random.Range(0, bosses.Length);
        yield return new WaitForSeconds(spawnDelay);
        GameObject bossInstance = Instantiate(bosses[bossIndex]);
    }


    // 보스 바로 생성 - 테스트 환경을 위함.                 
    public void SpawnBoss()              
    {
        int bossIndex = Random.Range(0, bosses.Length);
        currBoss = Instantiate(bosses[bossIndex]);
    }


    // 보스 즉사시키기 - 테스트 환경을 위함
    public void BossKill()
    {
        if (currBoss ==null)
        {
            return;
        }
        
        float dmg = 9999999f;
        
        EntityEffectController.eec.CreateDamageText(currBoss, dmg);
        
        currBoss.GetComponent<Boss>().bossHp -= dmg;
    }

}
