using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    [Header("PlayerInformation")]
    
    public Text NicknameText;
    //MakeRoom RoomName InputField
    public InputField inputRoomName;
    //MakeRoom MaxPlayer InputField
    public InputField inputMaxPlayer;
    //MakeRoom Paasword InputField
    public InputField inputPassword;
    Text text;

    public bool SetPassword;
    //JoinRoom PassWordInputPopup;
    public GameObject JoinInputPasswordPopup;
    //JoinRoom SaveRoomName;
    public string JoininputRoomName;
    //JoinRoom InputField Password
    public InputField JoinInputPassword;
    //JoinRoom Button
    public Button btnJoin;
    //MakeRoom Button
    public Button btnCreate;

    //RoomList Content
    public Transform roomListContent;

    // RoomInfomation
    Dictionary<string, RoomInfo> roomCache = new Dictionary<string, RoomInfo>();
    // RoomCreateFactory
    public GameObject roomItemFactory;
    //map Thumbnail
    public GameObject[] mapThums;

    void Start()
    {
        // 방이름(InputField)이 변경될때 호출되는 함수 등록
        inputRoomName.onValueChanged.AddListener(OnRoomNameValueChanged);
        // 총인원(InputField)이 변경될때 호출되는 함수 등록
        inputMaxPlayer.onValueChanged.AddListener(OnMaxPlayerValueChanged);

        PhotonNetwork.AutomaticallySyncScene = true;

        NicknameText.text = PhotonNetwork.NickName;
        print(NicknameText.text);

        
    }

    void OnRoomNameValueChanged(string room)
    {
        // Join Room
        btnJoin.interactable = room.Length > 0;
        // Make Room
        btnCreate.interactable = room.Length > 0 && inputMaxPlayer.text.Length > 0;
    }

    void OnMaxPlayerValueChanged(string max)
    {
        // Create
        btnCreate.interactable = max.Length > 0 && inputRoomName.text.Length > 0;
    }



    //CreateRoom
    public void CreateRoom()
    {
        // MakeRoom Setting
        RoomOptions roomOptions = new RoomOptions();
        // MaxPlayerCount(MaxPlayer == 0)
        roomOptions.MaxPlayers = byte.Parse(inputMaxPlayer.text);
        // RoomList에 IsVisible? 보이게?
        roomOptions.IsVisible = true;
        // custom Infomation Setting
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash["description"] = "This is Rookie Room" + Random.Range(1, 1000);
        hash["mapId"] = Random.Range(0, mapThums.Length);
        hash["room_name"] = inputRoomName.text;
        JoininputRoomName = inputRoomName.text; 
        hash["password"] = inputPassword.text;
        JoinInputPassword.text = inputPassword.text;



        roomOptions.CustomRoomProperties = hash;
        //custom 정보를 공개하는 설정
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "description", "mapId" , "room_name", "password"};

        // 방 생성 요청 (해당 옵션을 이용해서)
        PhotonNetwork.CreateRoom(inputRoomName.text, roomOptions);
        
    }

    //방이 생성되면 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    // if -> reateRoomFailed
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed , " + returnCode + ", " + message);
    }

    // JoinRoom -> NonPassWord?! | PassWord?!
    public void JoinInputPasswordOnPopup(string room)
    {
        RoomInfo info = roomCache[room];
        string password = (string)info.CustomProperties["password"];
        if (password.Length >= 1)// 방에 비밀번호가 있다
        {
            JoinInputPasswordPopup.SetActive(true);
            print("1111");
        }
        else // 방에 비밀번호가 없다면
        {
            PhotonNetwork.JoinRoom(room);
            
        }
    }
    // JoinRoom -> PassWord Input -> Submit Btn Click
    public void JoinRoom_Password(string room)
    {
        PhotonNetwork.JoinRoom(room + JoinInputPassword.text);
    }

    // JoinRoom Requested
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("WaitingRoom에 참가하였습니다.");
        PhotonNetwork.LoadLevel("WaitingRoom");
    }

    //방 참가가 완료 되었을 때 호출 되는 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("WaitingRoom에, " + returnCode + ", " + message + "사유로 참가하지 못했습니다.");
    }

    //방 참가가 실패 되었을 때 호출 되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);


        // Remove -> RoomList
        RemoveRoomListUI();
        // Update -> RoomList
        UpdateRoomCache(roomList);
        // Create All RoomList UI
        CreateRoomListUI();
    }

    void RemoveRoomListUI()
    {
        foreach (Transform tr in roomListContent)
        {
            Destroy(tr.gameObject);
        }

        //Button[] tr = roomListContent.GetComponentsInChildren<Button>();
        //for (int i = 0; i < tr.Length; i++)
        //    Destroy(tr[i].gameObject);
    }

    void UpdateRoomCache(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            // Edit Or Remove
            if(roomCache.ContainsKey(info.Name))
            {
                // 만약에 해당 룸이 삭제된것이라면(if Room is already Removed?)
                if (info.RemovedFromList)
                {
                    //roomCache 에서 해당 정보를 삭제(roomCache -> Remove RoomInfomation )
                    roomCache.Remove(info.Name);
                }
                else
                {
                    // Edit Infomation
                    roomCache[info.Name] = info;
                }
            }
            else
            {
                // Add roomCache
                roomCache[info.Name] = info;
            }
        }
    }

    void CreateRoomListUI()
    {
        foreach(RoomInfo info in roomCache.Values)
        {
            // make RoomItem
            GameObject go = Instantiate(roomItemFactory, roomListContent);

            //Setting RoomItem Info
            RoomItem roomItem = go.GetComponent<RoomItem>();
            roomItem.SetInfo(info);
            // onClick roomItem -> Call Founction 
            roomItem.onClickAction = SelectRoom;

            
            string des = (string)info.CustomProperties["description"];
            int mapId = (int)info.CustomProperties["mapId"];
            print(des + ", " + mapId);

            
        }
    }

    int prevMapId = -1;
    void SelectRoom(string room, int mapId)
    {
        // Room name Setting          
        inputRoomName.text = room;

        //만약에 이전 맵 Thumbnail이 활성화가 되어있다면
        if (prevMapId > -1)
        {
            //이전 맵 Thumbnail을 비활성화
            mapThums[prevMapId].SetActive(false);
        }

        //맵 Thumbnail 설정
        mapThums[mapId].SetActive(true);
        //prevMapId <- mapId Save
        prevMapId = mapId;
    }
}
