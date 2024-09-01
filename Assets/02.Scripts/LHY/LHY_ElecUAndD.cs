using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_ElecUAndD : MonoBehaviour
{
    public float DMax = -6.5f;
    public float UMax = 3f;

    public float Maxdis;

    float cullPosx;
    float cullPosy;
    float cullposz;

    public float directionspeed = 10f;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        //현재 포지션의 x값에 내가 태어난 위치의 x값을 저장한다.
        cullPosx = transform.position.x;
        //현재 포지션의 y값에 내가 태어난 위치의 y값을 저장한다.
        cullPosy = transform.position.y;
        //현재 포지션의 z값에 내가 태어난 위치의 z값을 저장한다.
        cullposz = target.position.z;

        //오른쪽으로 이동할 수 있는 최대거리, 왼쪽으로 이동할 수 있는 최대 거리의 값에 Maxdis를 대입한다.
        //DMax = -Maxdis;
        //lMax = Maxdis;

        transform.position = new Vector3(cullPosx, cullPosy, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cullposz = target.position.z;
        cullPosy += Time.deltaTime * directionspeed;
        if (Maxdis > 0)
        {
            if (cullPosy <= DMax)
            {
                directionspeed *= -1;
                cullPosy = DMax;
            }
            if (cullPosy >= UMax)
            {
                directionspeed *= -1;
                cullPosy = UMax;
            }
            transform.position = new Vector3(cullPosx, cullPosy, cullposz);
        }
        else if (Maxdis < 0)
        {
            if (cullPosy >= DMax)
            {
                directionspeed *= -1;
                cullPosy = DMax;
            }
            if (cullPosy <= UMax)
            {
                directionspeed *= -1;
                cullPosy = UMax;
            }
            transform.position = new Vector3(cullPosx, cullPosy, cullposz);

        }
    }
}