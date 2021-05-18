using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButusAnimationSettings : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (1 < Mathf.Abs(rb.velocity.x))
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);

        }
    }

    public void Walking()
    {
        anim.SetBool("Walking", true);

    }
    public void NotWalking()
    {
        anim.SetBool("Walking", false);
    }
    public void BrutusAttack1()
    {
        anim.SetTrigger("Attack1");
    }
    public void BrutusAttack2()
    {
        anim.SetTrigger("Attack2");
    }
    public void BrutuStun()
    {
        anim.SetTrigger("Stun");
    }
    public void BrutusDead()
    {
        anim.SetBool("Dead", true);
    }

    public void BrutuHit()
    {
        anim.SetBool("Hit", true);
    }
    public void BrutuHitFalse()
    {
        anim.SetBool("Hit", false);
    }
}