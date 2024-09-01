using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { None, Collider, NonCollider };
public class ColliderFloor : MonoBehaviour
{

    Rigidbody rb;
    public float force = 5;

    CharacterController cc;

  /*  public Animator playeranim;

    public bool playerhit = false;

    public ColliderFloor instance;

    private void Awake()
    {
        instance = this;
    }*/

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            //playerhit = true;

            cc = collision.gameObject.GetComponent<CharacterController>();
            cc.enabled = false;
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            print("!!1");
            Vector3 ColliderRot = collision.transform.position - transform.position;
            ColliderRot.Normalize();

            collision.gameObject.GetComponent<Rigidbody>().AddForce((ColliderRot + Vector3.up) * force, ForceMode.Impulse);
            // Invoke("NonCollider", 2.5f);
            StartCoroutine(Collider());
        }

        //playerhit = false;

    }
    IEnumerator Collider()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        rb.useGravity = false;
        cc.enabled = true;
    }

}