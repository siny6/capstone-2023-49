using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//=================== 자식클래스 ========================
// 테스트무기 - 근접 : 가장 가까운 적을 직접 타격하는 근접무기
//======================================================
public class Weapon_testMelee : Weapon
{
    public GameObject prefab_parent;
    public GameObject prefab_proj;
    
    Vector3 attackDir;  // 공격 방향
    
    Transform hand;
    
    
    // =========== 오버라이드 =============
    // 초기 설정 
    // ===================================
    public override void InitWeaponStatus()
    {
        base.weaponName = "테스트용 근접 무기";
        base.id_weapon = 101;

        // base.isTargetingWeapon = true;
        base.isSingleTarget = false;
        
        base.range = 4;

        base.damage = 7  + GameManager.gm.player.GetComponent<Player>().Atk; 
        base.penetration = -99;
        base.attackSpeed = 1f  + GameManager.gm.player.GetComponent<Player>().Attack_Speed ;

        base.scale = 1f;


        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;
        
        base.projNum =  1 + GameManager.gm.player.GetComponent<Player>().projNum; // 기본 투사체 1개

        hand = transform.parent;

    }

    // ========= 오버라이드 ==============
    // 개별 공격 함수 - 이펙트 프리팹 생성
    // ===================================
    public override void Attack_custom()
    {   
        StartCoroutine(Slash());
    }


    // ==================================
    // 근접공격 코루틴 - 투사체수만큼 공격 (list_targets에 있는 적들을 순서대로 공격)
    // ===================================
    IEnumerator Slash()
    {
        // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        for (int i = list_targets.Count-1; i>=0;i--)
        {

            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                Transform target = list_targets[i];
                if (target==null)
                {
                    break;
                }
                attackDir = (target.position - GameManager.gm.player.transform.position).normalized;
                
                // 기준생성
                GameObject parent = Instantiate(prefab_parent, GameManager.gm.player.transform.position, Quaternion.FromToRotation(Vector3.left,attackDir));
                parent.GetComponent<ProjectileParent>().SetTarget(GameManager.gm.player.transform, Vector3.zero );

                // 효과생성
                GameObject proj = Instantiate(prefab_proj, parent.transform);
                proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , -1f);
                proj.GetComponent<Projectile>().Action();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    
    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
    }

}
