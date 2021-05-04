using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        WeaponShoot();
    }

    void WeaponShoot()
    {
        // If we have Assault rifle
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE) {
            // if we press and hold
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                //BulletFIred();
            }
        }
        // if we have a regular weapon that shoots once
        else {
            if (Input.GetMouseButtonDown(0)) {

                //handle axe
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET) {

                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                    //BulletFIred();
                }
                else {
                    // we have an arrow or spear


                }
            }
        }
    }
}
