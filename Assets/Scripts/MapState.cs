using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapState : MonoBehaviour
{
    [Header("About MapState SliderBar")]

    public Slider mapState;
    public GameObject Player; //플레이어 오브젝트 
    public Transform StartPosition; // 맵의 시작 지점
    public Transform ArrivalPosition; // 맵의 종료 지점
    private float curPos; // 지금 현재 지나 가고 있는 지점의 % 수치
    private float minPos; // 맵 시작 지점의 % 수치 = 0%
    private float maxPos;   // 도착 지점의 % 수치 = 100%


    Vector3 StartPosdir; // 시작 위42.1
    Vector3 ArrivalPosdir; // 도착위치
    Vector3 CurrentPosdir; // 현재 플레이어의 위치 

    void Start()
    {
        StartPosdir = StartPosition.transform.position;
        ArrivalPosdir = ArrivalPosition.transform.position;
        minPos = StartPosdir.z;
        maxPos = ArrivalPosdir.z;

        mapState.minValue = minPos - 42.1f;
        mapState.maxValue = maxPos + 1762.11f;
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosdir = Player.transform.position;
        curPos = CurrentPosdir.z;
        //curPos -= 34.5f;
        mapState.value = ((float)curPos / (float)maxPos) * 100;

    }

}
