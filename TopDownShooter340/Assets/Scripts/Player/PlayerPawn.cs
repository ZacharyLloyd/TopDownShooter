using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    public Animator animator;
    public float speed;
    bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        //Get animator
        animator = GetComponent<Animator>();
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
}
