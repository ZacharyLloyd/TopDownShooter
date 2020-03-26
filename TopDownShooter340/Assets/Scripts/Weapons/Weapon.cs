using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Transform Postions")]
    public Transform gunSlot; //position where gun will be
    public Transform desiredLeftHand; //left hand position
    public Transform desiredRightHand; //right hand position


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
