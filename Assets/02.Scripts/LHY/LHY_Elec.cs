using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Elec : MonoBehaviour
{
    //�̵��ӵ�, �̵�����

    public float speed = 5;

    Vector3 dir = Vector3.back;

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }
}
