using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;
    bool hitsomeThing = false;
    public bool isShot = false;
    public GameObject backParticle;
    public GameObject bloodParticle;
    public Collider col;
    // Start is called before the first frame update
    void Start()
    {
        if (isShot && !hitsomeThing)
        {
          //  transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShot && !hitsomeThing)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Arrow"))
        {
            col.enabled = false;
            backParticle.SetActive(false);
            hitsomeThing = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = collision.gameObject.transform;
            if(collision.gameObject.CompareTag("EnemyHead"))
            {
                bloodParticle.SetActive(true);
            }
        }
      
    }
}
