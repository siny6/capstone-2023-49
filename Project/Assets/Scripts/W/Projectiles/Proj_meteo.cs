using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_meteo : Projectile
{
    // =========== 오버라이드 =============
    // 아무것도 안함  
    // ===================================
    public override void Action()
    {
        SetSplitAngles(0,120,240);

        base.rb.velocity = -transform.up * base.speed;  // 떨어짐
    }    

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
                proj.GetComponent<Projectile>().SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight, base.projNum, base.penetration,  base.splitNum-1 , 1f  * splitWeight);
                proj.GetComponent<Projectile>().RotateProj(splitAngle[i]); 
                proj.GetComponent<Projectile>().Action();
            }
        }
    }
}
