using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public Button First;
    public string First_status;
    public int First_Amount;
    public Text First_text;
    public Button Second;
    public int Second_Amount;
    public string Second_status;
    public Text Second_text;
    public Button Third;
    public int Third_Amount;
    public string Third_status;
    public Text Third_text;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    { 
        First.onClick.AddListener(F);
        Second.onClick.AddListener(S);
        Third.onClick.AddListener(T);
    }
    public void F()
    {
        select(First_status, First_Amount);
        gameObject.SetActive(false);
        GameManager.gm.Pause(false);
    }
    public void S()
    {
        select(Second_status, Second_Amount);
        gameObject.SetActive(false);
        GameManager.gm.Pause(false);
    }
    public void T()
    {
        select(Third_status, Third_Amount);         // Second_status, Second_Amount 라 되어 있어서 수정했음  ***********************************************
        gameObject.SetActive(false);
        GameManager.gm.Pause(false);
    }
    void select(string status, int amount)
    {
        switch (status)
        {
            case "공격력":
                Player.GetComponent<Player>().Atk += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();                 // 변경 옵션 무기에 적용되도록  *********************************************************
                break;
            case "공격속도":
                Player.GetComponent<Player>().Attack_Speed += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();
                break;
            case "최대체력":
                Player.GetComponent<Player>().Max_Hp += amount;
                Player.GetComponent<Player>().Hp += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();
                break;
            case "사거리":
                Player.GetComponent<Player>().Range += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();
                break;
            case "이동속도":
                Player.GetComponent<Player>().Speed += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();
                break;
            case "행운":
                Player.GetComponent<Player>().Luk += amount;
                Player.GetComponent<PlayerWeapon>().InitWeaponStatus();
                break;
        }
    }
}
