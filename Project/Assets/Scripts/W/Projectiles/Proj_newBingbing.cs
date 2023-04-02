using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==================================================
// Weapon_testBingbing의 투사체 : 플레이어 주위를 빙빙 돈다. 
//==================================================
public class Proj_newBingbing : Projectile
{
    public GameObject parent;   // 분열의 중심
    
    
    // =========== 오버라이드 =============
    // 생성되면 바로 분열
    // ===================================
    public override void Action()
    {

        Split();
 
    }


    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // Debug.Log(gameObject.name + base.splitNum.ToString());
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            //
            parent = Instantiate(base.prefab_parent, transform);
            parent.GetComponent<ProjectileParent>().SetTarget(transform, Vector3.zero );

            ArrangeProj();
        }
    }

    // ===================================
    // 분열을 위해 배치해야함.
    // ===================================
    public void ArrangeProj()
    {
        float rotationPerUnit = 360/base.projNum;       // 투사체 당 각도
        float currRotation = 0;
        for (int i=0;i<base.projNum;i++)
        {
            GameObject proj = Instantiate(base.prefab_proj, parent.transform);

            proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currRotation));
            currRotation+=rotationPerUnit;

            float splitWeight = 0.7f;                  // 프리팹 스케일은 1로 통일시켜야겟음.
            
            proj.transform.localPosition = proj.transform.up * 2 * splitWeight ;
            proj.GetComponent<Projectile>().SetUp(base.damage * splitWeight , base.speed,  base.scale * splitWeight,   base.projNum, base.penetration,  base.splitNum -1 , -1f);
            proj.GetComponent<Projectile>().Action();
        }
    }

}
