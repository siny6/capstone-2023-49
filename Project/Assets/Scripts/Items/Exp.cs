using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===========================================
// 경험치 
//==============================================
public class Exp : DropItem
{
    //================== 오버라이드 =========================
    // 경험치  획득 효과 - 플레이어와 충돌시 발동
    //==============================================
    public override void PickupEffect()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.GetComponent<Player>().Cur_Exp += 3;
    }
}
