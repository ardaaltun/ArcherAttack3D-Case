using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool hit = false;
    public GameObject arrowHitPrefab;

    

    private void OnCollisionEnter(Collision collision)
    {
        InputController ic = GameObject.Find("InputController").GetComponent<InputController>();
        GameManager gameManager = ic.gameManager;
        if (!hit)
        {
            if (collision.gameObject.CompareTag("head") || collision.gameObject.CompareTag("chest") || collision.gameObject.CompareTag("arm") || collision.gameObject.CompareTag("leg"))
            {
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collision.gameObject.transform;

                gameManager.totalEnemyCount--;
                hit = true;

            }
            else if(collision.gameObject.CompareTag("environment"))
            {
                Instantiate(arrowHitPrefab, collision.contacts[0].point, transform.rotation);
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collision.gameObject.transform;

                hit = true;
            }

            GetComponent<TrailRenderer>().enabled = false;
            
            if (gameManager.totalEnemyCount == 0)
            {
                gameManager.YouWin();
            }
            else if (ic.arrowCount == 0)
            {
                gameManager.YouLose();
            }

        }
        
    }
}
