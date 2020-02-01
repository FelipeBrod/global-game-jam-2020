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
    public Camera cameraP1;
    public Camera cameraP2;

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

        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(ChangeRect(cameraP1, 3.0f));
            StartCoroutine(ChangeRect(cameraP2, 3.0f));
        }
    }

    IEnumerator ChangeRect(Camera obj, float time) 
    {
        float temp = obj.rect.x;
        if (temp > 0)
        {
            for (var i = temp; i >= 0; i -= Time.deltaTime/time) 
            {
                obj.rect = new Rect(i, 0, 1.0f, 1.0f);
                yield return null;
            }
            obj.rect = new Rect(0,0,1.0f,1.0f);
        }
        else
        {
            for (var i = temp; i >= -1; i -= Time.deltaTime/time) 
            {
                obj.rect = new Rect(i, 0, 1.0f, 1.0f);
                yield return null;
            }
        }
        
    }
    void RotateCam()
    {
        StartCoroutine(RotateMe(player1.playerCam, Vector3.forward * 0,3f));
        StartCoroutine(RotateMe(player2.playerCam, Vector3.forward * 0,3f));
    }

     IEnumerator RotateMe(GameObject obj, Vector3 byAngles, float inTime) 
     {    
         Debug.Log("Rodou");
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
