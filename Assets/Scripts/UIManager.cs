  using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviourPun
{
    [Header("About Damage")]
    // 데미지 게이지 프레임
    public Image DamageGageFrame;
    // 데미지 됐을떄 출력할 텍스트
    public GameObject DamageTxt;
    [Header("About Dash")]
    // 대쉬 가능을 알리는 텍스트
    public GameObject CanDashTxt;
    // 대쉬 발동을 알리는 텍스트
    public GameObject GoDashTxt;
    [Header("About Run")]
    // 달리기 게이지 프레임 Image
    public Image RunGageFrame;
    // 현재 달리기 게이지 잔량 텍스트
    public Text RunGage;
    [Header("About VunNo")]
    public GameObject VunNo;
    public GameObject VunnoGageFrame;
    [Header("About Rank")]
    public GameObject Goal;
    public Text RankNum;
    
    public Text EndCount;

    [Header("About State")]
    public Text NowState;

    [Header("About RankPopup")]
    public GameObject RankFrame;
    public Text[] Rank;
    public Text[] Name;
    public Text[] Time;
    
    //도착 위치
    Vector3 receivePos;
    //회전되야 하는 값
    Quaternion receiveRot;
    //보간 속력
    public float lerpSpeed = 100;

    
    
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
