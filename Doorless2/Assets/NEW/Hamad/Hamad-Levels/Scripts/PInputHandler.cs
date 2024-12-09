using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System;

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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_playerMover == null)
        {
            Debug.Log("searching for player gameobjects");
            Awake();
            
        }
    }

    public void OnMove(CallbackContext context)
    {
        if(_playerMover != null)
        {
            _playerMover.SetInputVector(context.ReadValue<Vector2>());
        }
        
    }

    
}
