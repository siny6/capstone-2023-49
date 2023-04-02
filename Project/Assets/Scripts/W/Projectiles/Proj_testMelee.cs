using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proj_testMelee : Projectile
{
    Animator animator;
    float animationLength;


    // =========== 오버라이드 =============
    // 애니메이터 정보를 설정함.
    // ===================================
    public override void Action()
    {
        SetSplitAngles(60,-60,180);
        
        animator = GetComponent<Animator>();   
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 

        //  애니메이션의 trail Render 폭 조정
        GetComponent<TrailRenderer>().widthCurve = new AnimationCurve(new Keyframe[] {
            new Keyframe(0f, 0f), 
            new Keyframe(0.5f, 0.5f * base.scale), 
            new Keyframe(1f, 0f)
        });
        
        // RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        // // 애니메이션 컨트롤러에 할당된 모든 애니메이션 클립에 대해 Scale 값을 설정합니다.
        // AnimationClip animationClip = controller.animationClips[0];

        // animationClip.scale = base.scale;


        Invoke("Split", animationLength* 0.3f);             // 애니메이션이 반 진행되면 분열 일으킴

        Destroy(transform.parent.gameObject, animationLength);      // 애니메이션 클립이 끝나면 파괴        

        
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
            base.splitPoint = transform.position;
            for (int i=0;i<3;i++)
            {
                // 기준이 되는 부모 오브젝트 회전 ( parent도 projectile을 상속하면 좋겠지만 안했음)
                GameObject parent = Instantiate(base.prefab_parent, base.splitPoint, transform.rotation);
                parent.GetComponent<ProjectileParent>().RotateProj( splitAngle[i] );

                Vector3 offset = parent.transform.right * 1.5f /base.scale;     // 조절하기 

                parent.GetComponent<ProjectileParent>().SetTarget(null, transform.position + offset);
                

                // 효과 생성하기 
                GameObject proj = Instantiate(base.prefab_proj, parent.transform);
                
                float splitWeight = 0.7f;

                proj.GetComponent<Projectile>().SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight, base.projNum,  base.penetration,  base.splitNum-1 , -1f);
                proj.GetComponent<Projectile>().Action();
            }
        }
    }
}
