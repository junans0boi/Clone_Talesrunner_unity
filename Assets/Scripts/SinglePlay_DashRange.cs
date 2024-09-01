using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlay_DashRange : MonoBehaviour
{
    public GameObject Player; // ?????????? ????
    public SinglePlay_PlayerMove PM;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PM = Player.GetComponent<SinglePlay_PlayerMove>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SinglePlay_PlayerMove>())
        {
            PM.isCanDash = true;
            print("Can Dash");
        }


    }

}
