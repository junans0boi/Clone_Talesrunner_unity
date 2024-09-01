using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_CardSoldierAce : MonoBehaviour
{
    //카드병정 상태머신 
    public enum CardAceState
    {
        Idle,
        Move,
        Attact,
        Die
    }

    public CardAceState ca_state = CardAceState.Idle;

    //카드병정 애니메션관리 애니메이터
    public Animator animSoldierAce;


    public float idleWaitTime = 2f;

    float currtime;

    float currtime2;

    [Header("HP")]
    public int hp = 3;
    public Slider hpbar;

    

    [Header("Move")]
    public float moveTime = 3f;
    public float rotTime = 1f;

    public float moveSpeed = 1f;

  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(ca_state)
        {
            case CardAceState.Idle:
                IdleUpdate();
                break;
            case CardAceState.Move:
                MoveUpdate();
                break;
            case CardAceState.Attact:
                AttactUpdate();
                break;
            case CardAceState.Die:
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
            ca_state = CardAceState.Move;
        }
    }

    private float maxDistance = 3f;
    private Color _rayColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;

        if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, maxDistance))
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

    private void MoveUpdate()
    {

        BoxRay();

        animSoldierAce.SetTrigger("Move");

        //currtime에 현재시간을 대입해 흐르는시간을 동기화한다.
        currtime += Time.deltaTime;

        //만약 현재시간이 moveTime(이동시간)보다 작거나 같다면 
        if(currtime <= moveTime)
        {
            //카드병정은 스스로의 앞방향을 향해 지정된 이동속도로 전진한다.
            transform.position += transform.forward * moveSpeed * Time.deltaTime;            
        }
        //그것이 아니라, 만약 현재시간이 moveTime(이동시간)보다 크다면 
        else if(currtime > moveTime)
        {
            //currtime2에 현재시간을 대입해 흐르는시간을 동기화한다.
            currtime2 += Time.deltaTime;
            //만약 현재시간이 rotTime(회전시간)보다 작거나 같다면
            if(currtime2 <= rotTime)
            {
                //시간이 흐름에 따라(1초동안) y축으로 180도 회전한다.
                transform.Rotate(0, 180 * 1 * Time.deltaTime, 0);
            }
            //그것이 아니라면(현재시간이  rotTime(회전시간)보다 크다면)
            else
            {
                //현재시간 1,2를 모두 초기화 해준다
                currtime = 0;
                currtime2 = 0;
            }
        }      
    }

    private void AttactUpdate()
    {
        animSoldierAce.SetTrigger("Attack");

        BoxRay();
    }

    private void DieUpdate()
    {
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
                ca_state = CardAceState.Attact;
            }
            else
            {
                ca_state = CardAceState.Move;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hp--;
            hpbar.value = hp;
            if(hp < 1)
            {
                ca_state = CardAceState.Die;
                animSoldierAce.SetTrigger("Die");
            }
        }
    }
}