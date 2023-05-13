﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_000_Normal_itemBox : Enemy
{
    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "000";
    }
    
    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    public override void InitEnemyStatusCustom()
    {     
        hpFull =10;

        damage = 0;

        speed = 0;      //움직이지않음
        attackSpeed = 0;    //공격하지 않음


        //
        itemProb = 0f;      //아이템 박스는 죽을 때 다른 적과 달리 확률적으로 회복아이템을 생성하지 않ㅇ므 .
        manaValue = 6f;
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
    public override void DieCustom()
    {
        // lucky mana
        int num = Random.Range(0,100); // 확률
        // lucky 
        DropItem defaultItem;
        DropItem luckyItem;


        // default dropItem 
        if (num <50)    // big mana
        {
            defaultItem = ItemPoolManager.ipm.SpawnItem("000", manaValue * 3, myTransform.position);
        }
        else            // ruby - heal Item
        {
            defaultItem = ItemPoolManager.ipm.SpawnItem("001", Player.Instance.Hp_item_up, myTransform.position);  
        }
        
        
        // 추가로 드랍할 아이템 선택
        num = Random.Range(0,100); // 확률

        // luckyItem = ItemPoolManager.ipm.SpawnItem("005", manaValue * 3, myTransform.position);
        luckyItem = ItemPoolManager.ipm.SpawnItem("002", manaValue * 3, myTransform.position);
        // luckyItem = ItemPoolManager.ipm.SpawnItem("003", manaValue * 3, myTransform.position);
        luckyItem = ItemPoolManager.ipm.SpawnItem("004", manaValue * 3, myTransform.position);
        luckyItem = ItemPoolManager.ipm.SpawnItem("005", manaValue * 3, myTransform.position);
        
        // // 20 % 확률로 럭키 아이템 생성 
        // if (num <= 5)  // sapphire -  magnet
        // {   
        //     luckyItem = ItemPoolManager.ipm.SpawnItem("002", manaValue, myTransform.position); 
        // }
        // else if (num <= 10) // opal - strengthen
        // {
        //     luckyItem = ItemPoolManager.ipm.SpawnItem("003", manaValue, myTransform.position);
        // }
        // else if (num <= 15 )    // topaz- explosion
        // {
        //     luckyItem = ItemPoolManager.ipm.SpawnItem("004", manaValue * 3, myTransform.position);
        // }
        // else if (num <= 20)    // amethyst - paralysis
        // {
        //     luckyItem = ItemPoolManager.ipm.SpawnItem("005", manaValue * 3, myTransform.position);
        // }
    }


}
