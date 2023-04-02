using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunB : MonoBehaviour
{
    public GameObject splitBulletPrefab;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SplitBullet", 2f);      // 2���� SplitBullet �Լ� �۵�
        Destroy(gameObject, 5f);        // 5���� ������Ʈ �ı�
    }

    void SplitBullet()
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject splitBullet = Instantiate(splitBulletPrefab, transform.position, Quaternion.identity);   // ������ �ν��Ͻ� ����
            Rigidbody2D splitRigid = splitBullet.GetComponent<Rigidbody2D>();                                   
            splitRigid.velocity = Quaternion.AngleAxis(20f * i, Vector3.forward) * transform.right * speed;     // 20���� ������ �и��ϰ� �߻�
        }
        Invoke("SplitBullet", 2f);  
        //Destroy(gameObject);
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
