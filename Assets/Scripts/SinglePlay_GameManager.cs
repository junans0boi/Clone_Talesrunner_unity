using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SinglePlay_GameManager : MonoBehaviour
{
    public static SinglePlay_GameManager instance;
    // spawn위치를 담을 변수
    public SinglePlay_GameSoundManager GSM;

    public GameObject Char_Lerf;
    public GameObject Char_Kai;
    public GameObject Char_Bada;
    public GameObject Char_Bera;


    public GameObject goalPopup;

    public bool EnterGamebool;
    public bool Gameing;
    public Text ScoreTxt;
    public GameObject Canvas;

    public GameObject Canvas2;
    public GameObject FailedPopup;

    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        EnterGamebool = false;
        Invoke("EnterGame", 13.7f);

        Canvas.SetActive(false);
        Canvas2.SetActive(false);

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
        
    }


    void Update()
    {
        
    }
    
    
    public void GoBack()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void EnterGame()
    {
        
        EnterGamebool = true;
        Gameing = true;
        Canvas.SetActive(true);
        Canvas2.SetActive(true);
        

    }
    


}