using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoalCount : MonoBehaviourPun
{
    public bool goal;
    public int goalNum;
    public GameSoundManager GSM;
    void Start()
    {
        goalNum = 0;
        GSM = GameObject.Find("SoundManager").GetComponent<GameSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            goalNum++;
            if (goalNum == 1)
            {
                
                GSM.AS_Angly3th.clip = GSM.Angly3thEnd[Random.Range(0, 5)]; // Player가 Falldown상태   일때 Angly3thStart 사운드 출력
                GSM.AS_Angly3th.Play();
            }
            
        }
    }

}