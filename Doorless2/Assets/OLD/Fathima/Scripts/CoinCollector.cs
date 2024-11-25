using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public AudioClip coinSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject); // Destroy the coin after collection
        }
    }
}

