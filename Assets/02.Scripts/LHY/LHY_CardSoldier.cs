using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_CardSoldier : MonoBehaviour
{
    //카드병정 상태머신 
    public enum CardState
    {
        Idle,
        Move,
        Attact,
        Die
    }

    public CardState c_state = CardState.Idle;

    //카드병정 애니메션관리 애니메이터
    public Animator animSoldier;


    public float idleWaitTime = 2f;

    float currtime;

    float currtime2;

    [Header("HP_Boss")]
    public LHY_CardSoldierAce AceHP;
    int chackHP;


    [Header("Move")]
    public float moveTime = 2f;
    public float rotTime = 1f;

    public int rotmax;

    public float moveSpeed = 1f;

  


    // Start is called before the first frame update
    void Start()
    {
        chackHP = AceHP.hp;
    }

    // Update is called once per frame
    void Update()
    {
        chackHP = AceHP.hp;

        if (chackHP < 1)
        {
            //animSoldier.SetTrigger("Die");
            c_state = CardState.Die;         
        }

        switch (c_state)
        {
            case CardState.Idle:
                IdleUpdate();
                break;
            case CardState.Move:
                MoveUpdate();
                break;
            case CardState.Attact:
                AttactUpdate();
                break;
            case CardState.Die:
                DieUpdate();
                break;
        }

    }

    private void IdleUpdate()
    {
        currtime += Time.deltaTime;
        if (currtime >= idleWaitTime)
        {
            currtime = 0;
            c_state = CardState.Move;
        }
    }

    private float maxDistance = 1f;
    private Color _rayColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;

        if(Physics.BoxCast(transform.position,transform.lossyScale/2.0f, transform.forward, out RaycastHit hit, transform.rotation, maxDistance))
        {
            //레이캐스트가 충돌한 지점까지 Ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            //hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            //hit가 감지되지 않았다면 최대 거리만큼 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }


    Quaternion Goalrot = new Quaternion(0f, 1f, 0f, 0f);

    private void MoveUpdate()
    {
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        BoxRay();

        //정해진 위치에 도착하면 y축으로 180도 회전해서 다시 시간만큼 전진한다.
        animSoldier.SetTrigger("Move");

        currtime += Time.deltaTime;
        if(currtime <= moveTime)
        {
            //카드병정은 일정 시간동안 지정된 이동속도로 스스로의 앞방향으로 전진한다.
            transform.position += transform.forward * moveSpeed * Time.deltaTime;            
        }
        else if(currtime > moveTime)
        {

            currtime2 += Time.deltaTime;
            if(currtime2 <= rotTime)
            {
                //transform.localRotation = Quaternion.Euler(0, 180, 0);
                //transform.localRotation = Quaternion.Euler(0, 180, 0);
                //transform.localEulerAngles = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), 3 * Time.deltaTime);
               /* transform.Rotate(new Vector3(0, rotmax, 0) * Time.deltaTime);
                if (transform.rotation.y >= 180)
                {
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    currtime = 0;
                }*/

                transform.Rotate(Vector3.up, rotmax * Time.deltaTime, Space.World);
                /*  else if(transform.rotation.y == -moveRot.y)* Time.deltaTime
                  {
                      transform.localRotation = Quaternion.Lerp(moveRot, setRot, 5f);
                  }*/
            }
            else
            {
                //if(transform.localRotation 
                currtime = 0;
                currtime2 = 0;
            }
        }



    }

    private void AttactUpdate()
    {       
        animSoldier.SetTrigger("Attack");

        BoxRay();
    }

    private void DieUpdate()
    {

        animSoldier.Play("Die");
        Destroy(gameObject, 3);
    }

    private void BoxRay()
    {
        Gizmos.color = _rayColor;

        //카드병정의 앞방향으로 박스 형태의 레이를 생성
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, maxDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                c_state = CardState.Attact;
            }
            else
            {
                c_state = CardState.Move;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            c_state = CardState.Die;
        }
    }
}
