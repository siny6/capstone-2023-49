using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_meteo : Weapon
{
    // 프리팹
    public GameObject prefab_parent;       // 공격 목표지점의 프리팹
    public GameObject prefab_proj;               // 광선의 프리팹

    // 능력치
    float delay = 1f;   
    
    
    // =========== 오버라이드 =============
    // 초기 설정 
    // ===================================
    public override void InitWeaponStatus()
    {
        base.weaponName = "메테오";
        base.id_weapon = 10;


        // base.isTargetingWeapon = true;

        base.isSingleTarget = false;
        
        base.range = 7;

        base.damage = 33 + GameManager.gm.player.GetComponent<Player>().Atk; 
        base.penetration = -99;
        base.attackSpeed = 0.5f + GameManager.gm.player.GetComponent<Player>().Attack_Speed;


        base.scale = 1f;

        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;



        base.projNum =  1 + GameManager.gm.player.GetComponent<Player>().projNum;
        base.projSpeed = 20f;

        delay = 1f;


    }

    // =========== 오버라이드 =============
    // 개별 공격 함수 : 우선 공격 목표지점을 생성하고, 일정 시간후 해당 지점에 운석을 떨어뜨린다.
    // ===================================
    public override void Attack_custom()
    {        
        StartCoroutine(Coroutine_Fire());
    } 


    // ==================================
    // 메테오 공격 코루틴 - 타겟들에 대해 타겟지점 생성
    // ===================================
    IEnumerator Coroutine_Fire()
    {
        
        // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        for (int i = list_targets.Count-1; i>=0;i--)
        {
            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                Transform target = list_targets[i];
                Fire(target);
                
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    // ================================
    // 메테오 떨구기
    // ===================================
    void Fire(Transform target)
    {    
        Vector3 pos_target = target.position; //이거 안하면 광선이 목표지점과 다르게 생성될 수 있음 
        
        // 타겟 생성
        GameObject proj = Instantiate(prefab_proj, pos_target, Quaternion.identity );
        proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , -1f);
        proj.GetComponent<Projectile>().Action();
    }


    
    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
    }
}
