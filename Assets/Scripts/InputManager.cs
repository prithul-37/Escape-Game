using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{

    bool click;
    public static Action<Vector3> Ontouching;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            click = true;
        }
        else if (Input.GetMouseButton(0) && click)
            Clicking();
        else if (Input.GetMouseButtonUp(0) && click) 
            click = false;

    }

    void Clicking()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50);

        if(hit.collider == null) return;

        Ontouching?.Invoke(hit.point);
    }
}
