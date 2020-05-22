using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Pawn : MonoBehaviour
{
    protected static Pawn Instance;

    [Header("Components Needed")]
    public Animator animator;
    [SerializeField] public Stats stats;

    [Header("Movement Variables")]
    public float speed;
    [SerializeField] public bool isCrouching;

    //[Header("Transform Postions")]
    //public Transform leftHand; //left hand position
    //public Transform rightHand; //right hand position

    //Const for zero position
    public Vector3 ZERO_POSITION = new Vector3(0f, 0f, 0f);

    //Const for zero rotation
    public Quaternion ZERO_ROTATION = new Quaternion(0f, 0f, 0f, 0f);

    [Header("Bools")]
    public bool isDead;

    [Header("NavMesh Agent"), Tooltip("If the pawn uses Artificial Intelligence.")]
    [SerializeField] protected NavMeshAgent agent;

    [Header("Time variables for death")]
    public float time;
    public float timeToDie;
    public float reset;

    public SpawnPoint spawner;

    public bool droppedSomething;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Instance = this;

        //Get animator
        animator = GetComponent<Animator>();
        stats = GetComponentInParent<Stats>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        SetColliderEnablementOfChildrenRigidBodies(false);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isDead == true)
        {
            time += Time.deltaTime;
            if (time >= timeToDie)
            {
                DestroySelf();
            }
        }
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

                    stats.weaponEquipped.transform.SetParent(stats.pistolSpawnPoint);
                    ZeroOutLocalTransforms(stats.weaponEquipped.transform);
                    break;
                case Weapon.weaponType.smg:
                    stats.weaponEquipped.transform.SetParent(stats.smgSpawnPoint);
                    ZeroOutLocalTransforms(stats.weaponEquipped.transform);
                    break;
                case Weapon.weaponType.rifle:
                    stats.weaponEquipped.transform.SetParent(stats.rifleSpawnPoint);
                    ZeroOutLocalTransforms(stats.weaponEquipped.transform);
                    break;
                default:
                    break;
            }
            //stats.weaponEquipped.transform.localPosition = weapon.transform.localPosition;
            //stats.weaponEquipped.transform.localRotation = weapon.transform.localRotation;
            stats.weaponEquipped.currentWeaponType = weapon.currentWeaponType;
            //SetAnimationLayer();



            stats.weaponEquipped.gameObject.layer = gameObject.layer;
            ManageInventory();
        }
    }

    /// <summary>
    /// Zero out the local position, rotation, and scale of a particular transform
    /// </summary>
    /// <param name="_transform"></param>
    void ZeroOutLocalTransforms(Transform _transform)
    {
        _transform.localPosition = ZERO_POSITION;
        _transform.localRotation = ZERO_ROTATION;
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
        if (stats == null || stats.weaponEquipped == null)
        {
            return;
        }

        //If there's a spot for the model to place its right hand
        if (stats.weaponEquipped.desiredRightHand == null)
        {

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
        }
        else
        {
            //Debug.Log("If you got this, you are amazing");
            animator.SetIKPosition(AvatarIKGoal.RightHand, stats.weaponEquipped.desiredRightHand.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, stats.weaponEquipped.desiredRightHand.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        }

        //If there's a spot for the model to place its left hand
        if (stats.weaponEquipped.desiredLeftHand == null)
        {

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
        }
        else
        {
            //Debug.Log("If you got this, you are amazing");
            animator.SetIKPosition(AvatarIKGoal.LeftHand, stats.weaponEquipped.desiredLeftHand.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, stats.weaponEquipped.desiredLeftHand.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        }
    }
    //Set the animation layer to display the proper animation for holding the weapon
    public virtual void SetAnimationLayer()
    {
        animator.SetInteger("Weapon", (int)(stats.weaponEquipped.currentWeaponType));
    }
    public virtual void EnableRagDoll()
    {
    }
    public virtual void DestroySelf()
    {
        if (isDead == true)
        {
            Destroy(stats.gameObject);
        }
    }
    protected Collider[] FindAllCollidersInChildren()
    {
        List<Collider> childrenColliders = new List<Collider>();
        Transform[] childrenObject = GetComponentsInChildren<Transform>();

        //The first object we grab will not be added to the list.
        int count = 0;

        foreach (Transform obj in childrenObject)
        {
            Collider childCollider = null;

            if (obj.gameObject.GetComponent<Collider>() != null && count > 0)
            {
                childCollider = obj.gameObject.GetComponent<Collider>();
                childrenColliders.Add(childCollider);
            }

            count++;
        }

        return childrenColliders.ToArray();
    }

    protected void SetColliderEnablementOfChildrenRigidBodies(bool _enable)
    {
        Collider[] childrenColliders = FindAllCollidersInChildren();

        foreach (Collider collider in childrenColliders)
        {
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
               
            collider.enabled = _enable;
            rb.isKinematic = !_enable;

        }
    }
}
