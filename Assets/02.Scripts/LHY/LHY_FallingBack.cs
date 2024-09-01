using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_FallingBack : MonoBehaviour
{
    public LHY_CardSoldierAce AceHP;
    int chackHP;

    public Animator anim;

    void Start()
    {
        chackHP = AceHP.hp;
    }

    // Update is called once per frame
    void Update()
    {
        chackHP = AceHP.hp;

        if (chackHP < 1)
        {
            anim.Play("Back");
        }
    }

}
