using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPawn : Pawn
{
    public enum AIMoveState
    {
        Idle,
        Chase,
        Flee
    }
    public enum AIShootState
    {
        None,
        Shoot
    }

    [SerializeField]
    private float chaseDistance;

    [SerializeField]
    private float shootingDistance;

    public AIMoveState currentMoveState { get; set; }
    public AIShootState currentShootState { get; set; }
    public EnemyController enemyController { get; set; }

    public bool isShooting { get; set; }
    public bool isChasing { get; set; }
    public bool isEvading { get; set; }

    IEnumerator manageStates;
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
        enemyController = GetComponent<EnemyController>();
        manageStates = ManageAIStates();
        StartCoroutine(manageStates);

    }
    // Update is called once per frame
    void Update()
    {
        //stats.weaponEquipped.Shoot(stats);
    }
    public void ChangeMoveStateTo(AIMoveState aiMoveStateToChange)
    {
        currentMoveState = aiMoveStateToChange;
        switch (currentMoveState)
        {
            case AIMoveState.Idle:
                isChasing = isEvading = false;
                break;
            case AIMoveState.Chase:
                isChasing = true;
                isEvading = false;
                break;
            case AIMoveState.Flee:
                isChasing = false;
                isEvading = true;
                break;
            default:
                break;
        }
        //UpdateAnimatorValues<bool>("isChasing", isChasing);
        //UpdateAnimatorValues<bool>("isEvading", isEvading);
        return;
    }
    public void ChangeShootStateTo(AIShootState aiShootStateToChange)
    {
        currentShootState = aiShootStateToChange;
        switch (currentShootState)
        {
            case AIShootState.None:
                isShooting = false;
                break;
            case AIShootState.Shoot:
                isShooting = true;
                break;
            default:
                break;
        }
        //UpdateAnimatorValues<bool>("isShooting", isShooting);
        return;
    }
    void UpdateAnimatorValues<T>(string param, object paramValue)
    {
        Type paramType = typeof(T);
        try
        {
            if (paramType == typeof(int))
                enemyController.pawn.animator.SetInteger(param, (int)paramValue);
            if (paramType == typeof(float))
                enemyController.pawn.animator.SetFloat(param, (float)paramValue);
            if (paramType == typeof(bool))
                enemyController.pawn.animator.SetBool(param, (bool)paramValue);
        }
        catch { }
    }
    IEnumerator ManageAIStates()
    {
        while (true)
        {
            if (!enemyController.pawn.isDead)
            {
                if (enemyController.pawn.stats.weaponEquipped != null)
                {
                    Transform target = enemyController.target;

                    if (target != null)
                    {
                        switch (currentMoveState)
                        {
                            case AIMoveState.Idle:
                                //Does nothing
                                enemyController.agent.speed = 0f;

                                //Check if player is in distance
                                if (Vector3.Distance(enemyController.pawn.transform.position, target.position) < chaseDistance &&
                                    Vector3.Distance(enemyController.pawn.transform.position, target.position) > shootingDistance)
                                {
                                    ChangeMoveStateTo(AIMoveState.Chase);
                                    ChangeShootStateTo(AIShootState.Shoot);
                                }
                                break;
                            case AIMoveState.Chase:
                                //Chase the target
                                enemyController.agent.SetDestination(target.position);
                                enemyController.agent.speed = 10f;
                                //Check if in range to shoot
                                if (Vector3.Distance(enemyController.pawn.transform.position, target.position) < chaseDistance)
                                    ChangeShootStateTo(AIShootState.Shoot);
                                else
                                    ChangeShootStateTo(AIShootState.None);
                                //Check health to see if we need to flee
                                if (enemyController.pawn.stats.currentHealth < 26)
                                    ChangeMoveStateTo(AIMoveState.Flee);
                                break;
                            case AIMoveState.Flee:
                                //Same this as Chase but reversed
                                enemyController.agent.SetDestination(target.position);
                                enemyController.agent.speed = -10f;
                                //Check if in distance for shooting
                                if (Vector3.Distance(enemyController.pawn.transform.position, target.position) < shootingDistance)
                                    ChangeShootStateTo(AIShootState.Shoot);
                                else
                                    ChangeShootStateTo(AIShootState.None);
                                //Are we close to death?
                                if (enemyController.pawn.stats.currentHealth > 19)
                                    ChangeMoveStateTo(AIMoveState.Chase);
                                break;
                            default:
                                break;
                        }
                        switch (currentShootState)
                        {
                            case AIShootState.None:
                                //does nothing
                                break;
                            case AIShootState.Shoot:
                                if (Vector3.Distance(enemyController.pawn.transform.position, target.position) < shootingDistance)
                                    enemyController.pawn.stats.weaponEquipped.Shoot(enemyController.pawn.stats);
                                break;
                            default:
                                break;
                        }
                    }
                }
                enemyController.agent.SetDestination(Vector3.zero);
                //Item drop goes here
                yield return false;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }
}
