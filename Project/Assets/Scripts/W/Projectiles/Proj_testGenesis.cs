using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//========================================
//  Weapon_testGeneis의 ray(목표지점) : 생성 후 일정 시간 후 사라짐 
//========================================
public class Proj_testGenesis : Projectile
{
    // =========== 오버라이드 =============
    // 아무것도 안함  
    // ===================================
    public override void Action()
    {
        SetSplitAngles(0,120,240);
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
                GameObject proj = Instantiate(base.prefab_proj, base.splitPoint, Quaternion.identity);

                
                float splitWeight = 0.5f;
                proj.GetComponent<Projectile>().SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight,   base.projNum, base.penetration,  base.splitNum-1 , 1f);
                proj.GetComponent<Projectile>().RotateProj(splitAngle[i]); 

                proj.transform.localPosition -= transform.up*2;

                proj.GetComponent<Projectile>().Action();
            }
        }
    }

}
