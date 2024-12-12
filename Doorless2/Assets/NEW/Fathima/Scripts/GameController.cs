using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionReference tagActionReference;  // Reference for the "Tag" action
    public List<PlayerMover> players = new List<PlayerMover>();
    public GameObject chaserIndicatorPrefab;
    public GameObject chasePopup; // Popup for chaser announcement
    public GameObject tagPopup; // Popup for tagging announcement
    public GameObject winPopup; // Popup for endgame results
    public TMP_Text timerText; // Timer text
    public TMP_Text tagPopupText; // Tagging popup text
    public TMP_Text winPopupText; // Endgame popup text
    public TMP_Text chasePopupText;

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
        chasePopupText.text = $"Player {chaser.GetPlayerIndex()} is the Chaser!";
        Invoke(nameof(HideChasePopup), 3f);
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
        chasePopup.SetActive(false);

        if (players.Count <= 1) // Only chaser remains
        {
            winPopup.SetActive(true);
            winPopupText.text = "Chaser Wins!";
            Debug.Log("Chaser wins!");
        }
        else
        {
            winPopup.SetActive(true);
            winPopupText.text = "Players Win!";
            Debug.Log("Players win!");
        }
    }


    public void OnPlayerCollision(PlayerMover chasedPlayer)
    {
        if (chasedPlayer == chaser || !gameRunning || isTagging) return;

        // Start tagging coroutine
        isTagging = true;
        StartCoroutine(HandleTagging(chasedPlayer));
    }

    private IEnumerator HandleTagging(PlayerMover chasedPlayer)
    {
        tagPopup.SetActive(true);
        tagPopupText.text = "Press X to Tag!";

        // Freeze both players
        DisablePlayerMovement(chaser, chasedPlayer);

        float tagTimer = 2f;
        bool tagged = false;

        while (tagTimer > 0)
        {
            tagTimer -= Time.deltaTime;

            // Check if X is pressed (Controller or Keyboard)
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.X))
            {
                tagged = true;
                break;
            }

            yield return null;
        }

        // Hide the tag popup
        tagPopup.SetActive(false);

        // Enable player movement again
        EnablePlayerMovement(chaser, chasedPlayer);

        if (tagged)
        {
            Debug.Log($"{chasedPlayer.name} has been tagged!");
            tagPopup.SetActive(true);
            tagPopupText.text = $"{chasedPlayer.name} has been tagged!";
            EliminatePlayer(chasedPlayer);

            // Wait before hiding the popup
            yield return new WaitForSeconds(2f);
            tagPopup.SetActive(false);
        }

        // Reset tagging status
        isTagging = false;
    }



    // Helper Method to Disable Player Movement
    private void DisablePlayerMovement(params PlayerMover[] players)
    {
        foreach (var p in players)
        {
            var input = p.GetComponent<PlayerInput>();
            if (input != null)
            {
                input.enabled = false;
            }
            else
            {
                Debug.LogWarning($"Player {p.name} is missing PlayerInput!");
            }
        }
    }

    // Helper Method to Enable Player Movement
    private void EnablePlayerMovement(params PlayerMover[] players)
    {
        foreach (var p in players)
        {
            var input = p.GetComponent<PlayerInput>();
            if (input != null)
            {
                input.enabled = true;
            }
            else
            {
                Debug.LogWarning($"Player {p.name} is missing PlayerInput!");
            }
        }
    }

    // Eliminate the Tagged Player
    public void EliminatePlayer(PlayerMover player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            Destroy(player.gameObject);

            // Check if only the chaser remains
            if (players.Count == 1 && players.Contains(chaser))
            {
                EndGame();
            }
        }
    }

    private void CompleteTagging()
    {
        tagPopup.SetActive(false);

        if (chaser != null) chaser.GetComponent<PlayerInput>().enabled = true;

        isTagging = false;
    }
}
