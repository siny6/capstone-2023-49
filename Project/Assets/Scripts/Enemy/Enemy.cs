using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float speed; // �̵��ӵ�
    
    [SerializeField] // ************************************************************** �̰��ϸ� private�̳� protected�� �ν����� â���� �� �� ����
    protected float hp; // ü��

    protected float damage; // ���ݷ�

    public float attackSpeed;   // ���ݼӵ�(�ʴ� ���ݼӵ�)

    protected bool state = true; // ���� ���� true : �⺻����, false : �����̻�
    public float lastAttackTime;
    public bool canAttack       // ���� ���� ����
    {
        get
        {
            if (attackSpeed == 0)                
            {
                return false;
            }            
            // ���� ���� �� �����̻�(cc�� ) �Ǻ�
            float attackDelay = 1 / attackSpeed;
            if (lastAttackTime + attackDelay <= GameManager.gm.gameTime)
            {
                return true;
            }
            return false;
        }
    }
    public GameObject[] item; // ü���� ���� �� ����ϴ� ������ ����Ʈ

    protected Rigidbody2D rb;

    public GameObject target; // ���� ���

    //=====================================================================================

    // Status �ʱ�ȭ
    public abstract void InitEnemyStatus();

    // ����
    protected abstract void AttackCustom();

    // ������(exp) ���
    public void DropItem()
    {
        // for (int i = 0; i < item.Length; i++)
        //     Instantiate(item[i], transform.position, Quaternion.identity);
        // Instantiate(item[0], transform.position, Quaternion.identity);   
        
        //�ϴ��� ����ġ ����� - ���߿� Ȯ�������� �ڼ�, ȸ�������� ����� ���� ********************************
        GameObject dropItem = Instantiate(item[0], GameObject.Find("Items").transform);

        dropItem.transform.position = transform.position;
    }

    // ����
    protected void Death(GameObject obj)                                  //********************************
    {
        DieCustom(obj);
        
        // obj.SetActive(false);
        // gameObject.GetComponent<Collider2D>().enabled = true;
        // hp = 100;
        // DieCustom();
        // DropItem();
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
    }

    // ���� ����
    public void Damaged(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death(gameObject);
        }
    }

    //=========================================================================================
    //===================================
    // ������ ���� �÷ο�
    //===================================
    public IEnumerator BattleFlow()
    {
        while (true)
        {
            AttackCustom();

            float attackDelay = 1 / attackSpeed;
            yield return new WaitForSeconds(attackDelay);
        }
    }

    //=======================================
    // ����_����
    //=======================================
    public void Attack()
    {
        // ���ݰ����� ��Ȳ�϶� ����
        if (canAttack)
        {
            lastAttackTime = GameManager.gm.gameTime;   // ������ ���� �ð� ����

            AttackCustom();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.gm.player; // ���� ��� = �÷��̾�

        // �ʱ�ȭ �ϰ� ���� �÷ο� ����
        InitEnemyStatus();
        StartCoroutine(BattleFlow());
    }

    void FixedUpdate()
    {
        MoveCustom();
    }


    public abstract void MoveCustom();

    public abstract void DieCustom(GameObject obj); //********************************




    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            float dmg = damage;

            if (dmg != 0 )
            {
                EntityEffectController.eec.CreateDamageText(GameManager.gm.player, dmg);
            
                GameManager.gm.player.GetComponent<Player>().Hp -= (int)dmg;
            }

            //rigid.isKinematic = true;
        }
    }
}
