using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParent : MonoBehaviour
{
    public bool isTracing = false;
    public Transform target;
    public Vector3 pos_target;

    //============================================
    // SetTarget - target 이 null인경우 pos를 따라다님(고정됨), target이 주어지면 target을 따라다님
    //===========================================
    public void SetTarget(Transform target, Vector3 pos)
    {
        isTracing = true;

        this.target = target;
        pos_target = pos;
    }


    //=======================================
    // ProjectileParent 회전시키기    : 현재 방향에서 주어진 각도만큼회전
    //============================================
    public void RotateProj(float angle) // 회전각이 직접 주어짐 
    {
        transform.Rotate(new Vector3(0,0,1) * angle);
    }

    //============================================
    // ProjectileParent 는 pos_target의 위치를 따라다닌다. 
    //===========================================
    void FixedUpdate()
    {
        if (!isTracing)
        {
            return;
        }

        if (target != null)
        {
            pos_target = target.position;
        }
        transform.position = pos_target;
    }
}
