using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpImpulse = 10;
    private Rigidbody2D body;
    private CapsuleCollider2D col;
    public Transform feetPosition;    public float groundCheckRadius = .1f;    private RopeHingeAnchor rope;

    private Collider2D isGround;

    [Header("Ground setup")]
    //if the object collides with another object tagged as this, it can jump again
    public LayerMask groundLayerMask;

    private bool canJump = true;
    public bool isGrounded
    {
        get
        {
            return canJump;
        }
    }
    private void Awake()
    {
        rope = GetComponent<RopeHingeAnchor>();
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (groundCheck())
        {
            canJump = true;

        }
        else
        {
            canJump = false;

        }
        bool jump = Input.GetButtonDown("Jump");
        if (jump & canJump)
        {
            jumpNow();

        }
 
    }


    public void jumpNow()
    {
        body.velocity = new Vector2(body.velocity.x, jumpImpulse);
    }

    private bool groundCheck()
    {

        isGround = Physics2D.OverlapCircle(feetPosition.position, groundCheckRadius, groundLayerMask);
        return isGround != null;
    }
}
