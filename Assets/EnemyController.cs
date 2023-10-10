using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    public Transform playerT;

    private void Awake()
    {
        playerT = GameObject.Find("FPS").transform;
    }

}
