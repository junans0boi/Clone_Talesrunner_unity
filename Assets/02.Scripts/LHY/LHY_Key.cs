using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Key : MonoBehaviour
{
    //충돌되었을 떄 켜지는 키
    public GameObject sellectKey;

    //충돌 할 때 올라가는 카운트
    public int count;

    public LHY_DoorKeyManager keySet;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

  /*  public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(count == 0)
            {
                count += 1;
                print(count + "키 감지");
                sellectKey.SetActive(true);
            }
        }
    }*/

    public void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Player"))
        {
            if (count <= 0)
            {
                keySet.countKey++;
                count += 1;
                print(count + "키 감지");
                sellectKey.SetActive(true);
            }
        }
    }
}
