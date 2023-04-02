using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region 기초스텟
    public int Atk; // 공격력
    public int Hp;  // 체력
    public int Max_Hp; // 최대체력
    public int Exp; // 레벨업 필요 경험치
    public int Cur_Exp; // 현제 경험치
    public float Range; // 공격 사거리
    public float Item_Range; // 아이템 획득 거리 
    public float Speed; // 이동속도
    public float Attack_Speed; // 공격속도

    public int projNum;
    public int splitNum;


    public int Luk; // 행운
    public int[,] Luk_Table = new int[,] { { 98, 1, 1 }, { 90, 9, 1 }, { 80, 18, 2 }, { 70, 25, 5 }, { 50, 25, 25 } };
    #endregion
    public Vector2 inputVector;
    public GameObject LevelUpManager;
    Rigidbody2D rb;
    public bool alive = true;

 
    // 애니메이션을 위한 스프라이트 렌더러 ****************************
    SpriteRenderer spriter;
    Animator animator;

    
    
    //=================================== 투사체수 =======================================
    public Slider slider_projNum; 
    public Text text_projNum;


    public void SetProjNum()
    {
        projNum = (int)slider_projNum.value;
        text_projNum.text = projNum.ToString();

        GetComponent<PlayerWeapon>().InitWeaponStatus();
    }


//=================================== 분열 레벨 =======================================
    public Slider slider_splitNum;
    public Text text_splitNum;

    public void SetSplitNum()
    {
        splitNum = (int)slider_splitNum.value;
        text_splitNum.text = splitNum.ToString();

        GetComponent<PlayerWeapon>().InitWeaponStatus();
    }




    // Start is called before the first frame update
    void Start()
    {
        #region 스텟 초기화
        Atk = 5;
        Max_Hp = 100;
        Hp = 30;
        Exp = 10;
        Cur_Exp = 0;
        Range = 3;
        Item_Range = 3;
        Speed = 5f;
        Attack_Speed = 1;
        Luk = 0;

        rb=GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); // 렌더러 초기화 ****************************
        animator = GetComponent<Animator>();
        

        #endregion
        LevelUpManager = GameObject.Find("LevelupManager");
    }


    void FixedUpdate()
    {
        #region 이동

        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("speed", inputVector.magnitude);
        // animator.speed = (inputVector.magnitude==0)?1f:1.2f;    //애니메이션 속도 조절 
        animator.speed = 0.7f;
        if(inputVector.x != 0) // 스프라이트 뒤집기(입력에 따라) ****************************
        {
            spriter.flipX = inputVector.x <0;
        }
        


        Vector2 nextVector = inputVector.normalized * Speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVector);

        #endregion
    }

    // Update is called once per frame
    void Update()
    { 
        #region 레벨업
        if(Cur_Exp >= Exp)
        {
            Cur_Exp = Cur_Exp - Exp;
            Exp = (int)(Exp * 1.2f);
            LevelUpManager.GetComponent<LevelUpManager>().LevelUp();

        }
        #endregion
        #region 죽음
        if(Hp <= 0 && alive == true)
        {
            Hp = 0;
            alive = false;
            GameObject.Find("StageInfoManager").GetComponent<StageInfoManager>().FinishStage(alive);
        }
        #endregion
    }
}
