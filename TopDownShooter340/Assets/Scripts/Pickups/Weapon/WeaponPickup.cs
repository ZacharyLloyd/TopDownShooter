using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void OnPickup(GameObject target)
    {
        Stats targetStats = target.GetComponent<Stats>();
        AddToInventory(targetStats);
        base.OnPickup(target);
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
