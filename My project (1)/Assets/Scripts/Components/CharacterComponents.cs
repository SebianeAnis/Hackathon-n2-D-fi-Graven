using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponents : MonoBehaviour
{

    protected float horizontalInput;
    protected float verticalInput; 

    protected CharacterController controller;
    protected CharacterMovement characterMovement;
    protected Animator animator;
    protected Character character;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        character = GetComponent<Character>();
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
    
        HandleAbility();

    }


    protected virtual void HandleAbility()
    {

        InternalInput();
        HandleInput();
    }

    protected virtual void HandleInput()
    {
        

    }

    protected virtual void InternalInput()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }


}
