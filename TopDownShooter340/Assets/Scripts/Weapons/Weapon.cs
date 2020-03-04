using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("IK Information")]
    public Transform desiredLeftHand;
    public Transform desiredRightHand;

    
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
