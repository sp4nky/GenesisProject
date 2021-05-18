using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMovementToEndGame : MonoBehaviour
{
    private Jump jump;
    private PlayerMovement move;
    private RopeHingeAnchor rope;
    private Rigidbody2D rb;
    public float walkingVelocity = 10;
    public PlataformVerticalMovement plataform;
    private bool userMovementDisable = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = GetComponent<Jump>();
        rope = GetComponent<RopeHingeAnchor>();
        move = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!userMovementDisable && jump.isGrounded)
        {
            jump.enabled = false;
            move.enabled = false;
            rope.enabled = false;
            rb.velocity = -Vector2.right * walkingVelocity;
            userMovementDisable = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ascensor"))
        {
            StartCoroutine(stopWalking());
        }
    }

    private IEnumerator stopWalking()
    {
        yield return new WaitForSeconds(.2f);
        walkingVelocity = 0;
        rb.velocity = Vector2.zero;
        plataform.enabled = true;
    }
}
