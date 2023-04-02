using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  // bomb �ִϸ��̼� ����ϱ� ���ؼ� �ʿ�
        Invoke("bomb", 3f);
    }
    
    void bomb()
    {
        animator.SetTrigger("bomb");          // bomb �ִϸ��̼� �ߵ� Ʈ����
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GameObject.Find("Player").GetComponent<Player>().Hp -= 1;
            float dmg = 1;
            EntityEffectController.eec.CreateDamageText(GameManager.gm.player, dmg);
            other.gameObject.GetComponent<Player>().Hp -= (int)dmg;
            Destroy(gameObject);
        }
    }

}
