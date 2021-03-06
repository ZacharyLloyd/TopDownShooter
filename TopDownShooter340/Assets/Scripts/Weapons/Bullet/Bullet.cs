﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weaponThatShot;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform tf;
    [SerializeField]
    private float bulletTravelTime;
    [SerializeField]
    private float maxBulletTravelTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        MoveBullet();
    }

    // Update is called once per frame
    void Update()
    {
        bulletTravelTime += Time.deltaTime;
        if(bulletTravelTime > maxBulletTravelTime)
        {
            Destroy(gameObject);
        }
    }
    void MoveBullet()
    {
        //push the bullet forward
        rb.AddRelativeForce(weaponThatShot.pointOfFire.transform.forward * weaponThatShot.bulletTravelSpeed, ForceMode.VelocityChange);
    }

    /// <summary>
    /// destroy the projectile's gameobject, so it doesn't exist longer than it needs to on collision
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerStay(Collider other)
    {
        Debug.Log(weaponThatShot.owner.gameObject.tag);
        if (other.gameObject.GetComponent<Pawn>() != null && other.gameObject.tag != weaponThatShot.owner.gameObject.tag)
        {
            //take damage
            other.GetComponentInParent<Stats>().TakeDamage(weaponThatShot.damage);

            //check health and is dead bool
            if (other.GetComponentInParent<Stats>().currentHealth <= 0 && !other.GetComponent<Pawn>().isDead)
            {
                //enable ragdoll
                other.GetComponent<Pawn>().EnableRagDoll();
            }
            Destroy(gameObject);
        }
    }
}
