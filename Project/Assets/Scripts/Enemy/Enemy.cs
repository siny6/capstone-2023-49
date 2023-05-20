using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool isBoss;     // 보스몬스터인지
    public bool deathAnimationEnd;
    public bool ready;
    public float deltaScale;

    public bool oneShot = false;
    public bool shot = false;
    
    protected float speed; // �̵��ӵ�
    
    public float hp; // ü��

    public float hpFull;     // ****************

    public int def;

    public int damage; // ���ݷ�

    public bool strongAttack = false;

    public float attackSpeed;   // ���ݼӵ�(�ʴ� ���ݼӵ�)

    protected bool state = true; // ���� ���� true : �⺻����, false : �����̻�
    public float lastAttackTime;

    public bool canKnockBack = true;
    public float knockBack_time = 0.2f;

    public float lastMoveTime;

    public bool canMove = true;
    public bool canAttack_ = true;      // can damage player 
    public bool canAttack       // ���� ���� ����
    {
        get
        {
            if (DirectingManager.dm.onDirecting || !canAttack_ || attackSpeed == 0)                
            {
                return false;
            }            
            // ���� ���� �� �����̻�(cc�� ) �Ǻ�
            if (oneShot && shot)
            {
                return false;
            }


            float attackDelay = 1 / attackSpeed;
            if (lastAttackTime + attackDelay <= GameManager.gm.totalGameTime)
            {
                return true;
            }
            return false;
        }
    }
    public bool canMoving
    {
        get
        {
            if (DirectingManager.dm.onDirecting || !canMove || speed == 0)
            {
                return false;
            }

            float moveDelay = 1 / speed;
            if (lastMoveTime + moveDelay <= GameManager.gm.totalGameTime)
            {
                return true;
            }
            return false;
        }
    }

    // Item
    public float itemProb;  // probability of drop other items (not mana)
    public float manaValue; // mana acquisition amount



    protected Rigidbody2D rb;
    public Transform myTransform;
    public Transform center;

    public GameObject target; // ���� ���
    public Vector3 target_proj;

    public string id_enemy;

    //==========
    //애니메이션 관련                   //  *******************************************************************************
    protected Animator animator;

    public bool isDead = false;

    public Vector3 originScale;

    // dot damage Q
    public bool onBleeding;
    public Queue<float> dotQ = new Queue<float>();
    // public int currDotNum;
    //=====================================================================================

    // 초기화작업 (공통)                         
    public void InitEnemyStatus()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        animator = GetComponent<Animator>();
        

        target = Player.Instance.gameObject; // ���� ��� = �÷��̾�

        

        myTransform.localScale = originScale;
        myTransform.rotation = Quaternion.identity;


        InitEnemyStatusCustom();                    // 개별 능력치 먼저 초기화

        hp = hpFull;

        StartCoroutine(BattleFlow());
        StartCoroutine(MoveFlow());
    }

    // 개별 능력치 초기화
    public abstract void InitEnemyStatusCustom();

    public abstract void InitEssentialEnemyInfo();

    // ������(exp) ���
    public void DropItem()
    {
        // default : drop mana.
        DropItem item = ItemPoolManager.ipm.SpawnItem("000", manaValue, transform.position);

        if (Random.Range(0,100) < itemProb)
        {
            // drop heal item if you are 'lucky'
            DropItem luckyItem = ItemPoolManager.ipm.SpawnItem("001", Player.Instance.Hp_item_up, transform.position);
        }
    }

    // ����
    protected void Death()                                  //********************************
    {             
        GameManager.gm.KillCount += 1;

        isDead = true;
        ready = false;

        dotQ.Clear();

        DieCustom();
        DropItem();
        // StopCoroutine( MoveAnimation());
        rb.simulated = false;

        StartCoroutine( DeathAnimation());
    }

    // ===============================
    // Clean Death : not drop item, not increase score 
    // ================================
    public void CleanDeath()
    {
        // GameManager.gm.KillCount += 1;
        isDead = true;
        ready = false;

        dotQ.Clear();


        // DieCustom();
        // DropItem();
        // StopCoroutine( MoveAnimation());
        rb.simulated = false;

        
        StartCoroutine( DeathAnimation());        
    }



    // 죽는 애니메이션 (빙글빙글 돌면서 크기가 작아짐)
    public IEnumerator DeathAnimation()
    {
        if (!isBoss)
        {
            int tickNum = 10;

            float angle= 36;
            for (int i=tickNum-1;i>0;i--)
            {
                myTransform.localScale = originScale * 0.1f * i;
                myTransform.rotation = Quaternion.Euler(0,0,angle);

                angle+=36;
                yield return new WaitForSeconds(0.05f);
            }
            deathAnimationEnd = true;
        }
        // wait until animation end 
        yield return new WaitUntil(() => deathAnimationEnd);    

        EnemyPoolManager.epm.TakeToPool(this);
    }

    // 평상시 이동 애니메이션 (두근두근거림)
    public IEnumerator MoveAnimation()
    {
        if (!isBoss)
        {
            ready = true;
        }
        yield return new WaitUntil(()=>ready);

        deltaScale = (isBoss)?0.001f: 0.005f;
        
        while(!isDead)
        {
            for(int i=0;i<20;i++)
            {
                myTransform.localScale += originScale * deltaScale;
                yield return null;
            }
            for(int i=0;i<20;i++)
            {
                myTransform.localScale -= originScale * deltaScale;
                yield return null;
            }
        }
    }


    // get dmg for direct attack ( knockback & bleed ) 
    public void Damaged(int damage, Vector3 hitPoint, float knockbackPower)
    {
        hp -= damage;

        //drain
        int prob = Random.Range(1, 101);
        if (prob <= Player.Instance.Drain_prob)
            Player.Instance.ChangeHp(Player.Instance.Drain);


        //knockback
        if (canKnockBack && knockbackPower>0)
        {
            StartCoroutine(KnockBack(knockbackPower, hitPoint));
        }
        

        //bleed
        Bleed(4);


        // detect death
        if (hp <= 0)
        {
            Death();
        }
    }

    // get dmg for indirect attack ( not knockback)
    public void Damaged(float damage)
    {
        hp -= damage;

        //drain
        int prob = Random.Range(1, 101);
        if (prob <= Player.Instance.Drain_prob)
            Player.Instance.ChangeHp(Player.Instance.Drain);
        
        // detect death
        if (hp <= 0)
        {
            Death();
        }
    }


    // knockBack 
    public IEnumerator KnockBack(float power, Vector3 pos)
    {
        float duration = 0.1f;
        Stunned(duration);

        Vector3 dir = (center.position - pos).normalized;
        rb.velocity = dir * power;
        // rb.AddForce(dir * power );

        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
        

    }
    //===================================================
    // stun : can't move and attack
    //===================================================
    public void Stunned(float time)
    {
        canMove = false;
        canAttack_ = false;
        rb.velocity = Vector3.zero;
        
        StartCoroutine( SetDuration_stun(time));
    }

    //===================================================
    // release stun
    //===================================================
    public IEnumerator SetDuration_stun(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
        canAttack_ = true;
    }


    //===================================================================================================
    // dot damage
    //=========================================
    public void Bleed(float dpt)
    {
        if(isDead)
        {
            return;
        }
        
        int num = Random.Range(0, 100);
        if ( num < Player.Instance.bleedingLevel * 25)
        {
            // Debug.Log("출혈");
            int tickNum = 6;
            Dot_Init(dpt, tickNum);
        }
    }

    //========================================
    // dot init : set waitQ /  send dot from waitQ to dotQ continuously  (dmg per tick, tickNum)
    //=======================================
    public void Dot_Init(float dpt, int tickNum)
    {
        Queue<float> waitQ = new Queue<float>();
        for(int i=0;i<tickNum;i++)
        {
            waitQ.Enqueue(dpt);
        }

        StartCoroutine(Dot_sendQ( waitQ ) );
    }

    //========================================
    // send dmg from waitQ to dotQ 
    //=======================================
    public IEnumerator Dot_sendQ( Queue<float> waitQ )
    {
        while(waitQ.Count>0)
        {
            if (isDead)
            {
                waitQ.Clear();
                break;
            }
            
            dotQ.Enqueue( waitQ.Dequeue() );

            yield return new WaitForSeconds(0.5f);
        }

    }
    //========================================
    // get dot dmg
    //=======================================
    public IEnumerator GetDotDamage()
    {
        yield return null;  // delay an frame to wait for every waitQ 

        if (!onBleeding)
        {
            onBleeding = true;
            while(true)
            {
                Damaged_dot();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    
    //=================
    // get dot dmg
    //=================
    public void Damaged_dot()
    {
        if (isDead)
        {
            return;
        }
        
        if (dotQ.Count==0)
        {
            onBleeding = false;
            return;
        }
        // 현재 dotQ에 있는 dmg들을 계산해서 피해입음 
        float totalTickDmg = 0f;
        int num = 0;
        while(dotQ.Count>0)
        {
            totalTickDmg += dotQ.Dequeue();
            num++;
        }
        
        Vector3 hitPoint = center.position;
        EffectPoolManager.epm.CreateText(hitPoint, totalTickDmg.ToString(), new Color(0.9f,0.5f,0.5f,1));
        // bleeding effect
        Effect effect = EffectPoolManager.epm.GetFromPool("102");
        effect.InitEffect(center.position);
        effect.ActionEffect();
        //
        // EffectPoolManager.epm.CreateText(hitPoint, num.ToString(), Color.red);
        Damaged(totalTickDmg);
    }

    //===================================================================================

    public void Healing(float heal)
    {
        // 1. 마지막 공격받은 시간에서 일정시간 지나면 스스로 힐
        // 2. 버프몬스터 근처에 있다가 힐 받음

        EffectPoolManager.epm.CreateText(myTransform.position, heal.ToString(), new Color(0.2f, 0.4f, 0.1f, 1.0f));
        hp += heal;
        if (hp > hpFull)
        {
            hp = hpFull;
        }
    }


    //=========================================================================================
    //===================================
    // ������ ���� �÷ο�
    //===================================
    public IEnumerator BattleFlow()
    {
        canAttack_ = true;
        StartCoroutine( MoveAnimation());
        StartCoroutine( GetDotDamage() );       // detect bleeding dmg continuously 
        while (!isDead)     // 죽은 상태가 아닐때 전투플로우 
        {
            Attack();

            float attackDelay = 1 / attackSpeed;
            yield return new WaitForSeconds(attackDelay);
        }
    }

    public IEnumerator MoveFlow()
    {
        canMove = true;
        while (!isDead)
        {
            Move();

            float moveDelay = 0.5f;
            yield return new WaitForSeconds(moveDelay);
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
            lastAttackTime = GameManager.gm.totalGameTime;   // ������ ���� �ð� ����

            AttackCustom();
        }
    }

    // ����
    protected abstract void AttackCustom();

    //=============================================
    public void Move()
    {
        if (canMoving)
        {
            lastMoveTime = GameManager.gm.totalGameTime;
            MoveCustom();
        }
    }

    public abstract void MoveCustom();


    //==============================================
    void Awake()
    {
        originScale = transform.localScale;         // 가장 처음
        myTransform = transform;
        center = transform.Find("Center");
    }


    void Start()
    {
        InitEnemyStatus();

    }

    void FixedUpdate()
    {
        if (DirectingManager.dm.onDirecting)
        {
            return;
        }
        
        //if(!isDead && canMove)
        //{
        //    MoveCustom();
        //}
    }



    public abstract void DieCustom(); //********************************


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && canAttack_)    // player get dmg when canAttack_
    //    { 
    //        Vector3 hitPoint = center.position;

    //        int dmg = damage;

    //        if (dmg != 0 )
    //        { 
    //            // Player.player.OnDamage(dmg);
    //            Player.Instance.OnDamage(damage, hitPoint, strongAttack);
    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canMove = false;
            Vector3 hitPoint = center.position;
            Debug.Log("벽 닿음");
            StartCoroutine(KnockBack(20f, hitPoint));
            canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack_)    // player get dmg when canAttack_
        {
            Vector3 hitPoint = center.position;

            int dmg = damage;

            if (dmg != 0)
            {
                Player.Instance.OnDamage(damage, hitPoint, strongAttack);
            }
        }
    }

}
