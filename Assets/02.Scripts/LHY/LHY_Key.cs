using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Key : MonoBehaviour
{
    //�浹�Ǿ��� �� ������ Ű
    public GameObject sellectKey;

    //�浹 �� �� �ö󰡴� ī��Ʈ
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
                print(count + "Ű ����");
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
                print(count + "Ű ����");
                sellectKey.SetActive(true);
            }
        }
    }
}
