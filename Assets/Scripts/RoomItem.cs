using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    //?? Text
    public Text roomInfo;
    //??
    public Text roomDescription;

    //??? ??? ? ???? ??? ????? ??
    public Action<string, int> onClickAction;






    //map Id
    int mapId;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetInfo(string roomName, int currPlayer, byte maxPlayer)
    {
        //??????? ??? roomName??!
        name = roomName;
        //??? (0/0)
        roomInfo.text = roomName + " (" + currPlayer + " / " + maxPlayer + ")";
    }

    public void SetInfo(RoomInfo info)
    {
        SetInfo((string)info.CustomProperties["room_name"], info.PlayerCount, info.MaxPlayers);

        //??? ?????? ??????
        roomDescription.text = (string)info.CustomProperties["description"];

        //??? id ??????
        mapId = (int)info.CustomProperties["mapId"];
    }



    public void OnClick()
    {
        //??? onClickAction ? null? ????
        if (onClickAction != null)
        {
            //onClickAction ??
            onClickAction(name, mapId);
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().JoinInputPasswordOnPopup(name);
            //GameObject.Find("LobbyManager").GetComponent<LobbyManager>().OnJoinedRoom();
        }


        ////1. InputRoomName ????? ??
        //GameObject go = GameObject.Find("InputRoomName");
        ////2. InputField ???? ????
        //InputField inputField = go.GetComponent<InputField>();
        ////3. text? roomName ??.
        //inputField.text = name;
    }
}

