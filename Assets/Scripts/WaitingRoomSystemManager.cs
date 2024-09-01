using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomSystemManager : MonoBehaviourPun

{
    public int Spawnidx;
    public List<WaitingRoomPlayerMove> playerMoves = new List<WaitingRoomPlayerMove>();

    
    public Text[] NicknameText;
    // spawn위치를 담을 변수
    public Vector3[] SpawnPos;
    public Vector3[] SpawnMovePos;
    public Vector3 targetPosition;
    public GameObject[] TeamText;

    public AudioClip Bgm;
    public AudioSource AS;

    public int ReadyPlayers = 0;

    public GameObject StartBtn;
    [Header("Map About")]
    public GameObject SelMapPopup;
    public GameObject MapFrame_CocainOct;
    public GameObject MapFrame_With4Mind;
    public bool CocainMap;
    public bool AliceMap;
    public bool StatingNow;
    public GameObject SelectMapBtn;
    public GameObject NonSel_Cocaine_Octopus;
    public GameObject NonSel_With4Mind;
    public GameObject Sel_Cocaine_Octopus;
    public GameObject Sel_With4Mind;

    private void Awake()
    {
        //instance = this;

    }

    void Start()
    {

        AS.clip = Bgm;      // PlayerSound의 FallDown 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
        AS.Play();

        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        //Rpc 호출 빈도
        PhotonNetwork.SendRate = 60;


        MapFrame_CocainOct.SetActive(true);
        
        CocainMap = true;
        AliceMap = false;
        StatingNow = false;
            PhotonNetwork.Instantiate("WaitingroomPlayer", SpawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1], Quaternion.identity);
        Spawnidx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        targetPosition = SpawnMovePos[PhotonNetwork.CurrentRoom.PlayerCount - 1];
        
        

}

    void Update()
    {

        if (CocainMap == true)
        {
            MapFrame_CocainOct.SetActive(true);
            MapFrame_With4Mind.SetActive(false);
        }
        if (AliceMap == true)
        {
            MapFrame_CocainOct.SetActive(false);
            MapFrame_With4Mind.SetActive(true);
        }
    }

    // 현재 방에 있는 Player를 담아 놓자
    public List<PhotonView> players = new List<PhotonView>();

    //public void AddPlayer(PhotonView pv)
    //{
    //    players.Add(pv);
    //    // 만약에 인원이 다 들어왔으면
    //    if (players.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
    //    {
    //        // 턴 변경
    //        ChangeTurn();

    //    }

    //}

    //// 턴 병경시 호출해주는 함수
    //public int turnIdx = -1;

    //public void ChangeTurn()
    //{
    //    // 방장이 아니라면 함수를 나가라
    //    if (PhotonNetwork.IsMasterClient == false) return;
    //    // 이전 차례 였던 애는 총을 쏘지못하게
    //    if (turnIdx > -1)
    //    {
    //        players[turnIdx].RPC("SetMyTurn", RpcTarget.All, false);
    //    }

    //    // 이번엔 너의 차례다.
    //    turnIdx++;
    //    turnIdx %= players.Count;
    //    players[turnIdx].RPC("SetMyTurn", RpcTarget.All, true);
    //}

    public void SelMapPopupBtn()
    {
        SelMapPopup.SetActive(true);
    }

    public void SelMapPopupSubmitBtn()
    {
        SelMapPopup.SetActive(false);

    }


    public void SelCocainOct()
    {
        CocainMap = true;
        AliceMap = false;
        NonSel_Cocaine_Octopus.SetActive(false);
        Sel_Cocaine_Octopus.SetActive(true);
        NonSel_With4Mind.SetActive(true);
        Sel_With4Mind.SetActive(false);
    }

    public void SelAlice()
    {
        NonSel_Cocaine_Octopus.SetActive(true);
        Sel_Cocaine_Octopus.SetActive(false);
        NonSel_With4Mind.SetActive(false);
        Sel_With4Mind.SetActive(true);
        AliceMap = true;
        CocainMap = false;

    }

}