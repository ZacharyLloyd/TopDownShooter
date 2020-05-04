using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    protected override void Start()
    {
        foreach (Weapon weapon in stats.inventory)
        {
            if (weapon != null)
            {
                EquipWeapon(weapon);
            }
        }
        //Get animator
        animator = GetComponent<Animator>();
        stats = GetComponentInParent<Stats>();
    }
    // Update is called once per frame
    void Update()
    {
       stats.weaponEquipped.Shoot(stats);
    }
}
