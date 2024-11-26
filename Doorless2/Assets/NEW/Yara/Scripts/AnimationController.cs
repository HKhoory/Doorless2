 using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    bool isMovingLeft;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true); 
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash , false); 
        }
        
    }
    
    
   
}
