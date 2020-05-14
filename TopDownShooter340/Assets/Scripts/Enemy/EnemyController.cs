using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyPawn pawn;
    public NavMeshAgent agent;
    public Transform target;
    Weapon[] allWeaponsInWorld;

    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponentInChildren<EnemyPawn>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = pawn.GetComponent<NavMeshAgent>();
        if(pawn.stats.weaponEquipped == null)
        {
            //Add the weapons in the scene to the list
            allWeaponsInWorld = FindObjectsOfType<Weapon>();
            //Cycle through all weapons in the scene
            //foreach(Weapon obj in allWeaponsInWorld)
            //{
            //    //If we are close to the weapon it becomes the target to chase
            //    if (Vector3.Distance(pawn.transform.position, obj.transform.position) <= 10)
            //        target = obj.transform;
            //    //Chase the weapon
            //    pawn.ChangeMoveStateTo(EnemyPawn.AIMoveState.Chase);
            //}
        }
        else
        {
            pawn.stats.weaponEquipped.owner = pawn;
            //Chase the player instead
            target = GameObject.FindGameObjectWithTag("Player").transform;
            pawn.ChangeMoveStateTo(EnemyPawn.AIMoveState.Idle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }
    private void FixedUpdate()
    {
        //Create a vector that is from the to our target
        Vector3 desiredVelocity = Vector3.MoveTowards(transform.position, agent.desiredVelocity, agent.acceleration);
        agent.transform.LookAt(target.transform);
        //Instead of using the x go with the z
        Vector3 input = transform.InverseTransformDirection(desiredVelocity);
        //Pass in values for our x and z axis for horizontal and vertical movement
        pawn.animator.SetFloat("Horizontal", input.x);
        pawn.animator.SetFloat("Vertical", input.z);
    }
    private void OnAnimatorMove()
    {
        agent.velocity = pawn.animator.velocity;
    }
}
