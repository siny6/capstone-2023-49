using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText_player : MonoBehaviour
{
public Vector3 targetPos;
    public Vector3 dir;

    float speed = 1;

    float lifeTime = 1f;

    
    //====================================
    // 타겟설정
    //====================================
    public void SetTargetPos(Vector3 pos)
    {
        targetPos =  pos;
    }

    //===================================
    //
    //===================================
    void Start()
    {
        dir =  new Vector3(Random.Range(-0.2f,0.2f), 0.5f, 0).normalized;
        
        float newX = Random.Range(-0.5f,0.5f);
        Vector3 offset = new Vector3(newX, 1f, 0);
        transform.position = targetPos + offset; 
        
        Destroy(gameObject,lifeTime);
    }

    //===================================  
    // 데미지 애니메이션 
    //===================================
    void FixedUpdate()
    {       
        // transform.Translate(dir *speed * Time.fixedDeltaTime);
    }
}
