using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1;

    //Variables para el gancho
    public Vector2 ropeHook;
    public float swingForce = 4f;
    public bool isSwinging = false;
    public LayerMask groundLayerMask;
    public bool flip { get; set; }
    public SpriteRenderer Body;
    public SpriteRenderer leds;
    private Collider2D col;
    private Rigidbody2D rb;
    private float horizontalInput = 0;
    private PlayerBulletShoot shoot;
    private Jump jump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        jump = GetComponent<Jump>();
        shoot = GetComponentInChildren<PlayerBulletShoot>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shoot.direction = 1;
        this.enabled = false;
    }

    private void FixedUpdate()
    {


        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            if (isSwinging)
            {
                // 1 - Get a normalized direction vector from the player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;
                                                
                // Invierte la direccion para tener direccion perpendicular
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);

                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                }

                var force = perpendicularDirection * swingForce;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                if (jump.isGrounded)
                {
                    var groundForce = speed/2f;
                    rb.AddForce(new Vector2((horizontalInput * groundForce - rb.velocity.x) * groundForce, 0));
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
        }


        if (!isSwinging)
        {
            rb.velocity = new Vector2(Vector2.right.x * horizontalInput * speed, rb.velocity.y);

        }


    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput < 0 || rb.velocity.x < -2f)
        {
            flip = true;
            shoot.direction = -1;
            if (!isSwinging)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                /*Body.flipX = true;
                leds.flipX = true;*/
            }
        }
        else if (horizontalInput > 0)
        {
            flip = false;
            shoot.direction = 1;

            if (!isSwinging)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                /*
                Body.flipX = false;
                leds.flipX = false;*/
            }
                
        }


    }

  






}
