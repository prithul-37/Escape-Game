using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Animator animator;
    public bool goForward = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {   if(goForward)
        {
            animator.SetBool("Running", true);
            rb.AddForce(transform.forward * 3f, ForceMode.Force);
        }
        else if (!goForward)
        {
            animator.SetBool("Running",false);
        }
    }
}
