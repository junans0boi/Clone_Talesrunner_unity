using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_GreenCube : MonoBehaviour
{

    public GameObject[] GreenFactorys;

    public int A;
    // Start is called before the first frame update

    private void Awake()
    {
        A = Random.Range(0, 3);
        GreenFactorys[0].gameObject.SetActive(false);
        GreenFactorys[1].gameObject.SetActive(false);
        GreenFactorys[2].gameObject.SetActive(false);
    }
    void Start()
    {
       // A = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /* private void OnCollisionEnter(Collision collision)
     {
         print(collision.gameObject.name);
         if(collision.gameObject.CompareTag("Player"))
         {
             switch (Random.Range(0, 2))
             {
                 case 0:
                     GreenFactorys[0].gameObject.SetActive(true);
                     break;
                 case 1:
                     GreenFactorys[1].gameObject.SetActive(true);
                     break;
                 case 2:
                     GreenFactorys[2].gameObject.SetActive(true);
                     break;
             }
         }     
     }*/

    public void CreatGreen()
    {
        switch (A)
        {
            case 0:
                GreenFactorys[0].gameObject.SetActive(true);
                break;
            case 1:
                GreenFactorys[1].gameObject.SetActive(true);
                break;
            case 2:
                GreenFactorys[2].gameObject.SetActive(true);
                break;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            CreatGreen();
        }
    }
}
