using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//===========================================
// 자석 : 획득 시 필드 위의 모든 경험치를 획득한다.
//==============================================
public class Magnet : DropItem
{
    //================== 오버라이드 =========================
    // 자석 아이템 획득 효과 - 플레이어와 충돌시 발동
    //==============================================
    public override void PickupEffect()
    {
        Debug.Log("Magnet");

        // 모든 경험치 오브젝트가 플레이어쪽으로 이동하도록 captured를 true로 설정
        GameObject[] exps = GameObject.FindGameObjectsWithTag("exp");

        foreach (var exp in exps)
        {
            exp.GetComponent<Exp>().captured = true;
        }
    }
}
