using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{

    [SerializeField] private GameObject reticlePrefab;
    public float CurrentAimAngleAbsolute { get; set; }
    public float CurrentAimAngle { get; set; }

    private Camera mainCamera;
    private GameObject reticle;
    private Weapon weapon;
    private Vector3 direction;
    private Vector3 mousePosition;
    private Vector3 reticlePosition;
    private Vector3 currentAim = Vector3.zero;
    private Vector3 currentAimAbsolute = Vector3.zero;
    private Quaternion initialRotation;
    private Quaternion lookRotation;





    private void Start()
    { 
        Cursor.visible = false;
        weapon = GetComponent<Weapon>();
        initialRotation = transform.rotation;

        mainCamera = Camera.main;
        reticle = Instantiate(reticlePrefab);   
    }

    private void Update()
    {
        GetMousePosition();
        MoveReticle();
        RotateWeapon();
        
    }
 

    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if(weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            currentAim = direction - transform.position;

        }
        else
        {
            currentAim = transform.position - direction;
        }

    }

    private void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero)
        {
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg ;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg ;
        
            if(weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            else
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }

            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;
      
      }
      else
      {
          CurrentAimAngle = 0f;
          transform.rotation = initialRotation;
      }
    }
     private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity;
        reticle.transform.position = reticlePosition;
    }


}
