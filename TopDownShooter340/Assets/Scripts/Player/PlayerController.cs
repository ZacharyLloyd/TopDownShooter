using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerPawn pawn;

    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponentInChildren<PlayerPawn>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCrouch();
        HandleMovement();
        HandleShooting();
        HandleInventory();
    }
    //Check to see if input for crouching was pressed
    void HandleCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            switch (pawn.isCrouching)
            {
                case true:
                    pawn.isCrouching = false;
                    pawn.CheckCrouch(transform.position);
                    break;
                case false:
                    pawn.isCrouching = true;
                    pawn.CheckCrouch(transform.position);
                    break;
            }
            Debug.Log(pawn.isCrouching);
        }
    }
    //Check to see if input for movement was pressed
    void HandleMovement()
    {
        pawn.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
    //Check to see if input for shooting was pressed
    void HandleShooting()
    {
        if (Input.GetButton("Fire1"))
        {
            pawn.stats.weaponEquipped.Shoot(pawn.stats);
        }
    }
    //Check to see if input for inventory was pressed
    void HandleInventory()
    {
        if (Input.GetButtonDown("WeaponSlot1"))
        {
            pawn.EquipWeapon(pawn.stats.inventory[0]);
            pawn.animator.SetInteger("Weapon", 1);
        }
        if (Input.GetButtonDown("WeaponSlot2"))
        {
            pawn.EquipWeapon(pawn.stats.inventory[1]);
            pawn.animator.SetInteger("Weapon", 0);
        }
        if (Input.GetButtonDown("WeaponSlot3"))
        {
            pawn.EquipWeapon(pawn.stats.inventory[2]);
            pawn.animator.SetInteger("Weapon", 0);
        }
    }
}
