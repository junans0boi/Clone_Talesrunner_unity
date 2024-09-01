using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = new Vector3(0, Camera.main.transform.forward.y, 0);      
    }
}
