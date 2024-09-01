using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Destroyzone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
