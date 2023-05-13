using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_003_Normal_Bomb : Enemy
{
    float distance;
    float radius = 5; // 폭발 데미지 범위
    public int bombDamage = 4;
    bool boooom = false;     // *****************************************************

    
    public GameObject particle_Boom;

    public override void InitEnemyStatusCustom()
    {
        hpFull = 25;
        hp = 25;
        damage = 2;
        attackSpeed = 0.1f; // *****************************************************
        speed = 1;

        itemProb = 5;
        manaValue = 3;

        // 폭파중 죽은 애 살아났을때 안터지게 
        boooom = false;
        StopAllCoroutines();
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


    public override void DieCustom() // *****************************************************
    {     
        // obj.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // base.hp = 15 ;                               // *****************************************************
        // base.DropItem();
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
        

        for (int i = 0; i < 3; i++)
        {
            rend.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(0.5f);
            rend.color = new Color(0, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }


        // var hitCollider = Physics2D.OverlapCircleAll(transform.position, radius);
        // foreach (var hit in hitCollider)
        // {
        //     var ply = hit.GetComponent<Player>();
        //     if (ply)
        //     {
        //         var closePoint = hit.ClosestPoint(transform.position);
        //         var dis = Vector3.Distance(closePoint, transform.position);

        //         var damagePercent = Mathf.InverseLerp(radius , 0, dis);
        //         //var damagePercent = bombDamage - (dis / 10);
        //         ply.OnDamage(damagePercent * bombDamage);
        //     }
        // }
        // GameObject explosion = Instantiate(particle_Boom, transform.position, Quaternion.identity);
        // yield return new WaitForSeconds(1f);
        // Destroy(explosion);

        Projectile_Enemy proj = EnemyProjPoolManager.eppm.GetFromPool("001");

        proj.SetUp(2, 0, 1f, -99, 0, 0.4f);
        proj.myTransform.position = transform.position;
        proj.SetUp_special(true);
        proj.Action();
        //Destroy(proj.gameObject, 0.5f);

        CleanDeath();
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "003";
    }
}
