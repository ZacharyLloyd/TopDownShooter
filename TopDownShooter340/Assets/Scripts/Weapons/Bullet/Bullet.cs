﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weaponThatShot;

    protected Rigidbody rb;

    protected Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MoveBullet()
    {
        rb.AddRelativeForce(Vector3.forward * weaponThatShot.bulletTravelSpeed, ForceMode.VelocityChange);
    }

    /// <summary>
    /// destroy the projectile's gameobject, so it doesn't exist longer than it needs to on collision
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
