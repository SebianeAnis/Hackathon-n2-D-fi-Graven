using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterComponents
{
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashDuration = 0.5f;

    private bool isDashing;
    private float dashTimer;
    private Vector2 dashOrigin;
    private Vector2 dashDestination;
    private Vector2 newPosition;
    
    protected override void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Dash();

        }

    }

    protected override async void HandleAbility()
    {
        base.HandleAbility();

        if (isDashing)
        {
            if (dashTimer < dashDuration)
            {

                newPosition = Vector2.Lerp(a: dashOrigin, b: dashDestination, t: dashTimer / dashDuration);
                controller.MovePosition(newPosition);
                dashTimer += Time.deltaTime; 

            }
            else 
            {
                StopDash();
            }

        }

    }


    private void Dash()
    {

        isDashing = true;
        dashTimer = 0f;
        controller.NormalMovement = false;
        dashOrigin = transform.position;

        dashDestination = transform.position + (Vector3) controller.CurrentMovement.normalized * dashDistance;
    }


    private void StopDash()
    {

        isDashing = false;
        controller.NormalMovement = true;
    }




}
