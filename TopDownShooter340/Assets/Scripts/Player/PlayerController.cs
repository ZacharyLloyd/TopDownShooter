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
    void FixedUpdate()
    {
        //Pass input to the pawn
        pawn.CheckCrouch(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        pawn.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
