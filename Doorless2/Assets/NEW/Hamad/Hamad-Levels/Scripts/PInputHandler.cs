using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PInputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerMover _playerMover;
    

    private void Awake()
    {
        
        playerInput = GetComponent<PlayerInput>();
        var playerMovers = FindObjectsOfType<PlayerMover>();
        var index = playerInput.playerIndex;
        _playerMover = playerMovers.FirstOrDefault(m => m.GetPlayerIndex() == index);
       
    }

    public void OnMove(CallbackContext context)
    {
        if(_playerMover != null)
        {
            _playerMover.SetInputVector(context.ReadValue<Vector2>());
        }
    }
}
