using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rework this into pawn then come back here and fill it out
public class PlayerPawn : MonoBehaviour
{
    [Header("Components Needed")]
    public Animator animator;
    [SerializeField] private Stats stats;

    [Header("Movement Variables")]
    public float speed;
    [SerializeField]bool isCrouching;

    [Header("Transform Postions")]
    public Transform gunSlot; //position where gun will be
    public Transform leftHand; //left hand position
    public Transform rightHand; //right hand position

    // Start is called before the first frame update
    void Start()
    {
        //Get animator
        animator = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    // USE FOR DEBUG ONLY
    //Move this to PlayerController
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            animator.SetInteger("Weapon", 0); 
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            animator.SetInteger("Weapon", 1);
        }
    }

    public void Move(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x * speed);
        animator.SetFloat("Vertical", direction.y * speed);
    }
    public void CheckCrouch(Vector2 direction)
    {
        //Go into crouching blend tree
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
            animator.SetBool("isCrouching", isCrouching);
            animator.SetFloat("Horizontal", direction.x * speed);
            animator.SetFloat("Vertical", direction.y * speed);
        }
        else
        {
            //Get out of crouching blend tree
            isCrouching = false;
            animator.SetBool("isCrouching", isCrouching);
        }
    }
    //Equip a weapon
    public void EquipWeapon(Weapon weapon)
    {
        if(stats.weaponEquipped != weapon)
        {
            if(stats.weaponEquipped != null)
            {
                UnepuipWeapon(stats.weaponEquipped);
            }
            stats.weaponEquipped = Instantiate(weapon);

            stats.weaponEquipped.transform.SetParent(gunSlot);
            stats.weaponEquipped.transform.localPosition = weapon.transform.localPosition;
            stats.weaponEquipped.transform.localRotation = weapon.transform.localRotation;
            stats.weaponEquipped.currentWeaponType = weapon.currentWeaponType;
            SetAnimationLayer();

            leftHand = stats.weaponEquipped.desiredLeftHand;
            rightHand = stats.weaponEquipped.desiredRightHand;

            stats.weaponEquipped.gameObject.layer = gameObject.layer;
            ManageInventory();
        }
    }
    /*Unequip the weapon by destorying it and setting weaponEquipped to null
     since there is no longer a weapon equiped*/
    public void UnepuipWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
        stats.weaponEquipped = null;
    }
    //Handle inventory changes
    public void ManageInventory()
    {
        //TODO: Fill in what this function is supposed to do
    }
    //IK hands to appear in the right spot for the weapon
    private void OnAnimatorIK(int layerIndex)
    {
        if (rightHand == null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }
        else
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        }
        if (leftHand == null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
        else
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        }
    }
    //Set the animation layer to display the proper animation for holding the weapon
    public void SetAnimationLayer()
    {
        animator.SetInteger("Weapon", (int)(stats.weaponEquipped.currentWeaponType));
    }
}
