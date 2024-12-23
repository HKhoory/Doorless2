using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{

    [SerializeField] private Rigidbody _rb;
    //[SerializeField] private CharacterController _cc;

    [SerializeField] private int playerIndex;

    [SerializeField] public float speed;
    
    [SerializeField] private Vector2 movementInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(movementInput.x * speed, _rb.velocity.y , movementInput.y * speed);
        //_cc.Move(new Vector3(movementInput.x * speed, 0, movementInput.y * speed));
    }


    //public void OnMove(InputAction.CallbackContext x) => movementInput = x.ReadValue<Vector2>();

    public int GetPlayerIndex() { return playerIndex; }

    public void SetInputVector(Vector2 dir)
    {
        movementInput = dir;
    }

    //public void SetTagButton(Button dir)
    //{
    //    movementInput = dir;
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            Destroy(gameObject);
        }
    }

}
