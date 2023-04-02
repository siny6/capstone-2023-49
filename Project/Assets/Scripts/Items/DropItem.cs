using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//======================= 부모 클래스 ==========================
// 몬스터를 잡거나, 상자를 부숴 필드에 드랍되어 플레이어가 획득 가능한 아이템에 대한 스크립트
// 플레이어와의 거리를 계산하여, 범위안에 들어오면 플레이어쪽으로 당겨지며, 플레이어와 충돌 시 획득된다.
//===================================================
public abstract class DropItem : MonoBehaviour
{
    Rigidbody2D rb;
    
    // 속도 관련
    public float speed; // 아이템이 플레이어 쪽으로 이동하는 속도 
    
    public bool captured = false;   // 

    // 경험치가 플레이어의 획득 반경 안에 있는지
    public bool inRange
    {
        get
        {
            float dist = (GameManager.gm.player.transform.position - transform.position).sqrMagnitude;
            float pickupRange = GameManager.gm.player.GetComponent<Player>().Item_Range;

            if (dist <= pickupRange*pickupRange) // 25는 획득범위의 제곱 - 나중에 수정해야함. 
            {
                return true;
            }
            return false;

        }
    }

    //==============================================
    // 아이템 습득 효과 ( 순수가상함수 ) - 플레이어와 충돌시 발동
    //==============================================
    public abstract void PickupEffect();


    //==============================================
    // start - 기본 변수 초기화
    //==============================================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = 3;
    }

    //==============================================
    // 업데이트 -  
    //      1) 플레이어가 해당 아이템에 근접했는지 판별하여 '캡처'
    //      2) '캡처'된 아이템이 플레이어 쪽으로 이동
    //==============================================
    void FixedUpdate()
    {
        // 아이템이 범위 안인지
        if(inRange)
            captured = true;

        // 캡처시 플레이어쪽으로 이동
        if(captured)
        {
            // 방향구하기
            Vector3 dir = (GameManager.gm.player.transform.position - transform.position).normalized;
            
            rb.velocity = dir * speed;

            speed += 10 * Time.fixedDeltaTime;  // 아이템 속도가 점점 빨라짐
        }
    }
    
    //==============================================
    // 충돌 판정
    //==============================================
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌시 (플레이어가 아이템 획득시 )
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PickupEffect(); // 아이템 습득 효과 발동

            Destroy(gameObject);
        }
    }
}
