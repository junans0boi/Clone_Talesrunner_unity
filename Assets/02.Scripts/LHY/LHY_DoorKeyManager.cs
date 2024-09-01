using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_DoorKeyManager : MonoBehaviour
{
    //최대 열쇠 발판개수
    public int maxCountKey;

    [SerializeField]
    public int countKey;

    //public LHY_Key[] keyss;


    //전부 눌리면 움직이는 발판들
    public Animator[] keys;


    //움직일 문
    public Animator Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        print(countKey + "눌린 키");
        
        
        if (countKey >= maxCountKey)
        {
            Door.Play("Open");
            for (int i = 0; i < maxCountKey; i++)
            {
                print(keys[i] + "키 번호");
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
