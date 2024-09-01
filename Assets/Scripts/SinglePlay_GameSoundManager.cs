using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlay_GameSoundManager : MonoBehaviour
{
    [Header("Angly3th SoundTrack")]
    public AudioClip[] Angly3thStart;
    public AudioClip[] Angly3thEnd;
    public AudioClip[] Angly3thFailed;
    public AudioClip[] Angly3thGoal;
    [Header("Angly3th SoundTrack")]
    public GameObject player;
    public SinglePlay_PlayerMove PM;

    public AudioSource AS_Angly3th;
    public AudioClip Bgm;
    public AudioSource AS;

    void Start()
    {
        AS_Angly3th = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        PM = player.GetComponent<SinglePlay_PlayerMove>();
        AS.clip = Bgm;      // PlayerSound의 FallDown 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
        AS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // if (PM.Falldownbool == true)
        // {
        //     AS_Angly3th.clip = Angly3thStart[Random.Range(0, 5)];      // Player가 Falldown상태 일때 Angly3thStart 사운드 출력
        //     AS_Angly3th.Play();
        //     return;
        // }
        // if (PM.Diebool == true)
        // {
        //     GSM.AS_Angly3th.clip = GSM.Angly3thFailed[Random.Range(0, 3)];      // Player가 Falldown상태 일때 Angly3thStart 사운드 출력
        //     AS_Angly3th.Play();
        //     return;
        // }
        // if (PM.Goalbool == true)
        // {
        //     GSM.AS_Angly3th.clip = GSM.Angly3thGoal[Random.Range(0, 1)];      // Player가 Falldown상태 일때 Angly3thStart 사운드 출력
        //     GSM.AS_Angly3th.Play();         
        //     return;
        // }
    }
    
}
