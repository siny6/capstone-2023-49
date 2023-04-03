using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public Boss_Kill uiBoss_Kill;
    public float check = 100f;


    // 보스 처치 시 Ui 보이기
    void Update()
    {
        if (check == 0f)
        {
            uiBoss_Kill.show();
            gameObject.SetActive(false);
        }
    }
}
