using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;            //텍스트매쉬프로 


//============================================
// 개체 (적, 플레이어)의 디버프 효과 등을 컨트롤함 : 
// - 데미지 텍스트 표시 etc
//============================================
public class EntityEffectController : MonoBehaviour
{
    //===========================================
    public static EntityEffectController eec;       

    public Canvas canvas_entity;
    public GameObject prefab_dmgTxt_enemy;
    public GameObject prefab_dmgTxt_player;

    //==========================================

    //========================================
    // 데미지 생성 - 플레이어나 적이 피해를 입으면 입은 피해량을 화면에 표시한다. 
    //========================================
    public void CreateDamageText(GameObject target, float hitDamage)
    {        
        
        GameObject dmgTxt;
        if (target.GetComponent<Player>() != null)
        {
            dmgTxt = Instantiate(prefab_dmgTxt_player);
            dmgTxt.GetComponent<DamageText_player>().SetTargetPos(target.transform.position + Vector3.up*2);
        }
        else
        {
            dmgTxt = Instantiate(prefab_dmgTxt_enemy);
            dmgTxt.GetComponent<DamageText>().SetTargetPos(target.transform.position );
        } 
        
        dmgTxt.GetComponent<TextMeshPro>().text = hitDamage.ToString();
    }
    
    //===========================================
    void Awake()
    {
        eec= this;
    }


}
