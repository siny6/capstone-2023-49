using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_meteo_default : Projectile
{
    public Animator animator;
    public float animationLength;


    // =========== 오버라이드 =============
    // 스플릿을 위한 설정.
    // ===================================
    public override void Action()
    {
        SetSplitAngles(0,120,240);

        animator = transform.GetChild(0).GetComponent<Animator>();   
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 

        // Invoke("ColliderOn", animationLength* 0.4f);             // 애니메이션이 거의 끝나면 콜라이더 on
        StartCoroutine(ColliderOn());

        Destroy(gameObject, animationLength);      // 애니메이션 클립이 끝나면 파괴   
    }    

    // ==============================
    // 콜라이더 키기 - 
    // ===================================
    IEnumerator ColliderOn()
    {
        yield return new WaitForSeconds(animationLength * 0.9f);
        // GetComponent<BoxCollider2D>().enabled = true;
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    
    // 메테오는 파괴될때 분열
    void OnDestroy()
    {
        Split();
    }


        
    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            //분열지점 세팅
            // base.splitPoint = transform.position;
            for (int i=0;i<3;i++)
            {
                base.splitPoint = transform.position;
                
                // 분열각도에 맞게 발사                
                GameObject proj = Instantiate(base.prefab_proj, base.splitPoint, transform.rotation);

                
                float splitWeight = 0.7f;
                proj.GetComponent<Projectile>().SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight,   base.projNum, 1,  base.splitNum-1 , 1f * splitWeight);
                proj.GetComponent<Projectile>().RotateProj(splitAngle[i]); 
                proj.GetComponent<Projectile>().Action();
            }
        }
    }
}
