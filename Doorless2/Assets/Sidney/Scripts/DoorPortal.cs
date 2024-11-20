using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPortal : MonoBehaviour
{
    public string doorID; // Unique identifier for matching doors
    public Transform teleportLocation; // The location to teleport the player to
    public float teleportCooldown = 1.0f; // Cooldown in seconds

    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            Debug.Log("Player entered the trigger.");

            DoorPortal[] doors = FindObjectsOfType<DoorPortal>();
            foreach (DoorPortal door in doors)
            {
                if (door != this && door.doorID == this.doorID)
                {
                    if (door.teleportLocation != null)
                    {
                        // Disable CharacterController temporarily to allow direct position change
                        CharacterController controller = other.GetComponent<CharacterController>();
                        if (controller != null) controller.enabled = false;

                        // Teleport the player to the specified teleport location
                        other.transform.position = door.teleportLocation.position;

                        // Rotate the player on the Y axis
                        CameraHandle cameraHandle = other.GetComponentInChildren<CameraHandle>();
                        if (cameraHandle != null)
                        {
                            cameraHandle.SetPlayerRotation(door.teleportLocation.eulerAngles.y);
                        }

                        // Re-enable CharacterController after teleport
                        if (controller != null) controller.enabled = true;

                        // Start cooldown to prevent immediate re-teleporting
                        StartCoroutine(TeleportCooldown());

                        Debug.Log("Teleport successful.");
                        break;
                    }
                    else
                    {
                        Debug.Log("No teleport location assigned to matching door.");
                    }
                }
            }
        }
    }

    private IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(teleportCooldown);
        canTeleport = true;
    }
}
