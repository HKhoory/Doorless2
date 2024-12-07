using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChaserController : MonoBehaviour
{
    public GameController gameController;

    private PlayerMover targetPlayer;
    private bool isTagging = false;
    public InputActionReference tagAction; // Correct Type

    private void OnEnable() => tagAction.action.Enable();
    private void OnDisable() => tagAction.action.Disable();

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

            if (tagAction.action.triggered) // Detect input from any bound device
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
