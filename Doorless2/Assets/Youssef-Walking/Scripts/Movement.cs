using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    private Transform mainCam;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        DirectionMovement();
    }

    // calculates movemnt direction 
    public void DirectionMovement()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        direction = (mainCam.forward * vAxis) + (mainCam.right * hAxis);
        direction.Normalize();
        direction.y = 0; // Prevent vertical movement from player input

        // Set the horizontal velocity based on input
        rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
    }
}