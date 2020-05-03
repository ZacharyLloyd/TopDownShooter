using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smg : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponType = weaponType.smg;
    }
    public override void Shoot(Stats stats)
    {
        if (shootCooldownCurrent <= 0)
        {
            if (stats.smgAmmoCurrent > 0)
            {
                stats.smgAmmoCurrent -= 1;
                shootCooldownCurrent = shootCooldownMax;
                base.Shoot(stats);  
            }
        }
    }
    public override void AddAmmo(Stats stats)
    {
        if ((stats.smgAmmoCurrent + ammoToAdd) >= stats.smgAmmoMax)
        {
            stats.smgAmmoCurrent = stats.smgAmmoMax;
        }
        else
        {
            stats.smgAmmoCurrent += ammoToAdd;
        }
        base.AddAmmo(stats);
    }
}