using UnityEngine;
using TMPro;  
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public List<PlayerMover> players = new List<PlayerMover>();
    public GameObject chaserIndicatorPrefab; // Prefab for chaser indicator (e.g., above their head)
    public GameObject chasePopup; // UI popup to show "You are the Chaser" or "Press X to Tag"
    public TextMeshProUGUI timerText; // UI TextMeshPro reference to display the countdown timer
    public float gameDuration = 150f; // 2:30 minutes in seconds

    [HideInInspector] public PlayerMover chaser;

    private float timer;
    private bool gameRunning = false;
    [Header("Map Settings")]
    public GameObject mapPrefab;  // Assign the map prefab in the Inspector
    public Vector3 shrinkAmount = new Vector3(1f, 0, 1f);  // Amount to shrink on each interval
    public Vector3 minSize = new Vector3(5f, 0, 5f);  // Minimum map size

    private void Start()
    {
        AssignRoles();
        StartGame();

        // Start shrinking map every 30 seconds
        InvokeRepeating(nameof(ShrinkMap), 30f, 30f);
    }

    private void ShrinkMap()
    {
        if (mapPrefab.transform.localScale.x > minSize.x && mapPrefab.transform.localScale.z > minSize.z)
        {
            mapPrefab.transform.localScale -= shrinkAmount;
        }
        else
        {
            CancelInvoke(nameof(ShrinkMap));  // Stop shrinking when minimum size is reached
        }
    }


    private void Update()
    {
        if (!gameRunning) return;

        timer -= Time.deltaTime;
        // Display the timer using TextMeshPro
        timerText.text = $"{Mathf.FloorToInt(timer / 60)}:{Mathf.FloorToInt(timer % 60):00}";

        if (timer <= 0)
        {
            EndGame();
        }
    }

    private void AssignRoles()
    {
        int randomIndex = Random.Range(0, players.Count);
        chaser = players[randomIndex];

        foreach (var player in players)
        {
            if (player == chaser)
            {
                // Boost the chaser's speed using reflection
                player.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                      ?.SetValue(player, 6.5f);

                // Show chaser indicator
                Vector3 indicatorPosition = player.transform.position + new Vector3(0, 1.5f, 0);
                Instantiate(chaserIndicatorPrefab, indicatorPosition, Quaternion.identity, player.transform);

                chasePopup.SetActive(true); // "You are the Chaser" popup
            }
            else
            {
                // Reset speed of other players
                player.GetType().GetField("speed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                      ?.SetValue(player, 5f);
            }
        }
    }



    private void StartGame()
    {
        timer = gameDuration;
        gameRunning = true;
    }

    private void EndGame()
    {
        gameRunning = false;
        chasePopup.SetActive(false);

        int remainingPlayers = players.Count - 1; // Chaser doesn't count
        if (remainingPlayers > 0)
        {
            Debug.Log("Team wins!");
        }
        else
        {
            Debug.Log("Chaser wins!");
        }
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
