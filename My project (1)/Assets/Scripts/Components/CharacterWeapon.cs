using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    public Weapon CurrentWeapon { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }

    protected override void HandleInput()
    {
        if(Input.GetMouseButton(0))
        {
            Shoot();
        }

        if(Input.GetMouseButtonUp(0))
        {
            StopWeapon();   
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }

    
    public void Shoot()
    {
        if(CurrentWeapon == null)
        {
            return;
        }
        CurrentWeapon.TriggerShot();
    }

    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.StopWeapon();
    }
    public void Reload()
    {
        if(CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();
      
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition; 
        CurrentWeapon.SetOwner(character);
    }
}
