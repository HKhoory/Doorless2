using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;  // Speed at which the block falls

    private void Update()
    {
        // Move the block down every frame
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the block hits a player, destroy the player and the block
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);  // Destroy the player
            Destroy(gameObject);        // Destroy the block
        }
    }
}
