using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject redBlockPrefab;   // Reference to the red block prefab
    [SerializeField] private float spawnInterval = 2f;    // Time interval between random block spawns
    [SerializeField] private float spawnRangeX = 10f;     // X-range in which the blocks can spawn
    [SerializeField] private float spawnRangeZ = 10f;     // Z-range in which the blocks can spawn
    [SerializeField] private float playerBlockSpawnInterval = 5f; // Time interval for spawning block above players
    [SerializeField] private float blockHeightAbovePlayer = 2f; // Height above player to spawn the block

    private List<Transform> players = new List<Transform>(); // List to store all player transforms

    private void Start()
    {
        // Start spawning blocks after the game begins
        InvokeRepeating("SpawnBlock", 1f, spawnInterval);   // Start spawning random blocks
        InvokeRepeating("SpawnBlockAbovePlayers", 1f, playerBlockSpawnInterval);   // Start spawning blocks above each player
    }

    private void Update()
    {
        // Continuously update the list of players in the scene
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        // Find all players in the scene with the "Player" tag
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Add all players to the list, ensuring no duplicates
        players.Clear(); // Clear the list to avoid duplicates
        foreach (GameObject player in playerObjects)
        {
            // Ensure no duplicate players are added
            if (!players.Contains(player.transform))
            {
                players.Add(player.transform);
            }
        }
    }

    private void SpawnBlock()
    {
        // Randomize the spawn position along the X and Z axes within their respective ranges
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ); // Random Z value
        Vector3 spawnPos = new Vector3(spawnPosX, transform.position.y, spawnPosZ); // Keep Y fixed at the spawner's Y position

        // Instantiate the red block prefab at the randomized position
        Instantiate(redBlockPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnBlockAbovePlayers()
    {
        foreach (Transform player in players)
        {
            // Get the current position of the player every time this method is called
            Vector3 spawnPosAbovePlayer = new Vector3(player.position.x, player.position.y + blockHeightAbovePlayer, player.position.z);

            // Instantiate the red block prefab above the player (this will now follow the players' movement)
            Instantiate(redBlockPrefab, spawnPosAbovePlayer, Quaternion.identity);
        }
    }
}
