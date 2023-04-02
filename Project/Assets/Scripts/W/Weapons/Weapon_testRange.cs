using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=================== 자식클래스 ========================
// 테스트무기 - 원거리 : 가장 가까운 적에게 총알을 발사하는 원거리 무기
//======================================================
public class Weapon_testRange : Weapon
{
    public GameObject prefab_proj;
    
    // =========== 오버라이드 =============
    // 초기 설정 
    // ===================================
    public override void InitWeaponStatus()
    {
        base.weaponName = "테스트용 원거리 무기";
        base.id_weapon = 100;

        // base.isTargetingWeapon = true;
        
        base.range = 10;

        base.damage = 2 + GameManager.gm.player.GetComponent<Player>().Atk;
        
        base.attackSpeed = 1 + GameManager.gm.player.GetComponent<Player>().Attack_Speed;

        base.scale = 1f;
        base.penetration = 1;

        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;

        base.projNum =  1 + GameManager.gm.player.GetComponent<Player>().projNum; //기본투사체 1개
    }

    // =========== 오버라이드 =============
    // 개별 공격 함수
    // ===================================
    public override void Attack_custom()
    {        
        // 총알 생성
        StartCoroutine(Fire());
    } 

    // ==================================
    // 총알 생성 코루틴 - 투사체수만큼 총알생성 (정확히 일직선으로 나가지 않게 조정)
    // ===================================
    IEnumerator Fire()
    {
        Transform target = list_targets[0];
        for (int i=0;i<base.projNum;i++)
        {

            GameObject proj = Instantiate(prefab_proj,transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , 5f);
            proj.GetComponent<Projectile>().SetDirection(target);
            proj.GetComponent<Projectile>().RotateProj();
            proj.GetComponent<Projectile>().Action();
            // 각도 함수 필요

            yield return new WaitForSeconds(0.05f);
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
