using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerBehaviour : MonoBehaviour
{   
    public Transform target;
    private Transform hitbox;
    public float speed;

    public 
    //values for internal use

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        Movement();
    }

    void Movement()
    {
        hitbox = target.transform.Find("Target");
        Vector3 alteredTarget = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.LookAt(hitbox);
        transform.position += (transform.forward * speed) * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponent<Health>().DealDamage(1);
            Destroy(this.gameObject);
        }
        
    }

}
