using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//===================== 부모의 부모 클래스 ========================
// 무기 클래스의 최상단에 위치하는 클래스 : 필수적인 무기 정보를 관리  (이름, 데미지 등 )
//===============================================================
public abstract class Weapon : MonoBehaviour
{
    //====================================================
    // 변수
    //====================================================
    // 기본 능력치
    public string weaponName = "";         // 무기 이름
    public int id_weapon;
    public bool isSingleTarget = true;
    // public bool isTargetingWeapon;        // 타겟팅 무기 여부 
    public float damage;        // 공격력
    public float range;     // 사거리
    public float scale = 1f;
    public int penetration;     // 관통횟수
    public int projNum = 1;     // 투사체 수 (기본)
    public float projSpeed = 10;    // 투사체(이펙트) 속도

    public int splitNum = 0;
    public float lifeTime;
    public float attackSpeed;   // 공격속도(초당 공격속도)
    public float lastAttackTime;
    public bool notAvailable = false;

    public bool canAttack       // 공격 가능 상태
    {
        get
        {
            if (notAvailable)
            {
                return false;
            }
            // 공격 간격 과 상태이상(cc기 ) 판별
            float attackDelay = 1/attackSpeed;
            if (lastAttackTime + attackDelay <= GameManager.gm.gameTime)
            {
                return true;
            }
            return false;
        }
    }
    
    // 타겟 관련
    LayerMask targetLayer = 1<<11;           // 적 레이어
    public RaycastHit2D[] targets;          // 사거리 내의 적 표시

    public Transform target;                // 타겟의 위치
    public List<Transform> list_targets;    // 타겟의 위치 정보 리스트 ( 단일 타겟의 경우 1개, 아닐경우 여러개 (투사체 수의 영향을 받음))
    public bool hasTarget                   // 타겟이 있는지, 그리고 그 타겟이 살아있는지 
    {
        get
        {
            int requiredTargetNum = (isSingleTarget)? 1:projNum;
            if (list_targets.Count >= requiredTargetNum )
            {
                return true;
            }
            return false;

        }
    }


    //===================================
    // 무기 별 스탯 초기화
    // 공속, 사거리, 공격력 등 
    //===================================
    public abstract void InitWeaponStatus();

    //======================================================================================================================================
    //===================================
    // 무기의 전투 플로우
    //===================================
    public IEnumerator BattleFlow()
    {
        while (true)
        {
            SearchTarget(projNum);
            // CheckTargetList();
            Attack();

            float attackDelay = 1/attackSpeed;
            yield return new WaitForSeconds(attackDelay);  
        }
    }

    //===================================
    // 타겟 탐색 함수 - searchNum == 탐색할 타겟의 개수 ( default = projNum )
    // 타겟은 매 탐색마다 가장 가까운 적들을 찾는다 (단일 타겟 무기일 경우) 
    // 혹은 범위내 투사체 수만큼의 임의의 적을 찾는다. 
    //===================================
    void SearchTarget(int searchNum)
    {
        //
        list_targets.Clear();  // 일단 현재 타겟 리스트 초기화 
        targets = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0, targetLayer);

        // 단일 타겟 무기일 경우 가장 가까운 적을 공격
        if (isSingleTarget)
        {
            Transform result = null;
            float minDiff =987654321;

            foreach(var t in targets)
            {
                Vector3 myPos = transform.position;
                Vector3 targetPos = t.transform.position;

                float currDiff = Vector3.Distance(myPos, targetPos);

                if (currDiff < minDiff)
                {
                    minDiff = currDiff;
                    result = t.transform;
                }
            }
            // 가장 가까운 적을 리스트에 추가 
            if (result !=null)
            {
                list_targets.Add(result);
            }
        }

        // 단일 타겟 무기가 아닐 경우, 범위 안의 탐색수(투사체 수) 만큼 적을 탐색 (랜덤으로) - 같은 적을 두번 추가할 수 있음
        else
        {
            // 타겟리스트가 비었다면( 사거리 내 적이 없다면) 반환
            if (targets.Length ==0)
            {
                return;
            }
            
            for (int i=0;i<searchNum;i++)
            {
                int idx = Random.Range(0,targets.Length);
                list_targets.Add(targets[idx].transform);
            }
        }
        
    }    
    //=======================================
    // 공격_공통
    //=======================================
    public void Attack()
    {
        // 공격가능한 상황일때 공격
        if (canAttack && list_targets.Count !=0 )
        {
            lastAttackTime = GameManager.gm.gameTime;   // 마지막 공격 시간 갱신

            Attack_custom();
        }
    }

    //=======================================
    // 공격_개별 ( 무기 특성 반영 코드)
    //=======================================
    public abstract void Attack_custom();

    //======================================================================================================================================



    //====================================================
    // 무기 교체시 발생하는 효과
    //====================================================
    public abstract void onDestroyWeapon();



    //==================================================================================


    // 생성되면, 
    void Start()
    {
        // 초기화 하고 전투 플로우 시작 ( 타겟 찾고, 공격 )
        InitWeaponStatus();
        StartCoroutine(BattleFlow());
    }

}
