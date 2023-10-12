using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public ArcherController archerController;
    
    [SerializeField] private Animator _animator;
    //[SerializeField] Camera _FPScamera, _TPSCamera;
    public Image crosshair;
    public GameManager gameManager;

    public float dragSpeed = 2.0f;

    public float sensitivity = 2.0f;

    private Vector3 touchStartPos;
    [SerializeField] private Vector2 rotX = new Vector2(-45f, 45f);
    [SerializeField] private Vector2 rotY = new Vector2(-45f, 45f);

    Quaternion firsCamPos;

    public CinemachineVirtualCamera tps, fps;

    GameObject arrow;
    public int arrowCount;
    private void Start()
    {
        //firsCamPos = _FPScamera.transform.rotation;
        gameManager.SetArrowText(arrowCount);
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
            
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            Quaternion targetRot = Quaternion.LookRotation(mousePos - fps.transform.position);

            //fps.transform.rotation = Quaternion.Slerp(_FPScamera.transform.rotation, targetRot, sensitivity * Time.deltaTime);

            LimitRot();

        }

        if (Input.GetMouseButtonUp(0))
        {
            arrowCount--;
            gameManager.SetArrowText(arrowCount);
            
            
            Shoot();
            crosshair.color = Color.white;
            arrow.GetComponent<TrailRenderer>().enabled = true;
            arrow.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 500f);
            arrow.transform.parent = null;
            _animator.SetTrigger("shoot");
            //ResetCamera();

            StartCoroutine(Reset());            
        }
    }

    IEnumerator Reset()
    {

        yield return new WaitForSeconds(1f);
        tps.Priority = 1;
        fps.Priority = 0;
        fps.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0;
        fps.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = 0;

        archerController.gameObject.transform.localRotation = archerController.oldRotation;
    }

    void LimitRot()
    {
        //Vector3 euler = _FPScamera.transform.rotation.eulerAngles;
        //euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
        //euler.x = Mathf.Clamp(euler.x, rotX.x, rotX.y);
        //euler.y = (euler.y > 180) ? euler.y - 360 : euler.y;
        //euler.y = Mathf.Clamp(euler.y, rotY.x, rotY.y);
        //_FPScamera.transform.rotation = Quaternion.Euler(euler);
    }

    void ResetCamera()
    {
        //_FPScamera.transform.rotation = firsCamPos;
    }

    void Shoot()
    {
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



}
