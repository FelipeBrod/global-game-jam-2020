using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{

    [SerializeField] Transform positionToTransport;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Instantiate(other.GetComponent<Object>().prefabObj, positionToTransport.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
