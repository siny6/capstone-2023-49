using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
// testRange에서 발사되는 총알
// =============================================
public class Proj_testRange : Projectile
{

    // =========== 오버라이드 =============
    // 초기설정 후 액션 (행동 개시) - 적 방향으로 머리를 회전시킨후 이동한다.  
    // ===================================
    public override void Action()
    {
        SetSplitAngles(0,120,240);
        
        base.rb.velocity = transform.up * base.speed;
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
                // 분열각도에 맞게 발사
                GameObject proj = Instantiate(base.prefab_proj, splitPoint, transform.rotation);
                proj.SetActive(true); // 이상하게 활성화 직접해줘야함
                proj.GetComponent<Collider2D>().enabled = true; // 이상하게 활성화 직접해줘야함


                float splitWeight = 0.7f;
                proj.GetComponent<Projectile>().SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight,   base.projNum, base.penetration,  base.splitNum-1 , 5f);
                proj.GetComponent<Projectile>().RotateProj(splitAngle[i]); 
                proj.GetComponent<Projectile>().Action();
            }
        }
    }



    void OnDestroy()
    {
        Split();
    }



}
