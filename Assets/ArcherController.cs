using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    Animator _animator;
    bool _isAiming;
    public PositionController cont;
    public GameObject arrow;
    public Transform arrowSpawnPoint;

    public Quaternion oldRotation;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        oldRotation = transform.rotation;

        //MovePlayerToNextPosition(cont.transformTest.position);
    }

    private void Update()
    {
        
    }
    //Move player to the next position, if there is any
    public void MovePlayerToNextPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, transform.position.y, position.z);
    }
    
}
