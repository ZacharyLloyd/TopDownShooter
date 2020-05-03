using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponType = weaponType.pistol;
    }
    public override void Shoot(Stats stats)
    {
        if (shootCooldownCurrent <= 0)
        {

            if (stats.pistolAmmoCurrent > 0)
            {
                stats.pistolAmmoCurrent -= 1;
                shootCooldownCurrent = shootCooldownMax;
                base.Shoot(stats); 
            }
        }
        
    }
    public override void AddAmmo(Stats stats)
    {
        if ((stats.pistolAmmoCurrent + ammoToAdd) >= stats.pistolAmmoMax)
        {
            stats.pistolAmmoCurrent = stats.pistolAmmoMax;
        }
        else
        {
            stats.pistolAmmoCurrent += ammoToAdd;
        }
        base.AddAmmo(stats); 
    }
}
