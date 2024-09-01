using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
[Header("SoundTrack")]
    public AudioClip[] Angry;
    public AudioClip[] Damage;
    public AudioClip[] Jump;
    public AudioClip[] DoubleJump;
    public AudioClip[] SuperJump;
    public AudioClip[] LowEnergy;
    public AudioClip[] FallDown;
    public AudioClip[] Victory;
    public AudioClip[] Failed;
    public AudioClip[] Lose;
    public AudioClip[] ElectricShock;

    public AudioSource AS_Move;
    public AudioSource AS_State;
    void Start()
    {
        
        AS_Move = GetComponent<AudioSource>();
        AS_State = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
