using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    // verti and hori input values
    public float verticalAxis;
    public float horizontalAxis;

    // mouse movemnt for camera rotation
    private float MouseX;
    private float MouseY;

    void Start()
    {
        // Locks the cursor in the middle and makes the mouse cursor disappear
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Call Moving function
        Moving();

        // Get mouse movements 
        MouseX += Input.GetAxis("Mouse X");
        MouseY += Input.GetAxis("Mouse Y");

        // Limits camera movement angle
        MouseY = Mathf.Clamp(MouseY, -45, 60);

        // Handles rotation of the player and the camera
        HandleCameraRotation();
    }

    private void Moving()
    {
        // Vertical axis for W and S keys or up and down arrows
        // Horizontal axis for A and D keys or left and right arrows
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
    }

    private void HandleCameraRotation()
    {
        // Rotate the player based on mouse input
        transform.rotation = Quaternion.Euler(0, MouseX, 0); // Rotate player horizontally

        // Rotate the camera based on mouse input
        Camera.main.transform.localRotation = Quaternion.Euler(-MouseY, 0, 0); // Rotate camera vertically
    }

    // Call this function to set the rotation from external scripts
    public void SetPlayerRotation(float yRotation)
    {
        MouseX = yRotation; // Update the horizontal rotation based on teleport
        transform.rotation = Quaternion.Euler(0, MouseX, 0); // Set the player's rotation
    }
}
