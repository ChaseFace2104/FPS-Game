using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed;
    public bool thing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!thing)
        {
            transform.position = Vector3.MoveTowards(transform.position, point2.position, speed);
            if (Vector3.Distance(transform.position, point2.position) < 0.01)
            {
                thing = !thing;
            }
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed);
            if (Vector3.Distance(transform.position, point1.position) < 0.01)
            {
                thing = !thing;
            }
        }
    }
}
