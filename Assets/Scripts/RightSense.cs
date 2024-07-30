using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSense : MonoBehaviour
{
    public Player player;
    public Rigidbody rb;

    /*private void FixedUpdate()
    {
        player.RightGo = true;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            player.goForward = false;
            

        }
        else if(other.tag == "t")
        {
            rb.AddForce(transform.forward * -1f, ForceMode.Impulse);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            player.goForward = true;
        }
    }

    /*
        private void OnTriggerStay(Collider other)
        {
            //player.animator.SetTrigger("Grounded");
            //player.animator.SetFloat("MoveSpeed", 0);
            if (other.tag == "Ladder") ;

            else player.RightGo = false;
        }
        private void OnTriggerExit(Collider other)
        {
            print("Exit called");
            player.RightGo = true;
        }*/
}
