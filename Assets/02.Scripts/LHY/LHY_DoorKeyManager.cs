using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_DoorKeyManager : MonoBehaviour
{
    //�ִ� ���� ���ǰ���
    public int maxCountKey;

    [SerializeField]
    public int countKey;

    //public LHY_Key[] keyss;


    //���� ������ �����̴� ���ǵ�
    public Animator[] keys;


    //������ ��
    public Animator Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        print(countKey + "���� Ű");
        
        
        if (countKey >= maxCountKey)
        {
            Door.Play("Open");
            for (int i = 0; i < maxCountKey; i++)
            {
                print(keys[i] + "Ű ��ȣ");
                keys[i].Play("Down");
            }
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            countKey++;
            if(countKey >= maxCountKey)
            {
                Door.Play("Open");
                for(int i = 0; i > maxCountKey; i++)
                {
                    keys[i].Play("Down");
                }
            }
        }

    }*/


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countKey++;
            if (countKey >= maxCountKey)
            {
                Door.Play("Open");
                for (int i = 0; i > maxCountKey; i++)
                {
                    keys[i].Play("Down");
                }
            }
        }
    }*/
}
