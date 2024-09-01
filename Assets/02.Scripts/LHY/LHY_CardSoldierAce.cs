using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHY_CardSoldierAce : MonoBehaviour
{
    //ī�庴�� ���¸ӽ� 
    public enum CardAceState
    {
        Idle,
        Move,
        Attact,
        Die
    }

    public CardAceState ca_state = CardAceState.Idle;

    //ī�庴�� �ִϸ޼ǰ��� �ִϸ�����
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
            //����ĳ��Ʈ�� �浹�� �������� Ray�� �׷��ش�.
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            //hit�� ������ �ڽ��� �׷��ش�.
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            //hit�� �������� �ʾҴٸ� �ִ� �Ÿ���ŭ ray�� �׷��ش�.
            Gizmos.DrawRay(transform.position, transform.forward * maxDistance);
        }
    }

    private void MoveUpdate()
    {

        BoxRay();

        animSoldierAce.SetTrigger("Move");

        //currtime�� ����ð��� ������ �帣�½ð��� ����ȭ�Ѵ�.
        currtime += Time.deltaTime;

        //���� ����ð��� moveTime(�̵��ð�)���� �۰ų� ���ٸ� 
        if(currtime <= moveTime)
        {
            //ī�庴���� �������� �չ����� ���� ������ �̵��ӵ��� �����Ѵ�.
            transform.position += transform.forward * moveSpeed * Time.deltaTime;            
        }
        //�װ��� �ƴ϶�, ���� ����ð��� moveTime(�̵��ð�)���� ũ�ٸ� 
        else if(currtime > moveTime)
        {
            //currtime2�� ����ð��� ������ �帣�½ð��� ����ȭ�Ѵ�.
            currtime2 += Time.deltaTime;
            //���� ����ð��� rotTime(ȸ���ð�)���� �۰ų� ���ٸ�
            if(currtime2 <= rotTime)
            {
                //�ð��� �帧�� ����(1�ʵ���) y������ 180�� ȸ���Ѵ�.
                transform.Rotate(0, 180 * 1 * Time.deltaTime, 0);
            }
            //�װ��� �ƴ϶��(����ð���  rotTime(ȸ���ð�)���� ũ�ٸ�)
            else
            {
                //����ð� 1,2�� ��� �ʱ�ȭ ���ش�
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

        //ī�庴���� �չ������� �ڽ� ������ ���̸� ����
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