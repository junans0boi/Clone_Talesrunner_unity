using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_CardSoldier : MonoBehaviour
{
    //ī�庴�� ���¸ӽ� 
    public enum CardState
    {
        Idle,
        Move,
        Attact,
        Die
    }

    public CardState c_state = CardState.Idle;

    //ī�庴�� �ִϸ޼ǰ��� �ִϸ�����
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


    Quaternion Goalrot = new Quaternion(0f, 1f, 0f, 0f);

    private void MoveUpdate()
    {
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);


        BoxRay();

        //������ ��ġ�� �����ϸ� y������ 180�� ȸ���ؼ� �ٽ� �ð���ŭ �����Ѵ�.
        animSoldier.SetTrigger("Move");

        currtime += Time.deltaTime;
        if(currtime <= moveTime)
        {
            //ī�庴���� ���� �ð����� ������ �̵��ӵ��� �������� �չ������� �����Ѵ�.
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

        //ī�庴���� �չ������� �ڽ� ������ ���̸� ����
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
