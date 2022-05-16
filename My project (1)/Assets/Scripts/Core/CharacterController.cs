using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Vector2 CurrentMovement { get; set; }

    public bool NormalMovement { get; set; }
    private Rigidbody2D myRigidbody2D;
    private Vector2 recoilMovement;
    

    /// Start is called before the first frame update
    private void Start()
    {
        NormalMovement = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        Recoil();

        if (NormalMovement)
        {
            MoveCharacter();
        }

        

    }
    

    private void MoveCharacter()
    {
        Vector2 currentMovePosition = myRigidbody2D.position + CurrentMovement * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(currentMovePosition);
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        recoilMovement = recoilDirection.normalized * recoilForce;
    }
    public void MovePosition(Vector2 newPosition)
    {
        myRigidbody2D.MovePosition(newPosition);
    }

    public void SetMovement(Vector2 newPosition)
    {

        CurrentMovement = newPosition;

    }

    private void Recoil()
    {
        if(recoilMovement.magnitude > 0.1f)
        {
            myRigidbody2D.AddForce(recoilMovement);
        }
    }
}
