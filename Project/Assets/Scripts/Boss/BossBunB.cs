using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunB : MonoBehaviour
{
    public GameObject splitBulletPrefab;
    public float speed = 2f;
    void Start()
    {
        Invoke("SplitBullet", 2f);      // 2초 후 SplitBullet 함수 실행
        Destroy(gameObject, 5f);        
    }

    // 18방향으로 분열되어 날아가는 총알 생성
    void SplitBullet() 
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject splitBullet = Instantiate(splitBulletPrefab, transform.position, Quaternion.identity); 
            Rigidbody2D splitRigid = splitBullet.GetComponent<Rigidbody2D>();                                   
            splitRigid.velocity = Quaternion.AngleAxis(20f * i, Vector3.forward) * transform.right * speed;  
        }
        Invoke("SplitBullet", 2f);  
    }

    // 플레이어와 충돌 시 플레이어 Hp감소
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float dmg = 1;
            EntityEffectController.eec.CreateDamageText(GameManager.gm.player, dmg);
            other.gameObject.GetComponent<Player>().Hp -= (int)dmg;
            Destroy(gameObject);
        }
    }
}
