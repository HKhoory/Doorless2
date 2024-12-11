using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameController : MonoBehaviour
{
    public List<PlayerMover> players = new List<PlayerMover>();
    public GameObject chaserIndicatorPrefab;
    public GameObject chasePopup; // Popup for chaser announcement
    public GameObject tagPopup; // Popup for tagging announcement
    public GameObject winPopup; // Popup for endgame results
    public TMP_Text timerText; // Timer text
    public TMP_Text tagPopupText; // Tagging popup text
    public TMP_Text winPopupText; // Endgame popup text

    public float gameDuration = 150f; // 2:30 minutes
    public float tagTimeWindow = 2f; // Time window for tagging
    public float chaserSpeedBoost = 2f;

    public PlayerMover chaser;
    private float timer;
    private bool gameRunning = false;
    private bool isTagging = false;

    // Check if the current game is running
    public bool IsGameRunning() => gameRunning;

    // Check if a player is the chaser
    public bool IsChaser(GameObject player) => chaser != null && chaser.gameObject == player;

   

    private void Start()
    {
        AssignRoles();
        StartGame();
    }

    private void Update()
    {
        if (!gameRunning) return;

        // Update Timer
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = $"{minutes}:{seconds:00}";

        // Timer End Logic
        if (timer <= 0)
        {
            timer = 0; // Clamp to 0
            EndGame();
        }
    }

    private void AssignRoles()
    {
        int randomIndex = Random.Range(0, players.Count);
        chaser = players[randomIndex];

        // Set Chaser Indicator
        Instantiate(chaserIndicatorPrefab, chaser.transform.position + Vector3.up * 2, Quaternion.identity, chaser.transform);

        // Adjust Chaser Speed
        chaser.GetComponent<Rigidbody>().velocity = Vector3.zero;
        chaser.GetComponent<PlayerMover>().SetInputVector(Vector2.zero); // Reset input
        chaser.speed += chaserSpeedBoost;

        // Display Popup
        chasePopup.SetActive(true);
        Invoke(nameof(HideChasePopup), 2f);
    }

    private void HideChasePopup() => chasePopup.SetActive(false);

    private void StartGame()
    {
        timer = gameDuration;
        gameRunning = true;
    }

    private void EndGame()
    {
        gameRunning = false;

        // Determine Winner
        int remainingPlayers = players.Count - 1; // Exclude chaser
        if (remainingPlayers > 0)
        {
            winPopupText.text = "Players Win!";
        }
        else
        {
            winPopupText.text = "Chaser Wins!";
        }

        winPopup.SetActive(true);
    }

    public void OnPlayerCollision(PlayerMover player)
    {
        if (!gameRunning || isTagging || player == chaser) return;

        isTagging = true;

        // Show tag popup
        tagPopup.SetActive(true);
        tagPopupText.text = $"Player {player.GetPlayerIndex()} has been tagged!";

        // Wait for the tagging window
        Invoke(nameof(CompleteTagging), tagTimeWindow);

        // Disable Player Movement
        player.GetComponent<PlayerInput>().enabled = false;
        chaser.GetComponent<PlayerInput>().enabled = false;
    }

    private void CompleteTagging()
    {
        tagPopup.SetActive(false);

        if (chaser != null) chaser.GetComponent<PlayerInput>().enabled = true;

        isTagging = false;
    }

    public void EliminatePlayer(PlayerMover player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            Destroy(player.gameObject);
        }
    }
}
