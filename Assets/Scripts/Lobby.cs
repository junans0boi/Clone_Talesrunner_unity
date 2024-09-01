using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices; //user32.dll ?????????
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [Header("GameMode Select")]
    public bool GameMode;
    public bool PracticeMode;
    public GameObject Sel_GameMode;
    public GameObject UnSel_GameMode;
    public GameObject Sel_PracticeMode;
    public GameObject UnSel_PracticeMode;

    public GameObject Select_Character_Popup;
    public GameObject Select_CreateRoom_Popup;
    public GameObject Multi_Popup;

    [Header("SinglePlay Select")]
    public GameObject Single_Popup;
    public GameObject Sel_Single_Map_Angry_OctopusBtn;
    public GameObject UnSel_Single_Map_Angry_OctopusBtn;
    public GameObject Sel_Single_Map_Cocaine_OctopusBtn;
    public GameObject UnSel_Single_Map_Cocaine_OctopusBtn;
    public Text MapName;
    public GameObject Single_Map_Angry_OctopusImage;
    public GameObject Single_Map_Cocaine_OctopusImage;
    public GameObject Single_Map_Map_Angry_OctopusTxt;
    public GameObject Single_Map_Cocaine_OctopusTxt;
    public bool Angry_Octopus;
    public bool Cocaine_Octopus;


    public AudioClip Bgm;
    public AudioSource AS;









    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();



    public void OnMinimizeButtonClick()
    {
        ShowWindow(GetActiveWindow(), 2);
    }
    private void Awake() {



    }
    void Start()
    {
        Angry_Octopus = true;
        print("Start");
        GameMode = false;
        PracticeMode = true;
        ChooseCharacter.instance.boolKai = true;
        AS.clip = Bgm;      // PlayerSound의 FallDown 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
        AS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMode == true)
        {
            
            Sel_GameMode.SetActive(true);
            UnSel_GameMode.SetActive(false);
            Sel_PracticeMode.SetActive(false);
            UnSel_PracticeMode.SetActive(true);
            Single_Popup.SetActive(false);
            Multi_Popup.SetActive(true);
        }
        if (PracticeMode == true)
        {
            
            Sel_GameMode.SetActive(false);
            UnSel_GameMode.SetActive(true);
            Sel_PracticeMode.SetActive(true);
            UnSel_PracticeMode.SetActive(false);
            Single_Popup.SetActive(true);
            Multi_Popup.SetActive(false);
        }
    }



    public void Click_Setting()
    {

    }
    public void Select_CreateRoomPopup()
    {
        print("Select_CreateRoomPopup");
        Select_CreateRoom_Popup.SetActive(true);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Select_CreateRoom_Popup.SetActive(false);

        }
    }
    public void Click_minimization()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void Click_Exit()
    {
        Application.Quit();
    }

    public void Click_GameMode()
    {
        GameMode = true;
        if (GameMode == true)
        {
            PracticeMode = false;

        }


    }

    public void Click_PracticeMode()
    {
        PracticeMode = true;
        if (PracticeMode == true)
        {
            GameMode = false;

        }



    }

   
    public void Click_Select_Character_Popup()
    {
        print("Click_Select_Character_Popup");
        Select_Character_Popup.SetActive(true);
    }
    public void Select_Character_Submit()
    {
        Select_Character_Popup.SetActive(false);
    }



    // Single Play

    public void Single_Sel_Angry_Octopus()
    {
        Angry_Octopus = true;
        Cocaine_Octopus = false;
        Sel_Single_Map_Angry_OctopusBtn.SetActive(true);
        UnSel_Single_Map_Angry_OctopusBtn.SetActive(false);
        Sel_Single_Map_Cocaine_OctopusBtn.SetActive(false);
        UnSel_Single_Map_Cocaine_OctopusBtn.SetActive(true);
        Single_Map_Angry_OctopusImage.SetActive(true);
        Single_Map_Cocaine_OctopusImage.SetActive(false);
        Single_Map_Map_Angry_OctopusTxt.SetActive(true);
        Single_Map_Cocaine_OctopusTxt.SetActive(false);
        MapName.text = "문어아빠의 분노(N)";
    }
    public void Single_Sel_Cocaine_Octopus()

    {
        Angry_Octopus = false;
        Cocaine_Octopus = true;
        Sel_Single_Map_Angry_OctopusBtn.SetActive(false);
        UnSel_Single_Map_Angry_OctopusBtn.SetActive(true);
        Sel_Single_Map_Cocaine_OctopusBtn.SetActive(true);
        UnSel_Single_Map_Cocaine_OctopusBtn.SetActive(false);
        Single_Map_Angry_OctopusImage.SetActive(false);
        Single_Map_Cocaine_OctopusImage.SetActive(true);
        Single_Map_Map_Angry_OctopusTxt.SetActive(false);
        Single_Map_Cocaine_OctopusTxt.SetActive(true);
        MapName.text = "세뇌당한 문어아빠(N)";

    }
    public void Single_StartGame()
    {
        if(Angry_Octopus == true)
        {
            SceneManager.LoadScene("SinglePlay_GameScene");
        }
        if (Cocaine_Octopus == true)
        {
            SceneManager.LoadScene("SinglePlay_GameScene");
        }
        
    }



}
