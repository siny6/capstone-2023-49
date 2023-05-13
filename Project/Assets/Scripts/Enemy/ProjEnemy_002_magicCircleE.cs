using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjEnemy_002_magicCircleE : Projectile_Enemy
{
    float tickDelay;
    Animator animator;

    public override void InitEssentialProjInfo()
    {
        id_proj = "002";
    }


    public override void Action()
    {
        animator = GetComponent<Animator>();

        //int weight = splitNum + 1;

        animator.speed = 1f;

        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;


        tickDelay = animationLength;       // 탄속에 영향을 받음 

        int tickNum = (int) (lifeTime / tickDelay);                        //깜빡임 수 

        StartCoroutine(Tick(tickNum));
    }

    // ===================================
    // 깜빡거릴때마다 피해입히기 
    // ===================================
    public IEnumerator Tick(int tickNum)
    {
        for (int i = 0; i < tickNum; i++)
        {
            rb.simulated = true;
            yield return new WaitForFixedUpdate();

            rb.simulated = false;
            yield return new WaitForSeconds(tickDelay);
        }
    }
    public override void EnemyProjDestroy_custom()
    {

    }


    private void Update()
    {
        if (caster!=null)
        {
            myTransform.position = caster.position;
        }
        
    }
}
