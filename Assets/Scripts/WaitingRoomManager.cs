 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


using Hashtable = ExitGames.Client.Photon.Hashtable;
public class WaitingRoomManager : MonoBehaviourPunCallbacks
{

    //도착 위치
    Vector3 receivePos;
    //회전되야 하는 값
    Quaternion receiveRot;
    //보간 속력
    public float lerpSpeed = 100;

    [Header("UpState")]
    //RoomName Text
    public Text RoomName;
    [Header("DownState")]
    
    public bool DoReady;
    public bool StartReady;

    public Button IamReadyButton;

    public GameObject NoticeCountGameObject;
    public Text Notice;
    public Text NoticeCount;
    public float setTime = 0;
    bool Start_Countbool;


    public WaitingRoomSystemManager WSM;
    void Start()
    {
        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        //Rpc 호출 빈도
        PhotonNetwork.SendRate = 60;
        setTime = 10.0f;
        Start_Countbool = false;
        // 마스터 클라이언트는 PhotonNetwork.LoadLevel()를 호출할 수 있고, 모든 연결된 플레이어는 자동적으로 동일한 레벨을 로드한다.
        NoticeCount.text = setTime.ToString();
        
    }
    void Update()
    {
        if (WSM.StatingNow == true)
        {
            Notice.text = "   초 후에 게임이 시작됩니다";
            if (setTime > 0)
            {
                NoticeCountGameObject.SetActive(true);
                setTime -= Time.deltaTime;
                NoticeCount.text = setTime.ToString();
                // print("current time: "+setTime);
                if (setTime <= 0)
                {
                    EnterGame();
                }
            }
        }




        if (PhotonNetwork.IsMasterClient == true && photonView.IsMine)
        {
            WSM.SelectMapBtn.SetActive(true);
            WSM.StartBtn.SetActive(true);
        }
        if (PhotonNetwork.IsMasterClient == false && photonView.IsMine)
        {
            WSM.SelectMapBtn.SetActive(false);
            WSM.StartBtn.SetActive(false);
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Start_Countbool = true;
        }
        else if(PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Start_Countbool = false;
            
        }
        
    }

    // JoinRoom -> PassWord Input -> Submit Btn Click
    public void JoinGame(string room)
    {
        PhotonNetwork.JoinRoom(room);
        

    }



    // JoinRoom Requested
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"플레이어{newPlayer.NickName}이 참가했습니다.");
        base.OnPlayerEnteredRoom(newPlayer);

        
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
                
            // Invoke("EnterGame", 10f);
                
        }
        

    }



    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed, " + returnCode + ", " + message);
    }
  

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
        if(PhotonNetwork.IsMasterClient == true && photonView.IsMine)
        {
            this.photonView.RequestOwnership();
        }
    }

    public void EnterGame()
    {
        if (WSM.CocainMap == true)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
        if (WSM.AliceMap == true)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }

    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    

    public void ActiveStartBtn()
    {
        if (Start_Countbool == true && PhotonNetwork.IsMasterClient == true)
        {
            WSM.StatingNow = true;
        }
        else if (Start_Countbool == false)
        {
            Notice.text = "아직 인원이 다 차지 않았습니다!";
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //데이터 보내기
        if (stream.IsWriting) // isMine == true
        {
            //position, rotation
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.position);
        }
        //데이터 받기
        else if (stream.IsReading) // ismMine == false
        {
            receiveRot = (Quaternion)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();
        }

    }

}
