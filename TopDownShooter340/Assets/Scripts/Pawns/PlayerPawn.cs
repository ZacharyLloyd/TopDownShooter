using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rework this into pawn then come back here and fill it out
public class PlayerPawn : Pawn
{
    public PlayerController controller;
    protected override void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
        stats.inventoryUIUpdate();
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
        base.ManageInventory();
    }
    //IK hands to appear in the right spot for the weapon
    public override void OnAnimatorIK(int layerIndex)
    {
        base.OnAnimatorIK(layerIndex);
    }
    //Set the animation layer to display the proper animation for holding the weapon
    public override void SetAnimationLayer()
    {
        base.SetAnimationLayer();
    }
    public override void EnableRagDoll()
    {

        try
        {
            if (stats.weaponEquipped != null)
            {
                stats.weaponEquipped.owner.UnepuipWeapon(stats.weaponEquipped);
            }

            animator.enabled = false;
            controller.enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            SetColliderEnablementOfChildrenRigidBodies(true);
            isDead = true;
        }
        catch { }
    }
}
