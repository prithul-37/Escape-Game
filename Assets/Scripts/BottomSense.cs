using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomSense : MonoBehaviour
{
    public Player player;


    private void OnTriggerEnter(Collider other)
    {
        //print("hi");
        player.animator.SetTrigger("Grounded");
    }

}
