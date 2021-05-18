using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0CU5TAnimationSettings : MonoBehaviour
{
    private Animator anim;
    private AutoJump aj;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        aj = GetComponent<AutoJump>();
    }    
    
    // Update is called once per frame
    void Update()
    {

            anim.SetBool("Grounded", aj.grounded);
            anim.SetBool("JumpAir", aj.jumping);


    }

    public void JumpAnimation()
    {
        anim.SetTrigger("Jump");
    }

    public void hitFalse()
    {
        anim.SetBool("Hit", false);
    }

    public void hitTrue()
    {
        anim.SetBool("Hit", true);
    }

    public void DeadTrue()
    {
        anim.SetBool("Dead", true);
    }

}
