using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//장판형 무기 ( 뱀서 마늘 )
//=============================================
public class Weapon_testCircle : Weapon
{
    public GameObject prefab_proj;

    public GameObject proj;     // 생성된 장판
    
    // =========== 오버라이드 =============
    // 초기 설정 
    // ===================================
    public override void InitWeaponStatus()
    {
        base.weaponName = "테스트용 장판";
        base.id_weapon = 104;

        // base.isTargetingWeapon = false;
        
        base.range = 10;

        base.damage = 0.2f  + GameManager.gm.player.GetComponent<Player>().Atk; 
        base.penetration = -99;


        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;

        base.projNum = 1 + GameManager.gm.player.GetComponent<Player>().projNum;


        base.attackSpeed = 2 + base.projNum/2 + GameManager.gm.player.GetComponent<Player>().Attack_Speed;   // 공속이 투사체수 영향받음

        base.scale = 1f;

        onDestroyWeapon();
        base.list_targets.Clear();  // 그리고 타겟 리스트 초기화
    }

    // =========== 오버라이드 =============
    // 개별 공격 함수 - 플레이이어 위치에 장판 생성( 한번 생성되면 끝) 
    // ===================================
    public override void Attack_custom()
    {
        base.notAvailable = true; // 지속형 무기여서 한번만 생성하면 되기 떄문

        proj = Instantiate(prefab_proj, GameManager.gm.player.transform.position, transform.rotation);
        proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , -1f);
        
        StartCoroutine(Tick());
    }  


    // ===================================
    // testCircle은 투사체 수가 공속에 영향을 줌
    // ===================================
    IEnumerator Tick()
    {
        float attackDelay = 1/base.attackSpeed;
        CircleCollider2D cc = proj.GetComponent<CircleCollider2D>();
        while(true)
        {
            if (cc!=null)
            {
                cc.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);

            if (cc!=null)
            {
                cc.enabled = false;
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }


    //============= 오버라이드 ==================
    // 무기 파괴시 생성된 장판을 파괴해야함.
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
        Destroy(proj);
    }
}
