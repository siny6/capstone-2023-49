using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
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

    // Update is called once per frame
}
