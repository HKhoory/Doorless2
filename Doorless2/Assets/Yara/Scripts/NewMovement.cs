using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMovement : MonoBehaviour
{
    PlayerInputs playerInputs;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovingPressed;
    CharacterController characterController;

    private bool externalRotationSet = false;
    private float targetYRotation;

    [SerializeField] float rotationFactorPerFrame = 15.0f;
    [SerializeField] float movementSpeed = 5f;

    void Awake()
    {
        playerInputs = new PlayerInputs();
        characterController = GetComponent<CharacterController>();

        playerInputs.CharacterControls.Move.started += onMovementInput;
        playerInputs.CharacterControls.Move.canceled += onMovementInput;
    }

    void Update()
    {
        handleRotation();
        characterController.Move(currentMovement * movementSpeed * Time.fixedDeltaTime);

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovingPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        Quaternion currentRotation;
        Quaternion targetRotation;

        if (externalRotationSet) // Apply external rotation if set
        {
            transform.rotation = Quaternion.Euler(0, targetYRotation, 0);
            externalRotationSet = false; // Reset flag after applying
            return;
        }

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        currentRotation = transform.rotation;

        if (isMovingPressed)
        {
            targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }



    public void SetFacingDirectionY(float yRotation)
    {
        targetYRotation = yRotation;
        externalRotationSet = true;
    }

    void OnEnable()
    {
        playerInputs.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInputs.CharacterControls.Disable();
    }
}
