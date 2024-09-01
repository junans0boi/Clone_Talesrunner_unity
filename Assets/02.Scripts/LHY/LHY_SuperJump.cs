using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_SuperJump : MonoBehaviour
{

    Rigidbody rb;
    public float force = 5;

    CharacterController cc;

    public Animator anim;

    public Transform RedPos;

    public Transform BluePos;

    Transform targetPos;

    /*  public Animator playeranim;

     *//* public bool playerhit = false;

      public LHY_XBoxG instance;*//*

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
            // anim.SetTrigger("Playerhit");

            // playerhit = true;

            anim.Play("Jump");

            cc = collision.gameObject.GetComponent<CharacterController>();
            cc.enabled = false;
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            if(collision.gameObject.layer == 13)
            {
                print("!!1");
                Vector3 ColliderRot = RedPos.position - collision.transform.position * Time.deltaTime + Vector3.down * 2 / Time.deltaTime;
                ColliderRot.Normalize();

                collision.gameObject.GetComponent<Rigidbody>().AddForce((ColliderRot + Vector3.up) * force, ForceMode.Impulse);
                // Invoke("NonCollider", 2.5f);
            }
            else if(collision.gameObject.layer ==14)
            {
                print("!!14");
                Vector3 ColliderRot = BluePos.position - collision.transform.position * Time.deltaTime + Vector3.down * 2 / Time.deltaTime;
                ColliderRot.Normalize();

                collision.gameObject.GetComponent<Rigidbody>().AddForce((ColliderRot + Vector3.up) * force, ForceMode.Impulse);
            }


            StartCoroutine(Collider());
        }


        //Destroy(gameObject, 1.1f);
    }
    IEnumerator Collider()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        rb.useGravity = false;
        cc.enabled = true;
    }
}
