using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_ElecRotation : MonoBehaviour
{
    //전기줄 큐브를 시계 방향(반시계방향)으로 회전하게 하고 싶다.
    //필요속성 :  회전방향

    public float rotspeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotspeed);
    }
}