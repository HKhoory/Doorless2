using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include if you're using TextMeshPro for UI text
using UnityEngine.SceneManagement;

public class TileHeatPlayerManager : MonoBehaviour
{
    public GameObject[] players; // Reference to player game objects
    public TMP_Text winMessageText; // UI element to display win message

    private void Start()
    {
        winMessageText.gameObject.SetActive(false);
    }

    void Update()
    {
        int alivePlayers = 0;
        int lastPlayerIndex = -1; // Track the last remaining player index

        // Check how many players are still alive and track the last player's index
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null) // If player is still alive
            {
                alivePlayers++;
                lastPlayerIndex = i; // Update the index of the last remaining player
            }
        }

        // If only one player is left, stop the game and display the win message
        if (alivePlayers <= 1)
        {
            StopGame(lastPlayerIndex); // Pass the index of the last player
        }
    }

    void StopGame(int lastPlayerIndex)
    {
        // Stop the game and freeze time
        Debug.Log("Game Over!");
        winMessageText.gameObject.SetActive(true);

        SceneManager.LoadScene(0);


        // Display the win message based on the last player's index
        switch (lastPlayerIndex)
        {
            case 0:
                winMessageText.text = "Player 1 Wins!";
                break;
            case 1:
                winMessageText.text = "Player 2 Wins!";
                break;
            case 2:
                winMessageText.text = "Player 3 Wins!";
                break;
            case 3:
                winMessageText.text = "Player 4 Wins!";
                break;
            default:
                winMessageText.text = "No Winner!";
                break;
        }
    }
}
