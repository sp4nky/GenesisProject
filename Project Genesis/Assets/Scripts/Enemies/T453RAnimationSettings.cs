using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T453RAnimationSettings : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float xPosition,previusPosition;
    private float yVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        xPosition = transform.position.x;
        previusPosition = xPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        yVelocity = rb.velocity.y;
        animator.SetFloat("SpeedY", yVelocity);
        xPosition = transform.position.x;
        if(xPosition>previusPosition)
        {
            animator.SetFloat("SpeedX", 1);
        }
        else
        {
            animator.SetFloat("SpeedX", -1);
        }
        previusPosition = xPosition;
    }


}
