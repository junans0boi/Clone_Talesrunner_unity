using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_SingleCamoff : MonoBehaviour
{
    float currtime;

    public float offTime = 3;

    public GameObject startcam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;

        if(currtime > offTime)
        {
            startcam.SetActive(false);
        }
    }
}
