using UnityEngine;

public class TagCollisionHandler : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure one is the chaser and the other is a player
        if (gameController == null || !gameController.IsGameRunning()) return;

        if (other.CompareTag("Player") && gameController.IsChaser(gameObject))
        {
            // Notify GameController of collision
            gameController.OnPlayerCollision(other.GetComponent<PlayerMover>());
        }
    }
}
