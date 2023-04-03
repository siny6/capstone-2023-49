using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Boss_Kill uiBoss_Kill;
    public Rigidbody2D rigid;
    public Transform target;                  // 플레이어 위치
    public float speed;                       // 보스 속도
    public float bossHp = 100f;               // 보스 체력
    public bool bossDied;

    public void Awake()
    {
        speed = 2f;
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
        bossHp = 100f;
        bossDied = false;
    }

    private void FixedUpdate()
    {
        if (bossHp > 0 && !bossDied)
        {
            bossHp += 0.2f;
        }
        if (bossHp <= 0)
        {
            bossDied = true;
            StartCoroutine(Die());
        }
    }

    // 보스 체력 0 이하로 내려갈 시 3초 뒤 보스 오브젝트 파괴
    IEnumerator Die()   
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        GameObject.Find("Boss").GetComponent<BossUI>().check = 0f;
    }


    // 보스 피격 시 플레이어 HP 감소
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            float dmg = 3;
            EntityEffectController.eec.CreateDamageText(GameManager.gm.player, dmg);            
            GameManager.gm.player.GetComponent<Player>().Hp -= (int)dmg;
        }
    }
}
