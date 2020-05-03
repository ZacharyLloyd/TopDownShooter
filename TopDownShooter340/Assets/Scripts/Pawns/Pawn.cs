using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [Header("Components Needed")]
    public Animator animator;
    [SerializeField] public Stats stats;

    [Header("Movement Variables")]
    public float speed;
    [SerializeField] public bool isCrouching;

    [Header("Transform Postions")]
    public Transform gunSlot; //position where gun will be
    public Transform leftHand; //left hand position
    public Transform rightHand; //right hand position

    [Header("Bools")]
    public bool isDead;

    // Start is called before the first frame update
    protected void Start()
    {
        //Get animator
        animator = GetComponent<Animator>();
        stats = GetComponentInParent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Move(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x * speed);
        animator.SetFloat("Vertical", direction.y * speed);
    }

    public virtual void CheckCrouch(Vector2 direction)
    {
        //Go into crouching blend tree
        if (isCrouching == true)
        {
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
    public virtual void EquipWeapon(Weapon weapon)
    {
        if (stats.weaponEquipped != weapon)
        {
            if (stats.weaponEquipped != null)
            {
                UnepuipWeapon(stats.weaponEquipped);
            }
            stats.weaponEquipped = weapon;
            stats.weaponEquipped.gameObject.SetActive(true);

            switch (weapon.currentWeaponType)
            {
                case Weapon.weaponType.none:
                    break;
                case Weapon.weaponType.pistol:
                    transform.SetParent(stats.pistolSpawnPoint);
                    stats.weaponEquipped.transform.localPosition = stats.pistolSpawnPoint.transform.localPosition;
                    //stats.weaponEquipped.transform.rotation = stats.pistolSpawnPoint.transform.rotation;
                    break;
                case Weapon.weaponType.smg:
                    transform.SetParent(stats.smgSpawnPoint);
                    stats.weaponEquipped.transform.localPosition = stats.smgSpawnPoint.transform.localPosition;
                    stats.weaponEquipped.transform.localRotation = stats.smgSpawnPoint.transform.localRotation;
                    break;
                case Weapon.weaponType.rifle:
                    transform.SetParent(stats.rifleSpawnPoint);
                    stats.weaponEquipped.transform.localPosition = stats.rifleSpawnPoint.transform.localPosition;
                    stats.weaponEquipped.transform.localRotation = stats.rifleSpawnPoint.transform.localRotation;
                    break;
                default:
                    break;
            }
            //stats.weaponEquipped.transform.localPosition = weapon.transform.localPosition;
            //stats.weaponEquipped.transform.localRotation = weapon.transform.localRotation;
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
    public virtual void UnepuipWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(false);
        stats.weaponEquipped = null;
    }
    //Handle inventory changes
    public virtual void ManageInventory()
    {
    }
    //IK hands to appear in the right spot for the weapon
    public virtual void OnAnimatorIK(int layerIndex)
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
    public virtual void SetAnimationLayer()
    {
        animator.SetInteger("Weapon", (int)(stats.weaponEquipped.currentWeaponType));
    }
}
