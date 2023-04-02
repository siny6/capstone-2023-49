using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_itemBox : Enemy
{
    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    public override void InitEnemyStatus()
    {
        hp = 10;

        damage = 0;

        speed = 0;      //움직이지않음
        attackSpeed = 0;    //공격하지 않음
    }

    // =========== 오버라이드 =============
    // 상자는 공격하지 않음
    // ===================================
    protected override void AttackCustom()
    {
        
    }

    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    public override void MoveCustom()
    {

    }


    // =========== 오버라이드 =============
    // 죽으면 아이템 드랍 
    // ===================================
    public override void DieCustom(GameObject obj)
    {
       // 아이템 리스트[0] == 경험치 : 상자 부숴도 경험치 줌

       GameObject exp = Instantiate(base.item[0], GameObject.Find("Items").transform);
       exp.transform.position = transform.position - Vector3.left*0.5f;


       // 추가로 드랍할 아이템 선택
       GameObject dropItem = null;
       
       int num = Random.Range(1,100); // 확률
       int itemNum = 0;

       // 아이템 리스트[1] == 체력회복 70%
       if (num <= 70)
       {   
            itemNum = 1;
       }
       // 아이템 리스트[2] == 자석 30%
       else
       {
            itemNum = 2;
       }
       dropItem = Instantiate(base.item[itemNum], GameObject.Find("Items").transform);
       dropItem.transform.position = transform.position;

        //    Destroy(gameObject);

        // 이건 긁어옴
        obj.SetActive(false);
        GetComponent<Collider2D>().enabled = true;
        base.hp = 15;               
        base.DropItem();
    }
}
