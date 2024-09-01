using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{

    public static ChooseCharacter instance;
    [Header("Select Character")]
    public bool boolKai;
    public bool boolBera;
    public bool boolBada;
    public bool boolLerf;
    public GameObject Kai;
    public GameObject Bera;
    public GameObject Bada;
    public GameObject Lerf;

    public GameObject KaiTxt;
    public GameObject BeraTxt;
    public GameObject BadaTxt;
    public GameObject LerfTxt;

    public bool RedTeam;
    public bool BlueTeam;
      void Awake()
    {
        
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            
        }
        
    }
    

    void Start()
    {
        
        boolKai = true;
        KaiTxt.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void Select_Character_Kai()
    {
        
        boolKai = true;
        boolBada = false;
        boolBera = false;
        boolLerf = false;
        Kai.SetActive(true);
        Bada.SetActive(false);
        Bera.SetActive(false);
        Lerf.SetActive(false);
        KaiTxt.SetActive(true);
        BeraTxt.SetActive(false);
        BadaTxt.SetActive(false);
        LerfTxt.SetActive(false);
      
    }
    public void Select_Character_Baba()
    {
        
        boolKai = false;
        boolBada = true;
        boolBera = false;
        boolLerf = false;
        Kai.SetActive(false);
        Bada.SetActive(true);
        Bera.SetActive(false);
        Lerf.SetActive(false);
        KaiTxt.SetActive(false);
        BeraTxt.SetActive(false);
        BadaTxt.SetActive(true);
        LerfTxt.SetActive(false);
    }
    public void Select_Character_Bera()
    {
        
        boolKai = false;
        boolBada = false;
        boolBera = true;
        boolLerf = false;
        Kai.SetActive(false);
        Bada.SetActive(false);
        Bera.SetActive(true);
        Lerf.SetActive(false);
        KaiTxt.SetActive(false);
        BeraTxt.SetActive(true);
        BadaTxt.SetActive(false);
        LerfTxt.SetActive(false);
    }
    public void Select_Character_Lerf()
    {
        
        boolKai = false;
        boolBada = false;
        boolBera = false;
        boolLerf = true;
        Kai.SetActive(false);
        Bada.SetActive(false);
        Bera.SetActive(false);
        Lerf.SetActive(true);
        KaiTxt.SetActive(false);
        BeraTxt.SetActive(false);
        BadaTxt.SetActive(false);
        LerfTxt.SetActive(true);
    }

}
