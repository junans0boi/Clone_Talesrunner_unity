using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRange : MonoBehaviour
{
    public GameObject Player;
    public PlayerMove PM;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PM = Player.GetComponent<PlayerMove>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMove>())
        {
            PM.isCanDash = true;
            print("Can Dash");
        } 
        
        
    }
    
}
