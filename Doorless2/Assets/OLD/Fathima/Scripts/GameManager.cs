using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public AudioClip winSound;
    public AudioClip coinSound;
    private AudioSource audioSource;

    private int coinsCollected = 0;
    public int totalCoins = 5;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (winPanel != null)
        {
            winPanel.SetActive(false); // Ensure panel is inactive at start
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
            else
            {
                Debug.LogWarning("Missing coin sound!");
            }

            Destroy(other.gameObject); // Destroy the coin
            coinsCollected++;

            // Check if win condition is met
            if (coinsCollected >= totalCoins)
            {
                TriggerWinCondition();
            }
        }
    }

    public void TriggerWinCondition()
    {
        // Play win sound
        audioSource.PlayOneShot(winSound);

        // Display win panel
        winPanel.SetActive(true);

    }
}