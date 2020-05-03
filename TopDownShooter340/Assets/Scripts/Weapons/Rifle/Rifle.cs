using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponType = weaponType.rifle;
    }
    public override void Shoot(Stats stats)
    {
        if (shootCooldownCurrent <= 0)
        {
            if (stats.rifleAmmoCurrent > 0)
            {
                stats.rifleAmmoCurrent -= 1;
                shootCooldownCurrent = shootCooldownMax;
                base.Shoot(stats);  
            }
        }
    }
    public override void AddAmmo(Stats stats)
    {
        if((stats.rifleAmmoCurrent + ammoToAdd) >= stats.rifleAmmoMax)
        {
            stats.rifleAmmoCurrent = stats.rifleAmmoMax;
        }
        else
        {
            stats.rifleAmmoCurrent += ammoToAdd;
        }
        base.AddAmmo(stats);
    }
}
