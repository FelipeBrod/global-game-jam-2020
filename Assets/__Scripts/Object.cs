using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{

    Rigidbody myRigidbody;
    bool beingTeleported = false;

    private void Start() {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
    
        if(other.CompareTag("Portal") && !beingTeleported)
        {
            myRigidbody.useGravity = false;
            transform.position = other.GetComponent<PortalSystem>().positionToTransport.position;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Portal"))
        {
            myRigidbody.useGravity = true;
            beingTeleported = true;
            StartCoroutine(WaitToTeleport());
        }        
    }

    IEnumerator WaitToTeleport()
    {
        yield return new WaitForSeconds(.1f);
        beingTeleported = false;
    }

}
