using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;  // Speed at which the block floats down
    [SerializeField] private float destroyHeight = -5f;  // Height at which the block is destroyed (once it falls out of view)

    private Rigidbody rb;

    private void Awake()
    {
        // Get the Rigidbody component attached to the red block
        rb = GetComponent<Rigidbody>();
        // Ensure gravity is disabled
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        // Apply a constant downward force to simulate the falling/block floating effect
        rb.velocity = new Vector3(0, -fallSpeed, 0);

        // Destroy the block if it falls below a certain height (off-screen)
        if (transform.position.y < destroyHeight)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the block collided with a player
        if (other.CompareTag("Player"))
        {
            // Destroy the block when it hits the player
            Destroy(gameObject);
        }
    }
}
