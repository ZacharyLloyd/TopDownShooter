using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weapon;

    protected override void OnPickup(GameObject target)
    {
        if (isPickedUp == false)
        {
            Stats targetStats = target.GetComponentInParent<Stats>();
            weapon.owner = targetStats.pawn;
            AddToInventory(targetStats);
            //transform.SetParent(target.transform);
            switch (weapon.currentWeaponType)
            {
                case Weapon.weaponType.none:
                    break;
                case Weapon.weaponType.pistol:
                    transform.SetParent(targetStats.pistolSpawnPoint);
                    break;
                case Weapon.weaponType.smg:
                    transform.SetParent(targetStats.smgSpawnPoint);
                    break;
                case Weapon.weaponType.rifle:
                    transform.SetParent(targetStats.rifleSpawnPoint);
                    break;
                default:
                    break;
            }
            isPickedUp = true;
            gameObject.SetActive(false);
            base.OnPickup(target);
            if (targetStats.pawn.GetType() == typeof(PlayerPawn))
            {
                targetStats.inventoryUIUpdate(); 
            }
        }
    }

    /// <summary>
    /// Loop through inventory array from stats Class
    /// if inventoryItem is the weapon we want to add to inventory, run addAmmo function from the weapon class
    /// else if inventoryItem is empty don't check last condition and move to next iteration of the loop
    /// else it doesn't exist in the inventory and we must put a reference to the weapon there.
    /// </summary>
    /// <param name="stats">reference to instigators stats, instigator is actor who entered trigger</param>
    void AddToInventory(Stats stats)
    {
        Debug.Log(stats.name);
        for (int i = 0; i < stats.inventory.Length; i++)
        {
            if (stats.inventory[i] == weapon)
            {
                // Add ammo instead
                weapon.AddAmmo(stats);
                break;
            }
            else if (stats.inventory[i] != null)
            {
                continue;
            }
            else
            {
                // add to inventory
                stats.inventory[i] = weapon;
                stats.pawn.ManageInventory();
                weapon.AddAmmo(stats);
                break;
            }
        }
    }
}
