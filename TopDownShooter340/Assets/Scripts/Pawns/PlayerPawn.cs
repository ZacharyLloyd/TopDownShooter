using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rework this into pawn then come back here and fill it out
public class PlayerPawn : Pawn
{

    // USE FOR DEBUG ONLY
    //Move this to PlayerController
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("Weapon", 0); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("Weapon", 1);
        }
    }

    public override void Move(Vector2 direction)
    {
        base.Move(direction);
    }
    public override void CheckCrouch(Vector2 direction)
    {
        base.CheckCrouch(direction);
    }
    //Equip a weapon
    public override void EquipWeapon(Weapon weapon)
    {
        base.EquipWeapon(weapon);
    }
    /*Unequip the weapon by destorying it and setting weaponEquipped to null
     since there is no longer a weapon equiped*/
    public override void UnepuipWeapon(Weapon weapon)
    {
        base.UnepuipWeapon(weapon);
    }
    //Handle inventory changes
    public override void ManageInventory()
    {
        stats.inventoryUIUpdate();
        base.ManageInventory();
    }
    //IK hands to appear in the right spot for the weapon
    public override void OnAnimatorIK(int layerIndex)
    {
        base.OnAnimatorIK(layerIndex);
    }
    //Set the animation layer to display the proper animation for holding the weapon
    public  override void SetAnimationLayer()
    {
        base.SetAnimationLayer();
    }
}
