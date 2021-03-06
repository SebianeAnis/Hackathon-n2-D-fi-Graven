using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeBtwShots = 0.5f;

    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")]
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 15;

    public Character WeaponOwner { get; set; }

    public int CurrentAmmo { get; set; }

    public WeaponAmmo WeaponAmmo { get; set; }

    public bool UseMagazine => useMagazine;

    public int MagazineSize => magazineSize;

    public bool CanShoot { get; set; }

    private float nextShotTime;

    private CharacterController controller;

    
    private void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = magazineSize;
    }

    private void Update()
    {
        WeaponCanShoot();
        Debug.Log(CurrentAmmo);
    }

    public void TriggerShot()
    {
        StartShooting();
    }

    public void StopWeapon()
    {
        if(useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        }
    }

    private void StartShooting()
    {
        if(useMagazine)
        {
            if(WeaponAmmo != null)
            {
                if(WeaponAmmo.CanUseWeapon())
                {
                    RequestShot();
                }
                else
                {
                    if(autoReload)
                    {
                        Reload();
                    }
                }
            }
        }
        else
        {
            RequestShot();
        }
    }

    private void RequestShot()
    {
        if(!CanShoot)
        {
            return;
        }

        if(useRecoil)
        {
            Recoil();
        }

         Debug.Log("Shooting");
         WeaponAmmo.ConsumeAmmo();

         CanShoot = false;
    }

    private void Recoil()
    {
        if (WeaponOwner != null)
        {
            if(WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                controller.ApplyRecoil(Vector2.left, recoilForce);
            }
            else
            {
                controller.ApplyRecoil(Vector2.right, recoilForce);   
            }
        }
    }
    private void WeaponCanShoot()
    {
        if(Time.time > nextShotTime)
        {
            CanShoot = true;
            nextShotTime = Time.time + timeBtwShots;
        }
    }

    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
        controller = WeaponOwner.GetComponent<CharacterController>();
    }
    
    public void Reload()
    {
        if(WeaponAmmo != null)
        {
            if(useMagazine)
            {
             WeaponAmmo.RefillAmmo();
            }
        }
    }
}

