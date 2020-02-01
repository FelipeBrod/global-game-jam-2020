using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    [SerializeField] PlayerController player1;
    [SerializeField] PlayerController player2;

    public static CameraController _instance;
    public List<GameObject> virtualCameras;

    PlayerController player;
    bool camRotate = false;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(gameObject);
        }

        player = FindObjectOfType<PlayerController>();

        virtualCameras.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            virtualCameras.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update() {
        if(Vector3.Distance(player1.transform.position, player2.transform.position) < 30 && !camRotate)
        {
            RotateCam();
            camRotate = true;
        }
    }

    void RotateCam()
    {
        StartCoroutine(RotateMe(player1.playerCam, Vector3.forward * 0,3f));
        StartCoroutine(RotateMe(player2.playerCam, Vector3.forward * 0,3f));
    }

     IEnumerator RotateMe(GameObject obj, Vector3 byAngles, float inTime) 
     {    

        var fromAngle = obj.transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
             obj.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
             yield return null;
        }
     }



    public void TransitionTo(GameObject cameraToTransitionTo)
    {
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            if (virtualCameras[i] == cameraToTransitionTo)
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
            else
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 5;
            }
        }

    }

    public void TransitionTo(GameObject cameraToTransitionTo, GameObject cameraOut, float timeToWait)
    {
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            if (virtualCameras[i] == cameraToTransitionTo)
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
            else
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 5;
            }
        }
        StartCoroutine(TransitionBack(cameraOut,timeToWait));
    }

    public IEnumerator TransitionBack(GameObject cameraOut, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            if (virtualCameras[i] == cameraOut)
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
            else
            {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 5;
            }
        }
    }



}
