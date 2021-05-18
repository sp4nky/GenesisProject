using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMapAnimationSettings : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.2f || Mathf.Abs(rb.velocity.y) > 0.2f)
        {
            anim.SetFloat("speedx", 1);
        }
        else
        {
            anim.SetFloat("speedx", 0);
        }
    }
}

