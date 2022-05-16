using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponents
{
   [SerializeField] private float walkSpeed = 4f;
   
   public float MoveSpeed { get; set; }
   public readonly int movingParameter = Animator.StringToHash("Moving");

   protected override void Start()
   {
       base.Start();
       MoveSpeed = walkSpeed;

   }

   protected override void HandleAbility()
   {

       base.HandleAbility();
       MoveCharacter();
       UpdateAnimations();
   }

   public void MoveCharacter()
   {
       Vector2 movement = new Vector2(horizontalInput, verticalInput);
       Vector2 movementNormalized = movement.normalized;
       Vector2 movementSpeed = movementNormalized * MoveSpeed;
       controller.SetMovement(movementSpeed);
   }

   private void UpdateAnimations()
   {
       if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
       {
           animator.SetBool(movingParameter, true);
       }
       else
       {
           animator.SetBool(movingParameter, false);
       }
   }

   public void ResetSpeed()
   {

       MoveSpeed = walkSpeed;
   }

}
