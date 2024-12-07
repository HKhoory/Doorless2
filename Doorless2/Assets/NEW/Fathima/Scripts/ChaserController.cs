using System.Collections;
using UnityEngine;

public class ChaserController : MonoBehaviour
{
    public GameController gameController;
    public KeyCode tagKey = KeyCode.X;

    private PlayerMover targetPlayer;
    private bool isTagging = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTagging && other.TryGetComponent(out PlayerMover player) && player != gameController.chaser)
        {
            StartCoroutine(AttemptTag(player));
        }
    }

    private IEnumerator AttemptTag(PlayerMover player)
    {
        isTagging = true;
        targetPlayer = player;

        // Freeze both players
        gameController.chaser.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Show popup
        gameController.chasePopup.SetActive(true);

        // Wait 2 seconds for tagging
        float tagTimer = 2f;
        while (tagTimer > 0)
        {
            tagTimer -= Time.deltaTime;

            if (Input.GetKeyDown(tagKey))
            {
                // Tag successfully
                gameController.EliminatePlayer(player);
                break;
            }

            yield return null;
        }

        // Hide popup and resume gameplay
        gameController.chasePopup.SetActive(false);
        isTagging = false;
        targetPlayer = null;
    }
}
