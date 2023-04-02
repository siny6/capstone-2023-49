using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public GameObject bunbPrefab;
    public GameObject normalPrefab;
    public GameObject bombPrefab;
    Rigidbody2D bulletRigid;
    public Transform target;
    public float bunbSpeed = 2f;
    public float norSpeed = 5f;
    public float bombSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        Invoke("Firebunb", 5f);
        Invoke("Firenormal", 0.5f);
        Invoke("Firebomb", 10f);
    }

    void Firenormal()
    {
        GameObject normalBullet = Instantiate(normalPrefab, transform.position, Quaternion.identity);
        Rigidbody2D normalRigid = normalBullet.GetComponent<Rigidbody2D>();
        Vector2 direction = target.position - transform.position;               // �÷��̾� ����
        normalRigid.velocity = direction.normalized * norSpeed;                 // ���� ����ȭ�ؼ� �߻�
        Invoke("Firenormal", 0.5f);
    }

    void Firebunb()
    {
        GameObject bunbBullet = Instantiate(bunbPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bunbRigid = bunbBullet.GetComponent<Rigidbody2D>();
        Vector2 direction = target.position - transform.position;
        bunbRigid.velocity = direction.normalized * bunbSpeed;
        Invoke("Firebunb", 5f);
    }

    void Firebomb()
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject bombBullet = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bombRigid = bombBullet.GetComponent<Rigidbody2D>();
            //Vector2 direction = target.position - transform.position;
            bombRigid.velocity = Quaternion.AngleAxis(20f * i, Vector3.forward) * transform.right * bombSpeed;
            //bombRigid.velocity = direction.normalized * bunbSpeed;
        }
        Invoke("Firebomb", 10f);
    }

}
