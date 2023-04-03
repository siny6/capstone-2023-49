using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other)   // 플레이어와 충돌 시 플레이어 Hp감소
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
