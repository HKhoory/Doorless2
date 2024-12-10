using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject redBlockPrefab;   // Reference to the red block prefab
    [SerializeField] private float spawnInterval = 2f;    // Initial time interval between random block spawns
    [SerializeField] private float spawnRangeX = 10f;     // X-range in which the blocks can spawn
    [SerializeField] private float spawnRangeZ = 10f;     // Z-range in which the blocks can spawn
    [SerializeField] private float playerBlockSpawnInterval = 5f; // Initial interval for spawning block above players
    [SerializeField] private float blockHeightAbovePlayer = 2f; // Height above player to spawn the block

    public TMP_Text countdownText; // UI element for the countdown timer
    public TMP_Text gameTimerText; // UI element for the game timer
    public TMP_Text winMessageText; // UI element for the winner announcement

    private List<Transform> players = new List<Transform>(); // List to store all player transforms
    private float gameTime = 0f; // Elapsed time since the game started
    private bool gameStarted = false; // Flag to track if the game has started
    private float difficultyTimer = 0f; // Timer to track when to increase difficulty
    private float difficultyInterval = 6f; // Time interval (in seconds) for difficulty increases

    private void Start()
    {
        gameTimerText.gameObject.SetActive(false);
        winMessageText.gameObject.SetActive(false);

        // Start the countdown coroutine
        StartCoroutine(GameStartCountdown());
    }

    private void Update()
    {
        // Continuously update the list of players in the scene
        UpdatePlayerList();

        if (gameStarted)
        {
            // Update game timer
            gameTime += Time.deltaTime;
            gameTimerText.text = $"Time: {gameTime:F1}s";

            // Increment difficulty timer
            difficultyTimer += Time.deltaTime;
            if (difficultyTimer >= difficultyInterval)
            {
                IncreaseDifficulty();
                difficultyTimer = 0f; // Reset the difficulty timer
            }

            // Check if only one player remains
            if (players.Count <= 1)
            {
                StopGame();
            }
        }
    }

    private void UpdatePlayerList()
    {
        // Find all players in the scene with the "Player" tag
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Add all players to the list, ensuring no duplicates
        players.Clear(); // Clear the list to avoid duplicates
        foreach (GameObject player in playerObjects)
        {
            if (!players.Contains(player.transform))
            {
                players.Add(player.transform);
            }
        }
    }

    private void SpawnBlock()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 spawnPos = new Vector3(spawnPosX, transform.position.y, spawnPosZ);

        Instantiate(redBlockPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnBlockAbovePlayers()
    {
        foreach (Transform player in players)
        {
            Vector3 spawnPosAbovePlayer = new Vector3(player.position.x, player.position.y + blockHeightAbovePlayer, player.position.z);
            Instantiate(redBlockPrefab, spawnPosAbovePlayer, Quaternion.identity);
        }
    }

    private IEnumerator GameStartCountdown()
    {
        int countdown = 5; // Start countdown from 5
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // Hide the countdown text and start the game
        countdownText.gameObject.SetActive(false);
        gameStarted = true;
        gameTimerText.gameObject.SetActive(true);

        // Start spawning blocks
        InvokeRepeating("SpawnBlock", 0f, spawnInterval);
        InvokeRepeating("SpawnBlockAbovePlayers", 0f, playerBlockSpawnInterval);
    }

    private void IncreaseDifficulty()
    {
        // Decrease spawn intervals
        spawnInterval = Mathf.Max(0.01f, spawnInterval - 0.1f); // Minimum interval of 0.5 seconds
        playerBlockSpawnInterval = Mathf.Max(0.05f, playerBlockSpawnInterval - 0.2f); // Minimum interval of 1 second

        // Restart spawning blocks with new intervals
        CancelInvoke();
        InvokeRepeating("SpawnBlock", 0f, spawnInterval);
        InvokeRepeating("SpawnBlockAbovePlayers", 0f, playerBlockSpawnInterval);
    }

    private void StopGame()
    {
        gameStarted = false; // Stop the game
        Time.timeScale = 0; // Pause the game

        if (players.Count == 1)
        {
            // Display winner message based on the last player
            string winner = players[0].name; // Assumes the player GameObject's name represents the player
            winMessageText.text = $"{winner} Wins!";
        }
        else
        {
            winMessageText.text = "No Winner!";
        }

        winMessageText.gameObject.SetActive(true);
    }
}
