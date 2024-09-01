using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomPlayerAnim : MonoBehaviourPun
{


    public bool ReadyboolBtn = false;
    //public Animator anim;
    public Animator Lerfanim;
    public Animator Kaianim;
    public Animator Badaanim;
    public Animator Beraanim;
    public WaitingRoomSystemManager waitingRoomSystemManager;
    public GameObject Lerf;
    public GameObject Kai;
    public GameObject Bada;
    public GameObject Bera;
    Vector3 receivePos;
    //회전되야 하는 값
    Quaternion receiveRot;
    //보간 속력
    public float lerpSpeed = 100;
    void Start()
    {


        
    
        //OnPhotonSerializeView 호출 빈도
        PhotonNetwork.SerializationRate = 60;
        //Rpc 호출 빈도
        PhotonNetwork.SendRate = 60;


        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Anim()
    {
        if (photonView.IsMine)
        {

            if (Lerf.activeSelf == true)
            {
                //Lerfanim.SetBool("Run", true);
                photonView.RPC("Lerf_RpcSetBool", RpcTarget.All, "Run", true);
                Invoke("Animfalse", 5f);
            }

            if (Kai.activeSelf == true)
            {
                //Kaianim.SetBool("Run", true);
                photonView.RPC("Kai_RpcSetBool", RpcTarget.All, "Run", true);
                Invoke("Animfalse", 5f);
            }

            if (Bada.activeSelf == true)
            {
                //Badaanim.SetBool("Run", true);
                photonView.RPC("Bada_RpcSetBool", RpcTarget.All, "Run", true);
                Invoke("Animfalse", 5f);
            }

            if (Bera.activeSelf == true)
            {
                //Beraanim.SetBool("Run", true);
                photonView.RPC("Bera_RpcSetBool", RpcTarget.All, "Run", true);
                Invoke("Animfalse", 5f);
            }
        }
    }
    public void Animfalse()
    {
        photonView.RPC("Lerf_RpcSetBool", RpcTarget.All, "Run",false);
        photonView.RPC("Kai_RpcSetBool", RpcTarget.All, "Run",false);
        photonView.RPC("Bada_RpcSetBool", RpcTarget.All, "Run",false);
        photonView.RPC("Bera_RpcSetBool", RpcTarget.All, "Run",false);
        
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //데이터 보내기
        if(stream.IsWriting) // isMine == true
        {
            //position, rotation
            stream.SendNext(transform.rotation);            
            stream.SendNext(transform.position);
        }
        //데이터 받기
        else if(stream.IsReading) // ismMine == false
        {
            receiveRot = (Quaternion)stream.ReceiveNext();
            receivePos = (Vector3)stream.ReceiveNext();
        }
    }
    
    [PunRPC]
    public void Lerf_RpcSetBool(string animName, bool animBool)
    {
        Lerfanim.SetBool(animName, animBool);
        
    } 
    [PunRPC]
    public void Kai_RpcSetBool(string animName, bool animBool)
    {
        Kaianim.SetBool(animName, animBool);
        
    } 
    [PunRPC]
    public void Bada_RpcSetBool(string animName, bool animBool)
    {
        Badaanim.SetBool(animName, animBool);
        
    } 
    [PunRPC]
    public void Bera_RpcSetBool(string animName, bool animBool)
    {
        Beraanim.SetBool(animName, animBool);
        
    } 
    
    
    
    
    
}
