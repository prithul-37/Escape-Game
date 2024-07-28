using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float thrust = 0.2f;

    public int pointSize = 2;
    public float[,] points;

    public float speed =0.1f;

    public int level = 1;
    Vector2 direction;

    int CheckPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = new float[pointSize,2];

        if(level==1)
        {
            points[0, 0] = 9f;
            points[0, 1] = -10.5f;

            points[1, 0] = 18f;
            points[1, 1] = -4.5f;

        }
    }



    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(points[CheckPoint, 0] - transform.position.x, points[CheckPoint, 1] - transform.position.y);
        direction.Normalize();
        rb.transform.Translate(direction * speed);

        float dis = (points[CheckPoint, 0] - transform.position.x) * (points[CheckPoint, 0] - transform.position.x) + (points[CheckPoint, 1] - transform.position.y) * (points[CheckPoint, 1] - transform.position.y);

        if (dis<3)
        {
            if(CheckPoint< pointSize)
                CheckPoint++;
        }
        
        //print(CheckPoint);

        /*if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector2.left * speed);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector2.right * speed);*/
    }

}
