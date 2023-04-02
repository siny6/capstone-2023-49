using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//==========================================
// HUD (Head Up Display) 
// 체력, 경험치, 시간 등 게임 진행 정보를 화면에 보여준다. 
//==========================================
public class HUD : MonoBehaviour
{
    // HUD 요소들
    Text text_gameTime; // 진행 경과 시간 UI
    Slider  slider_hp;  // HP바
    Slider  slider_exp; // exp바

    //=========================================================================
    // Start - 초기화
    void Start()
    {
        text_gameTime = GameObject.Find("Text_gameTime").GetComponent<Text>();
        slider_hp = GameObject.Find("Slider_hp").GetComponent<Slider>();
        slider_exp = GameObject.Find("Slider_exp").GetComponent<Slider>();
    }

    // UI 업데이트
    void LateUpdate()
    {
        // 시간 업데이트
        float gameTime_raw = GameManager.gm.gameTime;
        int gameTime_minutes = (int)gameTime_raw/60;
        int gameTime_seconds = (int)gameTime_raw%60;
        
        text_gameTime.text = string.Format("{0}:{1}",gameTime_minutes,gameTime_seconds);

        // hp바 업데이트

        // exp바 업데이트
    }
}
