using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_testGenesis : Weapon
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
        base.weaponName = "테스트용 제네시스  (분열 구현 필요)";
        base.id_weapon = 103;


        // base.isTargetingWeapon = true;

        base.isSingleTarget = false;
        
        base.range = 5;

        base.damage = 1 + GameManager.gm.player.GetComponent<Player>().Atk; 
        base.penetration = -99;
        base.attackSpeed = 0.5f  + GameManager.gm.player.GetComponent<Player>().Attack_Speed ;


        base.scale = 1f;

        base.splitNum = 0 + GameManager.gm.player.GetComponent<Player>().splitNum;

        base.projNum =  1 + GameManager.gm.player.GetComponent<Player>().projNum;
        base.projSpeed = 0;

        delay = 1f;


    }

    // =========== 오버라이드 =============
    // 개별 공격 함수 : 우선 공격 목표지점을 생성하고, 일정 시간후 해당 지점에 광선을 쏜다. 
    // ===================================
    public override void Attack_custom()
    {        
        StartCoroutine(Coroutine_Fire());
    } 


    // ==================================
    // 제네시스 공격 코루틴 - 타겟들에 대해 타겟지점 생성
    // ===================================
    IEnumerator Coroutine_Fire()
    {
        
        // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        for (int i = list_targets.Count-1; i>=0;i--)
        {
            Transform target = list_targets[i];
            
            StartCoroutine(Fire(target));
            
            yield return new WaitForSeconds(0.05f);
        }
    }

    // ================================
    // 광선 지연 생성을 위한 코루틴
    // ===================================
    IEnumerator Fire(Transform target)
    {
        
        Vector3 pos_target = target.position; //이거 안하면 광선이 목표지점과 다르게 생성될 수 있음 
        
        // 타겟 생성
        GameObject parent = Instantiate(prefab_parent, pos_target, Quaternion.identity );
        Destroy(parent, 1.1f);

        yield return new WaitForSeconds(1f);
        
        // 지연시간 후 proj 생성
        GameObject proj =  Instantiate(prefab_proj, pos_target, Quaternion.identity );
        proj.GetComponent<Projectile>().SetUp(base.damage, base.projSpeed, base.scale,  base.projNum, base.penetration,  base.splitNum , 1f);
        proj.GetComponent<Projectile>().Action();
        proj.GetComponent<Projectile>().SetTarget(parent.transform);

    }


    
    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
    }
}
