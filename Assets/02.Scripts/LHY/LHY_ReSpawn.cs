using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_ReSpawn : MonoBehaviour
{
    public Transform targetTR_R;

    public Transform targetTR_B;

    //public Transform targetPos;

    public GameObject RePlayer;

    public bool respawn;

    Vector3 dir;

    //public float Distance;

    // Start is called before the first frame update
    void Start()
    {
        respawn = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        //RePlayer = OnTriggerEnter()
        if (respawn == true)
        {
            if(RePlayer.gameObject.layer == 13)
            {
                dir = targetTR_R.position - RePlayer.transform.position;
                RePlayer.transform.position = targetTR_R.position;
            }
            if(RePlayer.gameObject.layer == 14)
            {
                dir = targetTR_B.position - RePlayer.transform.position;
                RePlayer.transform.position = targetTR_B.position;
            }
            float distance = dir.magnitude;
            if (distance < 0.1f)
            {
                respawn = false;
            }
          
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            respawn = true;

            RePlayer = other.gameObject; 
        }
    }

}

