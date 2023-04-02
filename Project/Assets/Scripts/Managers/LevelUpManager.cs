using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelUpManager : MonoBehaviour
{
    public GameObject Popup;
    // Start is called before the first frame update
    void Start()
    {
        Popup.SetActive(false);
    }
    public void LevelUp()
    {
        List<Dictionary<string, object>> data = GameObject.Find("CSVManager").GetComponent<test>().data;
        int[] numList = getList(3, 0, 5); // 랜덤하게 올릴 능력치 결정하기
        int[,] Luk_Table = GameObject.Find("Player").GetComponent<Player>().Luk_Table;
        int Luk = GameObject.Find("Player").GetComponent<Player>().Luk;
        int grade = 0;
        for (int i = 0; i < numList.Length; i++) // 행운 단계별로 몇단계 능력치를 얻을지 설정
        {
            int tmp = Random.Range(1, 101); // 1단계 2단계 3단계 확률이 90 7 3 이라면 1~90은 1단계 91~97은 2단계 98 ~ 100은 3단계로 되도록 난수 추출
            if (tmp < Luk_Table[Luk, 0])
                grade = 0;
            else if (tmp < Luk_Table[Luk,0] + Luk_Table[Luk, 1])
                grade = 1;
            else
                grade = 2;
            // 버튼에 연동시키기 // 
            if(i == 0)
                switch (grade)
            {
                case 0:
                    Popup.GetComponent<UiScript>().First_status = data[numList[i]]["Table"].ToString();
                    Popup.GetComponent<UiScript>().First_Amount = (int)data[numList[i]]["Level1"];
                    Popup.GetComponent<UiScript>().First_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level1"] + " 만큼 증가시킨다";
                    break;
                case 1:
                    Popup.GetComponent<UiScript>().First_status = data[numList[i]]["Table"].ToString();
                    Popup.GetComponent<UiScript>().First_Amount = (int)data[numList[i]]["Level2"];
                    Popup.GetComponent<UiScript>().First_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level2"] + " 만큼 증가시킨다";
                    break;
                default:
                    Popup.GetComponent<UiScript>().First_status = data[numList[i]]["Table"].ToString();
                    Popup.GetComponent<UiScript>().First_Amount = (int)data[numList[i]]["Level3"];
                    Popup.GetComponent<UiScript>().First_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level3"] + " 만큼 증가시킨다";
                    break;
            }
            else if (i == 1)
                switch (grade)
                {
                    case 0:
                        Popup.GetComponent<UiScript>().Second_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Second_Amount = (int)data[numList[i]]["Level1"];
                        Popup.GetComponent<UiScript>().Second_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level1"] + " 만큼 증가시킨다";
                        break;
                    case 1:
                        Popup.GetComponent<UiScript>().Second_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Second_Amount = (int)data[numList[i]]["Level2"];
                        Popup.GetComponent<UiScript>().Second_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level2"] + " 만큼 증가시킨다";
                        break;
                    default:
                        Popup.GetComponent<UiScript>().Second_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Second_Amount = (int)data[numList[i]]["Level3"];
                        Popup.GetComponent<UiScript>().Second_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level3"] + " 만큼 증가시킨다";
                        break;
                }
            else
                switch (grade)
                {
                    case 0:
                        Popup.GetComponent<UiScript>().Third_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Third_Amount = (int)data[numList[i]]["Level1"];
                        Popup.GetComponent<UiScript>().Third_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level1"] + " 만큼 증가시킨다";
                        break;
                    case 1:
                        Popup.GetComponent<UiScript>().Third_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Third_Amount = (int)data[numList[i]]["Level2"];
                        Popup.GetComponent<UiScript>().Third_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level2"] + " 만큼 증가시킨다";
                        break;
                    default:
                        Popup.GetComponent<UiScript>().Third_status = data[numList[i]]["Table"].ToString();
                        Popup.GetComponent<UiScript>().Third_Amount = (int)data[numList[i]]["Level3"];
                        Popup.GetComponent<UiScript>().Third_text.text = data[numList[i]]["Table"].ToString() + " 을(를) " + data[numList[i]]["Level3"] + " 만큼 증가시킨다";
                        break;
                }

        } // 행
        Popup.SetActive(true);
        GameManager.gm.Pause(true);
    }
    // 중복없는 난수 추출 //
    int[] getList(int length, int min, int max)
    {
        int[] chooseset = new int[3];
        int[] ranArr = Enumerable.Range(0, max).ToArray();
        for (int i=0; i<3; i++)
        {
            int rand = Random.Range(i, max);
            chooseset[i] = ranArr[rand];
            int tmp = ranArr[rand];
            ranArr[rand] = ranArr[i];
            ranArr[i] = tmp;
        }
        return chooseset;
    }
}
