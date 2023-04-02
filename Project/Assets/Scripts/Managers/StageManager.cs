using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager sm;


    public int stageNum;

    public GameObject ground;

    public GameObject enemies;      // 적들의 부모 오브젝트 - 제거할때 참조하기 위함
    public GameObject items;        // 아이템들의 부모 오브젝트 - 제거할때 참조하기 위함



    //=====================================
    // 스테이지 정리 - 스테이지 전환시 플레이어를 제외한 오브젝트를 전부 지운다.  
    // 아이템, 적, 효과, 맵 다 지우기 
    //====================================
    public void ClearStage()
    {
        // 적 오브젝트 파괴 - 맨 마지막 자식부터 찾아서 없애기
        int enemiesNum = enemies.transform.childCount;
        for (int i=enemiesNum-1;i>=0; i--)
        {
            // 다 죽여버림 - 아이템은 생성되지 않게

            
            //일단 사라지게
            enemies.transform.GetChild(i).GetComponent<Enemy>().Disappear();
            // Destroy(enemies.transform.GetChild(i).gameObject);
        }
        // 아이템 오브젝트 파괴 - 맨 뒤부터
        int itemsNum = items.transform.childCount;
        for (int i=itemsNum-1; i>=0;i--)
        {
            // 다 없애버림 - 플레이어가 살아있으면 플레이어가 먹을 수 잇게함. 플레이어가 죽었으면 그냥 삭제 
            Destroy(items.transform.GetChild(i).gameObject);
        }
    }


    //=====================================
    // 스테이지 세팅 - 이번 스테이지의 보스, 테마를 설정한다.
    // 
    //====================================
    public void SetStage()
    {

    }

    //=====================================
    // 스테이지 생성 - 세팅한 결과대로 스테이지 관련 오브젝트들을 생성한다.  
    // 맵이미지, 필요 오브젝트 생성 
    //====================================
    public void GenerateStage()
    {
        // ground.SetActive(true);
    }
    
    
    
    //==============================================================
    void Awake()
    {
        sm = this;
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
