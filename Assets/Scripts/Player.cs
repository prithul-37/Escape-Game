using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public Animator animator;

    public float speed = 3f;

    public int level = 1;


    public bool RightGo = true;
    public bool LeftGo = false;
    public bool UpGo = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }



    // Update is called once per frame
    void Update()
    {
        //print(Go);
        /*animator.SetFloat("MoveSpeed", speed);
        rb.AddForce(Vector2.right * speed, ForceMode.Force);

        if(transform.position.x >= 9.5 && transform.position.x <=15.0)
            rb.AddForce(Vector2.up * 0.2f, ForceMode.Impulse);*/



        
        if(RightGo)
        {
            //print("Right");
            animator.SetTrigger("Running");
            this.transform.Translate(new Vector3(0,0,0.3f) * speed*Time.deltaTime);

            if (transform.position.x >= 9.5 && transform.position.x <= 15.0)
            {
                rb.AddForce(Vector2.up * 0.2f, ForceMode.Impulse);
                animator.SetBool("Running", false);

            }
                
        }
        else if(UpGo)
        {
            //print("Up");
            animator.SetTrigger("ClimbingLadder");
            this.transform.Translate(new Vector3(0, 0.3f, 0) * speed * Time.deltaTime);
        }
        
        else if(LeftGo)
        {
            //print("Left");
            animator.SetTrigger("Running");
            this.transform.Translate(new Vector3(0, 0, 0.3f) * speed * Time.deltaTime);
        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Ladder")
        {
            rb.useGravity = false;
            if(RightGo)
                transform.Rotate(new Vector3(0, -90, 0));
            if (LeftGo)
                transform.Rotate(new Vector3(0, 90, 0));
            UpGo = true;
            LeftGo = false;
            RightGo = false;
        }

        if (other.tag == "RightInd")
        {
            rb.useGravity = true;
            transform.Rotate(new Vector3(0, 90, 0));
            UpGo = false;
            LeftGo = false;
            RightGo = true;
        }

        if (other.tag == "LeftInd")
        {
            rb.useGravity = true;
            transform.Rotate(new Vector3(0, -90, 0));
            UpGo = false;
            LeftGo = true;
            RightGo = false;
        }

        //Finish
        if(other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            UpGo = false;
            LeftGo = false;
            RightGo = false;
            animator.SetTrigger("Victory");
        }
    }

}
