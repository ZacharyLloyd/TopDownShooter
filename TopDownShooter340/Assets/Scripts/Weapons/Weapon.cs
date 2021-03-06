﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("UI")]
    public Sprite weaponSprite;
    [Header("Owner of Weapon")]
    public Pawn owner;

    [Header("Transform Postions")]
    public Transform gunSlot; //position where gun will be
    public Transform desiredLeftHand; //left hand position
    public Transform desiredRightHand; //right hand position
    public Transform pointOfFire; //where the bullet will shoot from

    [Header("Shooting Variables")]
    public float bulletTravelSpeed; //how fast the bullet moves
    public float damage;
    public float shootCooldownCurrent;
    public float shootCooldownMax;

    [Header("Bullet to shoot")]
    public Bullet bulletPrefab;

    [Header("Ammo Info")]
    [SerializeField]
    protected int ammoToAdd;

    public enum weaponType
    {
        none,
        pistol,
        smg,
        rifle
    }

    public weaponType currentWeaponType;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if(shootCooldownCurrent >= 0)
        {
            shootCooldownCurrent -= Time.deltaTime;
        }
    }
    public virtual void Shoot(Stats stats)
    {
        Bullet bullet = Instantiate(bulletPrefab, pointOfFire.position, Quaternion.identity);
        //Rotate the bullet since when it instantiates it does not fire straight
        bullet.gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 60f);
        bullet.weaponThatShot = this;
        if (stats.pawn.GetType() == typeof(PlayerPawn))
        {
            stats.inventoryUIUpdate(); 
        }
    }
    public virtual void AddAmmo(Stats stats)
    {
        if (stats.pawn.GetType() == typeof(PlayerPawn))
        {
            stats.inventoryUIUpdate(); 
        }
    }
}
