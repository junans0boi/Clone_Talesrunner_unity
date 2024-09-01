using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_OctopusFather : MonoBehaviour
{
    //일정시간이 지나면 한번씩 3초 동안 앞으로 이동하고 싶다.
    //필요속성 : 현재 시간, 대기시간, 현재위치, 이동 속도

    
    //현재 시간
    float curTime = 0;

    //대기 시간
    public float waitTime = 0.5f;

    //현재 나의 위치
    Vector3 dir = new Vector3(0, 10, 15);

    //이동속도
    public float speed;

    public Animator anim;


    void Start()
    {
        dir = transform.position;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if(curTime > waitTime)
        {
            anim.SetTrigger("Start");
            transform.position += Vector3.forward * speed * Time.deltaTime;
          /*  if (curTime > 0.6f )
            {
                curTime = 0;
            }*/
        }
    }
}