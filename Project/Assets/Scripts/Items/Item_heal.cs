using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_heal : DropItem
{
    //================== 오버라이드 =========================
    // 회복 아이템  획득 효과 - 플레이어와 충돌시 발동
    //==============================================
    public override void PickupEffect()
    {
        if (GameObject.Find("Player").GetComponent<Player>().Hp + 5 > GameObject.Find("Player").GetComponent<Player>().Max_Hp)
            GameObject.Find("Player").GetComponent<Player>().Hp = GameObject.Find("Player").GetComponent<Player>().Max_Hp;
        else
        {
            GameObject.Find("Player").GetComponent<Player>().Hp += 5;
        }
        
    }
}
