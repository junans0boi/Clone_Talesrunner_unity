using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [Header("Angly3th SoundTrack")]
    public AudioClip[] Angly3thStart;
    public AudioClip[] Angly3thEnd;
    public AudioClip[] Angly3thFailed;
    public AudioClip[] Angly3thGoal;
    [Header("Angly3th SoundTrack")]
    public GameObject player;
    public PlayerMove PM;

    public AudioSource AS_Angly3th;
    public AudioClip Bgm;
    public AudioSource AS;
    public GoalCount goalCount;
    public GameSoundManager GSM;


    void Start()
    {
        AS.clip = Bgm;      // PlayerSound의 FallDown 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
        AS.Play();
        AS_Angly3th = GetComponent<AudioSource>();
    }
}
