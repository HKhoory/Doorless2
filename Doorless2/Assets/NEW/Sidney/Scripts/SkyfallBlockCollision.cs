using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyfallBlockCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as a "RedBlock"
        if (other.CompareTag("RedBlock"))
        {
            // If a red block hits the player, destroy the player
            Destroy(gameObject);
        }
    }
}
