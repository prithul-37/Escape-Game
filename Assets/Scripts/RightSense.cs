using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSense : MonoBehaviour
{
    public Player player;

    /*private void FixedUpdate()
    {
        player.RightGo = true;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            player.RightGo = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            player.RightGo = true;
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
