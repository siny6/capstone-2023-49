using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : Enemy
{
    float distance;
    // float range;

    bool boooom = false;     // *****************************************************

    public override void InitEnemyStatus()
    {
        hp = 15;            // *****************************************************

        damage = 5;

        speed = 1;
    }

    protected override void AttackCustom()
    {
        
    }
    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        rb.MovePosition(transform.position + nextVec);
        rb.velocity = Vector2.zero; // ������ �ӵ� 0���� ����
    }


    public override void DieCustom(GameObject obj) // *****************************************************
    {     
        obj.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        base.hp = 15 ;                               // *****************************************************
        base.DropItem();
    }



    private void Update()
    {
        distance = Vector2.Distance(transform.position, base.target.transform.position);
        
        if (distance < 3f && !boooom)               // *****************************************************
        {
            boooom =true;
            gameObject.GetComponent<Collider2D>().enabled = true;
            // �÷��̾� ���ݿ� �浹���� ���� ����.

            StartCoroutine(Bomb());
        }
    }
    IEnumerator Bomb()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        Collider2D coll = GetComponent<Collider2D>();


        for (int i = 0; i < 3; i++)
        {
            rend.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(0.5f);
            rend.color = new Color(0, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }

        Death(gameObject);
    }
}
