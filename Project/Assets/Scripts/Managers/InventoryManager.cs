using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//=================== 테스트 환경 용 =====================
// 인벤토리 매니저 : 구현한 무기 목록 UI를 보여주고 무기를 선택하여 장착할 수 있게 해줌.
//===============================================
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager im;

    // 인벤토리 UI
    public GameObject inventory;
    
    // 무기 이름
    public Text text_weaponName_h0;
    public Text text_weaponName_h1;

    // 버튼 상태
    public Toggle btn_h0;
    public Toggle btn_h1;

    // 무기리스트 
    public GameObject contentBox_weapons;      // UI : 버튼들이 나열될 UI 

    public Dictionary<int, GameObject> dic_weapons = new Dictionary<int, GameObject>();     // <식변번호, 게임오브젝트>의 자료구조 

    //
    public GameObject prefab_btn_weapon;

    
    
    //=====================함수=============================================
    //======================================
    // 리스트 세팅 : 사용 가능한 무기 목록을 설정한다.
    //======================================
    public void  InitWeaponList()
    {
        // 리소스 파일에서 무기 오브젝트 정보를 가져온다.
        GameObject[] list_weapons = Resources.LoadAll<GameObject>("Prefabs/W/Weapons");

        // 일단 가져온 무기 오브젝트 정보들을 무기목록(사전)에 등록
        for(int i = 0;i<list_weapons.Length;i++)
        {
            GameObject obj_weapon = list_weapons[i];
            Weapon weapon = obj_weapon.GetComponent<Weapon>();  // 무기오브젝트의 무기 스크립트

            weapon.InitWeaponStatus();         // 무기 정보 초기화 - 무기 번호, 이름 얻으려고 초기화 했음. 

            dic_weapons.Add ( weapon.id_weapon, weapon.gameObject );   // 무기 목록에 추가             
        }
        
        // id 0번부터 차례대로 UI에 생성 ( 200은 그냥 임의의 수 )
        int num = 0;            // id와 순서가 일치하지 않기 때문에 추가함.
        for (int i=0;i<200;i++)
        {
            // 키가 포함되지 않으면 다음 키 검색
            if (!dic_weapons.ContainsKey(i))
            {
                continue;
            }

            GameObject newBtn = Instantiate(prefab_btn_weapon, contentBox_weapons.transform);   //버튼ui 인스턴스 생성 

            Vector3 newPos = contentBox_weapons.transform.localPosition + new Vector3(5,-5 - num++*30,0);    // 버튼ui를 배치할 좌표 설정 
            newBtn.transform.localPosition = newPos;

            // 버튼 정보 설정
            newBtn.GetComponent<InventoryBtn>().weapon = dic_weapons[i];        // 버튼에 무기정보 할당 - 나중에 버튼 클릭시 해당 무기의 정보가 넘어감
            newBtn.transform.GetComponentInChildren<Text>().text = string.Format("{0}: {1}",i ,dic_weapons[i].GetComponent<Weapon>().weaponName);    // 버튼의 텍스트 설정 - (번호: 이름)
        }
    }

    //======================================
    // 인벤토리에서 장착중인 무기 이름 갱신
    //======================================
    public void UpdateCurrWeaponName()
    {
        text_weaponName_h0.text = "h0: "+ GameManager.gm.player.GetComponent<PlayerWeapon>().currWeapon[0].GetComponent<Weapon>().weaponName;
        text_weaponName_h1.text = "h1: "+ GameManager.gm.player.GetComponent<PlayerWeapon>().currWeapon[1].GetComponent<Weapon>().weaponName;
    }

    //======================================
    // 인벤토리 오픈 : 게임 일시정지 후, 인벤토리 창을 연다
    //======================================
    public void OpenInventory(bool flag)
    {
        if (flag)
        {
            UpdateCurrWeaponName();
        }
        
        GameManager.gm.Pause(flag);
        inventory.SetActive(flag);
    }

    //======================================
    // 무기교체 - 버튼 클릭시 버튼의 InventoryBtn 스크립트에서 호출됨
    //======================================
    public void ChangeWeapon(GameObject weapon)
    {
        int nextNum = (btn_h0.isOn == true)?0:1;
    
        GameManager.gm.player.GetComponent<PlayerWeapon>().changeWeapon(nextNum,weapon);
        UpdateCurrWeaponName();
    }



    
    //=======================================================================
    void Awake()
    {
        im = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        InitWeaponList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
