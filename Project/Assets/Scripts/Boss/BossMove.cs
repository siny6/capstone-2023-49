using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : Boss
{
    public bool move = false;
    void Start()
    {
        StartCoroutine(MoveDown());
    }

    // 플레이어 쪽으로 이동
    void FixedUpdate() 
    {
        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    
    // 보스 등장 장면
    IEnumerator MoveDown() 
    {
        rigid.velocity = new Vector2(0, -5);
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        move = true;
    }
}
