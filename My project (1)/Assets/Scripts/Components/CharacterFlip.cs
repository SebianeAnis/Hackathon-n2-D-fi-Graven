using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponents
{
    public enum FlipMode
    {
        MovementDirection,
        WeaponDirection

    }

    [SerializeField] private FlipMode flipMode = FlipMode.MovementDirection;
    [SerializeField] private float threshold = 0.1f;

    public bool FacingRight { get; set; }

    private void Awake()
    {
        FacingRight = true;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        if (flipMode == FlipMode.MovementDirection)
        {
            FlipToMoveDirection();
        }
        else
        {
            FlipToWeaponDirection();
        }

    }

    private void FlipToMoveDirection()
    {
        if (controller.CurrentMovement.normalized.magnitude > threshold)
        {
            if (controller.CurrentMovement.normalized.x > 0 )
            {
                FaceDirection(1);
            }
            else
            {
                FaceDirection(-1);
            }
        }

    }

    private void FlipToWeaponDirection()
    {
        // -------
    }

    private void FaceDirection(int newDirection)
    {
        if (newDirection == 1)
        {
            transform.localScale = new Vector3(x: 1, y: 1, z: 1);
            FacingRight = true;
        }
        else 
        {
            transform.localScale = new Vector3(x: -1, y: 1, z: 1);   
            FacingRight = false;
        }


    }


}
