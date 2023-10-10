using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public BoxCollider mainCollider;
    public GameObject rig;
    public Animator animator;

    Collider[] colls;
    Rigidbody[] rbs;
    CharacterJoint[] cj;

    private void Start()
    {
        GetRagdollBits();
        RagdollOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("arrow"))
            RagdollOn();
    }


    void RagdollOn()
    {
        animator.enabled = false;
        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (Collider c in colls)
        {
            c.enabled = true;
        }

        foreach (Rigidbody r in rbs)
        {
            r.isKinematic = false;
        }

        
        
    }

    void RagdollOff()
    {
        foreach(Collider c in colls)
        {
            c.enabled = false;
        }

        foreach(Rigidbody r in rbs)
        {
            r.isKinematic = true;
        }

        foreach(CharacterJoint c in cj)
        {
            c.enableCollision = false;
            c.enablePreprocessing = false;
            c.enableProjection = false;
        }

        animator.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void GetRagdollBits()
    {
        colls = rig.GetComponentsInChildren<CapsuleCollider>();
        rbs = rig.GetComponentsInChildren<Rigidbody>();
        cj = rig.GetComponentsInChildren<CharacterJoint>();
    }

}
