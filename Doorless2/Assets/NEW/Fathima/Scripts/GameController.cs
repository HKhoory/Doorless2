using UnityEngine;
using TMPro;  
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public List<PlayerMover> players = new List<PlayerMover>();
    public GameObject chaserIndicatorPrefab; // Prefab for the indicator
    public GameObject chasePopup;            // Popup UI panel
    public TMP_Text popupText;               // Text inside the popup
    public TMP_Text timerText;               // Timer display
    public float gameDuration = 150f;        // 2:30 minutes

    [HideInInspector] public PlayerMover chaser;

    private float timer;
    private bool gameRunning = false;

    private void Start()
    {
        AssignRoles();
        StartGame();
    }

    private void Update()
    {
        if (!gameRunning) return;

        // Timer Countdown
        timer -= Time.deltaTime;
        timerText.text = $"Time Left: {Mathf.FloorToInt(timer / 60)}:{Mathf.FloorToInt(timer % 60):00}";

        if (timer <= 0)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Assigns the chaser based on a random player index.
    /// </summary>
    private void AssignRoles()
    {
        if (players.Count == 0)
        {
            Debug.LogError("No players found! Please assign players to the GameController.");
            return;
        }

        // Randomly choose the chaser
        int randomIndex = Random.Range(0, players.Count);
        chaser = players[randomIndex];

        foreach (var player in players)
        {
            if (player == chaser)
            {
                // Assign chaser speed boost
                player.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(player, 6.5f);

                // Spawn chaser indicator slightly above player
                Vector3 indicatorPosition = player.transform.position + new Vector3(0, 1.5f, 0);
                Instantiate(chaserIndicatorPrefab, indicatorPosition, Quaternion.identity, player.transform);

                // Show popup text for the chaser
                popupText.text = $"Player {player.GetPlayerIndex() + 1} is the Chaser!";
                chasePopup.SetActive(true);
            }
            else
            {
                // Reset other players' speed
                player.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(player, 5f);
            }
        }

        // Hide popup after 3 seconds
        Invoke(nameof(HidePopup), 3f);
    }

    /// <summary>
    /// Starts the game and the timer countdown.
    /// </summary>
    private void StartGame()
    {
        timer = gameDuration;
        gameRunning = true;
    }

    /// <summary>
    /// Ends the game based on remaining players.
    /// </summary>
    private void EndGame()
    {
        gameRunning = false;
        chasePopup.SetActive(false);

        int remainingPlayers = players.Count - 1;  // Exclude the chaser
        if (remainingPlayers > 0)
        {
            Debug.Log("Team wins!");
        }
        else
        {
            Debug.Log("Chaser wins!");
        }
    }

    /// <summary>
    /// Hides the popup after displaying the chaser announcement.
    /// </summary>
    private void HidePopup()
    {
        chasePopup.SetActive(false);
    }

    /// <summary>
    /// Removes a player from the game.
    /// </summary>
    public void EliminatePlayer(PlayerMover player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            Destroy(player.gameObject);
        }
    }
}
