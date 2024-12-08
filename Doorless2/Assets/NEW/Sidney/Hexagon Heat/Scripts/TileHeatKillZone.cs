using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHeatKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destroy the player object
        }
    }
}
