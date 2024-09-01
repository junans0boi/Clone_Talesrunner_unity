using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_SpawnHtDt : MonoBehaviour
{
    public GameObject htdtFactory;

    float currtime;

    public float spawnTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;
        if (currtime > spawnTime)
        {
            GameObject htdt = Instantiate(htdtFactory);
            htdt.transform.position = transform.position;

            currtime = 0;
        }
    }
}
