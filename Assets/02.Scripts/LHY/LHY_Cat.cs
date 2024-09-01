using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHY_Cat : MonoBehaviour
{
    public Animator anim;

    Rigidbody rb;
    public float force = 5;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* private void OnCollisionEnter(Collision collision)
     {
         if (collision.gameObject.CompareTag("Player"))
         {
              print("2222222");
            anim.SetTrigger("CatBool");

         }
     }*/

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //playerhit = true;

            cc = collision.gameObject.GetComponent<CharacterController>();
            cc.enabled = false;
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            print("!!1");
            Vector3 ColliderRot = Vector3.back;
            ColliderRot.Normalize();

            collision.gameObject.GetComponent<Rigidbody>().AddForce((ColliderRot + Vector3.up) * force, ForceMode.Impulse);
            // Invoke("NonCollider", 2.5f);
            StartCoroutine(Collider());
            print("1111");
            anim.SetTrigger("CatBool");
        }
            
            
    }

   
    IEnumerator Collider()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        rb.useGravity = false;
        cc.enabled = true;
    }
}
