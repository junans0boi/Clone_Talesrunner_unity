using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomPlayerMove : MonoBehaviourPun, IPunObservable
{
    //도착 위치
    Vector3 receivePos;
    //회전되야 하는 값
    Quaternion receiveRot;
    //보간 속력
    public float lerpSpeed = 100;

    public float Speed = 50;
    public Vector3 Target;
    public bool ReadyboolBtn = false;
    
    public WaitingRoomSystemManager WSM;
    public WaitingRoomPlayerAnim waitingRoomPlayerAnim;
    public GameObject Lerf;
    public GameObject Kai;
    public GameObject Bada;
    public GameObject Bera;
    void Start()
    {
        
        //Lerfanim = Lerf.GetComponent<Animator>();
        //Kaianim = Kai.GetComponent<Animator>();
        //Badaanim = Bada.GetComponent<Animator>();
        //Beraanim = Bera.GetComponent<Animator>();
        waitingRoomPlayerAnim = GetComponentInChildren<WaitingRoomPlayerAnim>();
        WSM = GameObject.Find("WaitingRoomManager").GetComponent<WaitingRoomSystemManager>();

        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        //Rpc 호출 빈도
        PhotonNetwork.SendRate = 60;
        WSM.playerMoves.Add(this);
        Target = WSM.targetPosition;
        
    }

    
    void Update()
    {
        
        if (photonView.IsMine)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
            WSM.NicknameText[PhotonNetwork.CurrentRoom.PlayerCount - 1].text = photonView.Owner.NickName;

            waitingRoomPlayerAnim.Anim();
            if (PhotonNetwork.CurrentRoom.PlayerCount - 1 <= 4)
            {
                ChooseCharacter.instance.RedTeam = true;
                WSM.TeamText[PhotonNetwork.CurrentRoom.PlayerCount - 1].SetActive(true);
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount - 1 >= 5)
            {
                ChooseCharacter.instance.BlueTeam = true;
                WSM.TeamText[PhotonNetwork.CurrentRoom.PlayerCount - 1].SetActive(true);
            }
            if (ChooseCharacter.instance.RedTeam == true)
            {
                this.gameObject.layer = 11;
            }
            if (ChooseCharacter.instance.BlueTeam == true)
            {
                this.gameObject.layer = 12;
            }
        }
        
        else
        {
            
            //Lerp를 이용해서 목적지, 목적방향까지 이동 및 회전
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
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

























