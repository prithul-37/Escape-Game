using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [SerializeField] private float force;
    private void OnTriggerEnter(Collider other)
    {   
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0)*force,ForceMode.Impulse);
        }
    }
}
