using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>(); 
        Invoke("bomb", 3f);
    }
    
    void bomb()
    {
        animator.SetTrigger("bomb");          // bomb 애니메이션 시작
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)  // 플레이어와 충돌시 데미지 입힘, 데미지 띄우기
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
