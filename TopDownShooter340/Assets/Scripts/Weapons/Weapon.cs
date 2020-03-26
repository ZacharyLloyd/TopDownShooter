using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Transform Postions")]
    public Transform gunSlot; //position where gun will be
    public Transform desiredLeftHand; //left hand position
    public Transform desiredRightHand; //right hand position
    public Transform pointOfFire; //where the bullet will shoot from

    [Header("Shooting Variables")]
    public int rateOfFire; //how fast the weapon can shoot
    public int bulletTravelSpeed; //how fast the bullet moves

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
    public virtual void Shoot(Weapon weapon)
    {

    }
}
