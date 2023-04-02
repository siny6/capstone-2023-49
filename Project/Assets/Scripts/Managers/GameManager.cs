using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpawnManager pool;
	
	//=================================
    // �⺻ ����
    //================================
    public static GameManager gm;
    public GameObject player;

    // ���� ���� ����
    public float gameTime;  // ������ ����� �ð�

    // ���� �Ͻ����� ����
    public bool isPaused = false;
    public GameObject pauseBoard; 
  


    // =========================================
    // ���� �Ͻ�����/���� - ���� �޴� ��� : UI, ������, ��, �÷��̾�, ���� 
    // =========================================
    public void Pause(bool flag)
    {
        isPaused = flag;
        pauseBoard.gameObject.SetActive(flag);
        Time.timeScale = (flag)?0:1;                    // �߿�
    }



    //==========================================
    // �������� ����� ������ �ʱ�ȭ�Ѵ�. - Ÿ�̸� �ʱ�ȭ, ü�� Ǯ�� ��
    //==========================================
    public void InitGame()
    {
        gameTime = 0;       // Ÿ�̸� �ʱ�ȭ
                            // ü�� Ǯ��
                            // �� ���� ������
    }

    
    // =========================================================

    void Awake()
    {
        gm = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pauseBoard = GameObject.Find("Canvas").transform.Find("PauseBoard").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
