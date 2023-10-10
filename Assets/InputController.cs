using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public ArcherController archerController;

    [SerializeField] private Animator _animator;
    [SerializeField] Camera _FPScamera, _TPSCamera;
    public Image crosshair;
    RaycastHit hitInfo;

    private Vector3 dragOrigin;

    [SerializeField] private Vector2 dragOffset = new Vector2(100, 100);
    public float dragSpeed = 2.0f;

    public float sensitivity = 2.0f;

    private Vector3 touchStartPos;
    private Vector2 turn = Vector2.zero;
    private bool isDragging = false;
    [SerializeField] private Vector2 rotX = new Vector2(-45f, 45f);
    [SerializeField] private Vector2 rotY = new Vector2(-45f, 45f);

    Quaternion firsCamPos;

    public CinemachineVirtualCamera tps, fps;

    GameObject arrow;
    private void Start()
    {
        firsCamPos = _FPScamera.transform.rotation;
        
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //AimStart();
            _animator.SetTrigger("aim");
            touchStartPos = Input.mousePosition;
            tps.Priority = 0;
            fps.Priority = 1;

            arrow = Instantiate(archerController.arrow, archerController.arrowSpawnPoint.position, Quaternion.identity, archerController.arrowSpawnPoint);

        }

        if (Input.GetMouseButton(0))
        {
            Shoot();
            Vector3 mouse = Input.mousePosition;
            //print(mouse - touchStartPos);
            //AimDrag();

            //_TPSCamera.transform.position = Vector3.Lerp(_TPSCamera.transform.position, _FPScamera.transform.position, 0.025f);
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            

            //target.transform.position += mousePos;

            //Quaternion targetRot = Quaternion.LookRotation(mousePos - _FPScamera.transform.position);
            Quaternion targetRot = Quaternion.LookRotation(mousePos - fps.transform.position);

            //_FPScamera.transform.rotation = Quaternion.Slerp(_FPScamera.transform.rotation, targetRot, sensitivity * Time.deltaTime);
            fps.transform.rotation = Quaternion.Slerp(_FPScamera.transform.rotation, targetRot, sensitivity * Time.deltaTime);

            LimitRot();
            //print(mousePos);

            
            //print(mouse);
            //virtualCam.transform.LookAt(mousePos * 10);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //AimEnd();
            
            Shoot();
            crosshair.color = Color.white;
            arrow.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 500f);
            arrow.transform.parent = null;
            _animator.SetTrigger("shoot");
            //ResetCamera();

            StartCoroutine(Reset());

            
            

            
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.2f);
        tps.Priority = 1;
        fps.Priority = 0;
        fps.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0;
        fps.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = 0;

        archerController.gameObject.transform.localRotation = archerController.oldRotation;
    }

    void LimitRot()
    {
        Vector3 euler = _FPScamera.transform.rotation.eulerAngles;
        euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
        euler.x = Mathf.Clamp(euler.x, rotX.x, rotX.y);
        euler.y = (euler.y > 180) ? euler.y - 360 : euler.y;
        euler.y = Mathf.Clamp(euler.y, rotY.x, rotY.y);
        _FPScamera.transform.rotation = Quaternion.Euler(euler);
    }

    void ResetCamera()
    {
        _FPScamera.transform.rotation = firsCamPos;
    }

    void Shoot()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(_FPScamera.transform.position, _FPScamera.transform.forward, out hit))
        //{
        //    Debug.Log(hit.transform.name);
        //    Debug.DrawRay(_FPScamera.transform.position, _FPScamera.transform.forward * 100f, Color.green);
        //}

        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit))
        {
            Debug.DrawRay(fps.transform.position, fps.transform.forward * 100f, Color.green);
            //Debug.Log(hit.transform.tag);
        }
        if (hit.transform.CompareTag("Enemy"))
            crosshair.color = Color.green;
        else
            crosshair.color = Color.white;

    }
    //void AimStart()
    //{
    //    fps.Priority = 11;
    //    tps.Priority = 10;
    //}

    void AimDrag()
    {
        
        //if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, 100.0f))
            //Debug.DrawRay(_camera.transform.position, _camera.transform.forward * 100.0f, Color.yellow);
    }


    //void AimEnd()
    //{
    //    fps.Priority = 10;
    //    tps.Priority = 11;
    //}

    
    //void HandleMouseInput()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        isDragging = true;
    //        touchStartPos = Input.mousePosition;
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isDragging = false;
    //    }

    //    if (isDragging)
    //    {
    //        Vector2 offset = Input.mousePosition - touchStartPos;
    //        Vector2 touchDelta = offset * sensitivity * Time.deltaTime;

    //        turn.x += touchDelta.x;
    //        turn.y += touchDelta.y;

    //        //print(_camera.transform.eulerAngles);


    //        print(_camera.transform.eulerAngles.x);
    //        //turn.x = Mathf.Clamp(turn.x, -60f, 60f);


    //        // Apply the rotation to your camera or GameObject.
    //        _camera.transform.Rotate(Vector3.up * turn.x);
    //        _camera.transform.Rotate(Vector3.left * turn.y);
    //        //if(_camera.transform.eulerAngles.x <)
    //        _camera.transform.eulerAngles = new Vector3(_camera.transform.eulerAngles.x, _camera.transform.eulerAngles.y, 0f);

    //        touchStartPos = Input.mousePosition;
    //    }
    //}

}
