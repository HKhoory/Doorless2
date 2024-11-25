using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Mirage : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform pointOrigin;

    public float speed = 2f;

    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        pointOrigin = pointA.transform;  
    }

    void Update()
    {
        Vector3 direction = (pointOrigin.position - transform.position);

        rb.velocity = direction * speed;

        if (Vector3.Distance(transform.position, pointOrigin.position) < 0.5f)
        { 
            pointOrigin = (pointOrigin == pointA.transform) ? pointB.transform : pointA.transform;
        }
          
        
    }
}
