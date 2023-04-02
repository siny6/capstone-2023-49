using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//=================== 자식클래스 ========================
// 테스트무기 - 빙빙 : 플레이어 주위를 빙빙 도는 논타겟 무기 : 
//     - 골드메탈 뱀서만들기 강의 영상 참고 https://www.youtube.com/watch?v=HPJVVcRKwn0&list=PLO-mt5Iu5TeZF8xMHqtT_DhAPKmjF6i3x&index=10 
//======================================================
public class Weapon_testBingbing : Weapon
{
    public GameObject prefab_parent; // 투사체의 중심 (얘가 돌아가는 거) 
    public GameObject prefab_proj;  // 적에 공격을 가하는 투사체


    public GameObject parent;   // 투사체의 중심
    
    // =========== 오버라이드 =============
    // 초기 설정 - 스탯 강화할때마다 호출하면 될듯 
    // ===================================
    public override void InitWeaponStatus()
    {
        base.weaponName = "테스트용 빙빙";
        base.id_weapon = 102;

        // base.isTargetingWeapon = false;
        
        base.range = 10;

        base.damage = 3 + GameManager.gm.player.GetComponent<Player>().Atk; 
        base.penetration = -99;
        base.attackSpeed = 1+ GameManager.gm.player.GetComponent<Player>().Attack_Speed;
        
        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;

        base.projNum = 2 + GameManager.gm.player.GetComponent<Player>().projNum;      // 기본 투사체 2개

        base.projSpeed= 10;


        base.scale = 1f;

        onDestroyWeapon();  // 투사체 같은 게 변경될 때 초기화해줘야함.
    }

    // =========== 오버라이드 =============
    // 개별 공격 함수
    // ===================================
    public override void Attack_custom()
    {
        base.notAvailable = true; // 지속형 무기여서 한번만 생성하면 되기 떄문

        // 회전의 중심 생성
        parent = Instantiate(prefab_parent, GameManager.gm.player.transform.position, Quaternion.identity);
        parent.GetComponent<ProjectileParent>().SetTarget(GameManager.gm.player.transform, Vector3.zero);

        // 그리고 투사체 배치 ( 투사체 수에 따라 배치가 달라짐. 기본 2개) - 함수필요 
        ArrangeProj();
    } 



    // ===================================
    // 회전하는 투사체 배치 - 투사체 수에 따라 투사체 배치
    // ===================================
    public void ArrangeProj()
    {
        float rotationPerUnit = 360/base.projNum;       // 투사체 당 각도
        float currRotation = 0;
        for (int i=0;i<base.projNum;i++)
        {
            GameObject proj = Instantiate(prefab_proj, parent.transform);

            proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currRotation));
            currRotation+=rotationPerUnit;

            proj.transform.localPosition = proj.transform.up * 2;      
            proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , -1f);
            proj.GetComponent<Projectile>().Action();
        }
    }


    //============= 오버라이드 ==================
    // 무기 파괴시 생성된 회전의 중심을 파괴해야함
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
        Destroy(parent);
    }
    
}
