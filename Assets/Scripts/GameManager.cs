using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    // spawn위치를 담을 변수
    public Vector3[] SpawnPos;

    public Text _TimerText;
    float _Sec;
    int _Min;


    public GameObject goalPopup;
    public List<PlayerMove> playerMoves = new List<PlayerMove>();
    public bool EnterGamebool;
    public bool Gameing;
    public Text ScoreTxt;
    public GameObject Canvas;

    public GameObject Canvas2;

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

        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        //Rpc 호출 빈도
        PhotonNetwork.SendRate = 60;
 
        int idx = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        //플레이어를 생성한다.
        string characterName = "";
        if (ChooseCharacter.instance.boolKai == true)
        {
            characterName = "Kai";
        }
        if (ChooseCharacter.instance.boolBada == true)
        {
            characterName = "Bada";
        }
        if (ChooseCharacter.instance.boolLerf == true)
        {
            characterName = "Lerf";
        }
        if (ChooseCharacter.instance.boolBera == true)
        {
            characterName = "Bera";
        }
        PhotonNetwork.Instantiate(characterName, SpawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1], Quaternion.identity);
    }


    void Update()
    {
        Timer();
    }
    // 현재 방에 있는 Player를 담아 놓자
    public List<PhotonView> players = new List<PhotonView>();
    public void AddPlayer(PhotonView pv)
    {
        players.Add(pv);
        // 만약에 인원이 다 들어왔으면
        if (players.Count == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // 턴 변경
            ChangeTurn();

        }

    }
    public void EnterGame()
    {
        EnterGamebool = true;
        Gameing = true;
        Canvas.SetActive(true);

        Canvas2.SetActive(true);
    }
    void Timer()
    {
        _Sec += Time.deltaTime;
        _TimerText.text = "Time : 00 :" + string.Format("{0:D2} :{1:D2}", _Min, (int)_Sec);
        if ((int)_Sec > 59)
        {
            _Sec = 0;
            _Min++;
        }
    }

    // 턴 병경시 호출해주는 함수
    public int turnIdx = -1;
    public void ChangeTurn()
    {
        // 방장이 아니라면 함수를 나가라
        if (PhotonNetwork.IsMasterClient == false) return;
        // 이전 차례 였던 애는 총을 쏘지못하게
        if (turnIdx > -1)
        {
            players[turnIdx].RPC("SetMyTurn", RpcTarget.All, false);
        }
        // 이번엔 너의 차례다.
        turnIdx++;
        turnIdx %= players.Count;
        players[turnIdx].RPC("SetMyTurn", RpcTarget.All, true);
    }

    //방에 플레이어가 참여 했을때 호출해주는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print(newPlayer.NickName + "님 이 참여 하였습니다.");
    }


    public void BroadCastGameEnd()
    {
        for (int i = 0; i < playerMoves.Count; i++)
        {
            print("반복문 시작");
            playerMoves[i].EndTimerStart();
        }
    }
    
}