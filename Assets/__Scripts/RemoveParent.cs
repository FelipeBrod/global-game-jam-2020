using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveParent : MonoBehaviour
{

    [SerializeField] Transform child;

   void RemoveChild()
    {
        var newObj = Instantiate(child.gameObject, child.position , Quaternion.identity);
        newObj.GetComponent<BoxCollider>().enabled = true;
        child.transform.SetParent(null);
        Destroy(child.gameObject);
        Destroy(gameObject);
    }

}
