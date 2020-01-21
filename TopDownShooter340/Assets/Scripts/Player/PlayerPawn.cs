using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    public Animator animator;
    public float speed;

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
}
