using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsSettings : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private RopeHingeAnchor rope;
    private Jump jump;
    private PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jump = GetComponent<Jump>();
        health = GetComponent<PlayerHealth>();
        rope = GetComponent<RopeHingeAnchor>();
        animator.SetBool("Respawn", true);
    }

    // Update is called once per frame
    void Update()
    { 
        animator.SetBool("Attached", rope.jumpOnRope);
        if(health.hp==0)
        {
            animator.SetTrigger("Dead");
        }
        float movementx = Input.GetAxis("Horizontal");
        if(movementx < 0 || rb.velocity.x<-2)
        {
            animator.SetFloat("SpeedX", -1);
        }
        else if(movementx>0)
        {
            animator.SetFloat("SpeedX", 1);
        }
        else
        {
            animator.SetFloat("SpeedX", 0);
        }

        float yVelocity = rb.velocity.y;

        if (yVelocity < -2)
        {
            animator.SetFloat("SpeedY", -1);
        }
        else if (yVelocity > 2)
        {
            animator.SetFloat("SpeedY", 1);
        }
        else
        {
            animator.SetFloat("SpeedY", 0);
        }

        animator.SetBool("IsGrounded", jump.isGrounded);
        if (Input.GetButtonDown("Fire2") & jump.isGrounded) animator.SetBool("GroundedHook", true);
    }

    public void AnimateAttack()
    {
        animator.SetBool("Attack",true);
    }
    
    public void endGroundedHook()
    {
        animator.SetBool("GroundedHook", false);

    }

    public void endAttackAnimation()
    {
        animator.SetBool("Attack", false);
    }

    public void endRespawn()
    {
        animator.SetBool("Respawn", false);
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        gameObject.GetComponent<Jump>().enabled = true;
    }
}
