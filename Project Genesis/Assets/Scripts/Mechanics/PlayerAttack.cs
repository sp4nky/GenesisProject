using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    private AnimationsSettings aSettings;
    private PlayerMovement pm;
    private Jump jump;
    private Rigidbody2D rb;
    public StunAttack stunAttack;
    public AudioController ac;

    private void Awake()
    {
        aSettings = GetComponent<AnimationsSettings>();
        pm = GetComponent<PlayerMovement>();
        jump = GetComponent<Jump>();
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<AudioController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            aSettings.AnimateAttack();
            jump.enabled = false;
            pm.enabled = false;
            if(jump.isGrounded)
            {
                rb.velocity = Vector2.zero;
            }
        }
        if((Input.GetButtonUp("Fire1")))
        {
            endAttack();
        }
    }

    public void runAtack()
    {
        stunAttack.ApplyStun();
    }

    public void endAttack()
    {
        aSettings.endAttackAnimation();
        jump.enabled = true;
        pm.enabled = true;
        ac.Stop();
    }



}