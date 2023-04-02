using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//====================================
// 플레이어가 장착하고 있는 무기에 대한 정보
//====================================
public class PlayerWeapon : MonoBehaviour
{
    // 무기 장착 위치
    public GameObject[] h = new GameObject[2];   
    
    // 현재 들고 있는 무기 
    public GameObject[] currWeapon = new GameObject[2];


    //=======================================================================
    //==========================
    // 무기 교체 : 현재 무기를 전달받은 무기 정보로 교체한다.
    // handNum - 무기 위치 정보, weapon - 무기정보
    //==========================
    public void changeWeapon(int handNum, GameObject weapon)
    {
        // 기존 무기 파괴 효과 호출
        currWeapon[handNum].GetComponent<Weapon>().onDestroyWeapon();
        // 기존 무기 제거
        Destroy(currWeapon[handNum]); 
        
        // 무기 장착 
        GameObject newWeapon = Instantiate(weapon, h[handNum].transform);
        newWeapon.transform.position = h[handNum].transform.position;

        // 무기 리스트 갱신
        currWeapon[handNum] = newWeapon;
    }

    //==========================
    // 스탯 업데이트 시 보유 무기 능력치 초기화 작업 (변한 능력치 반영)
    //==========================
    public void InitWeaponStatus()
    {
        foreach(var i in currWeapon)
        {
            i.GetComponent<Weapon>().InitWeaponStatus();
        }
    }

    //=========================================================================    
    
    // Start is called before the first frame update
    void Start()
    {
        currWeapon[0] = h[0].transform.GetChild(0).gameObject;
        currWeapon[1] = h[1].transform.GetChild(0).gameObject;
        InitWeaponStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
