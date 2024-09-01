using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomGameManager : MonoBehaviour
{

    public GameObject Char_Lerf;
    public GameObject Char_Kai;
    public GameObject Char_Bada;
    public GameObject Char_Bera;
    public bool RedTeam;
    public bool BlueTeam;


    void Start()
    {

        if (ChooseCharacter.instance.boolKai == true)
        {
            Char_Lerf.SetActive(false);
            Char_Kai.SetActive(true);
            Char_Bada.SetActive(false);
            Char_Bera.SetActive(false);
        }
        if (ChooseCharacter.instance.boolBada == true)
        {
            Char_Lerf.SetActive(false);
            Char_Kai.SetActive(false);
            Char_Bada.SetActive(true);
            Char_Bera.SetActive(false);
        }
        if (ChooseCharacter.instance.boolLerf == true)
        {
            Char_Lerf.SetActive(true);
            Char_Kai.SetActive(false);
            Char_Bada.SetActive(false);
            Char_Bera.SetActive(false);
        }
        if (ChooseCharacter.instance.boolBera == true)
        {
            Char_Lerf.SetActive(false);
            Char_Kai.SetActive(false);
            Char_Bada.SetActive(false);
            Char_Bera.SetActive(true);
        }
        if (ChooseCharacter.instance.RedTeam == true)
        {
            RedTeam = true;
            
        }
        if (ChooseCharacter.instance.BlueTeam == true)
        {
            BlueTeam = true;
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
