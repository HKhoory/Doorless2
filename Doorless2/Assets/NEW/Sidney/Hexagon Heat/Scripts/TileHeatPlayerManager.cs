using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHeatPlayerManager : MonoBehaviour
{
    public GameObject[] players; // Reference to player game objects

    void Update()
    {
        int alivePlayers = 0;

        foreach (GameObject player in players)
        {
            if (player != null) // Check if player is still alive
                alivePlayers++;
        }

        if (alivePlayers <= 1)
        {
            // Stop the game or trigger a game over
            StopGame();
        }
    }

    void StopGame()
    {
        // Code to stop the game, show game over, etc.
        Debug.Log("Game Over!");
        Time.timeScale = 0; // Freeze the game time (pause)
    }
}
