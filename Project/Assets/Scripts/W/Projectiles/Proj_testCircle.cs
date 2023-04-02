using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=================================
// testCircle 의 공격효과 (장판)
//================================
public class Proj_testCircle : Projectile
{
    
    // =========== 오버라이드 =============
    // 아무것도 안함  
    // ===================================
    public override void Action()
    {

    }  
    // =========== 오버라이드 =============
    // 분열    - 장판은 분열 안함.
    // ===================================
    public override void Split()
    {

    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = GameManager.gm.player.transform.position;
    }


}
