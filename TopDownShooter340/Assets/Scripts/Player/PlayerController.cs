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
        //Pass input to the pawn
        pawn.CheckCrouch(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        pawn.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetButton("Fire1"))
        {
            pawn.stats.weaponEquipped.Shoot(pawn.stats);
        }
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
}
