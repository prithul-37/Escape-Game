using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float thrust = 0.2f;

   

    public float speed = 0.1f;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }



    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.right * speed, ForceMode.Force);


        if(transform.position.x >= 9.5 && transform.position.x <=15.0)
            rb.AddForce(Vector2.up * 0.2f, ForceMode.Impulse);
        
    }


}
