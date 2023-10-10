using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool hit = false;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(!hit)
        {
            if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("chest") || collision.gameObject.CompareTag("arm") || collision.gameObject.CompareTag("leg"))
            {
                Debug.Log(collision.gameObject.tag);
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collision.gameObject.transform;
                
                hit = true;
            }
        }
        
    }
}
