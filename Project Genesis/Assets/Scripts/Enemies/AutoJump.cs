using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJump : MonoBehaviour
{

    private int grades= 60;

    public float speed = 20;
    public Transform target;
    public float maxDistanceTarjet;
    public LayerMask playerLayer;
    public float timeLapse = 2;
    public float cooldownJump = 4;
    private float nextJump=0;
    private Rigidbody2D rb;
    public SpriteRenderer body;
    public Transform feetPosition;
    private Collider2D isGround;
    public float groundCheckRadius = .1f;
    public LayerMask groundLayerMask;
    private L0CU5TAnimationSettings animSettings;
    private bool falling = false;
    private Vector2 jumpVelocity;
    public bool grounded
    {
        get
        {
            return groundCheck() && falling;
        }
    }
    //Variables para calcular el salto
    private float vx = 0;
    private float vy = 0;
    private float xf = 0;
    private float y0 = 0;
    public bool jumping = false;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animSettings = GetComponent<L0CU5TAnimationSettings>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        grades = Random.Range(60, 120);
        if (rb.velocity.y>-1f && rb.velocity.y < 1f && !groundCheck())
        {
            jumping = true;

        }
        if (rb.velocity.x < 0)
        {
            body.flipX = true;
        }
        else if (rb.velocity.x > 0.1f)
        {
            body.flipX = false;
        }

        if (Time.time > nextJump)
        {
            if (target != null)
            {
                //float distance = Vector3.Distance(transform.position, target.position);
                var facingDirection = target.position - transform.position;
                var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                if (aimAngle < 0f)
                {
                    aimAngle = Mathf.PI * 2 + aimAngle;
                }
                var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
                var ray = Physics2D.Raycast(transform.position, aimDirection, maxDistanceTarjet, playerLayer);
                if (ray.collider != null)
                {
                    xf = ray.point.x - rb.position.x;
                    y0 = ray.point.y - rb.position.y;
                    vy = (Physics2D.gravity.magnitude*rb.gravityScale * timeLapse * timeLapse / 2f - y0)/timeLapse;
                    vx = xf / timeLapse;
                    jumpVelocity = new Vector2(vx*2f, vy/2f);
                    animSettings.JumpAnimation();
                }
                else
                {
                    //Anticipacion del salto y luego salta
                    jumpVelocity= new Vector2(speed * Mathf.Cos(Mathf.Deg2Rad * grades), speed * Mathf.Sin(Mathf.Deg2Rad * grades));
                    animSettings.JumpAnimation();

                }
            }
            nextJump = Time.time + cooldownJump;
        }
        
        
    }

    public void isFalling()
    {
        falling = true;
        jumping = false;
    }

    public void endJump()
    {
        falling = false;
        jumping = false;
    }

    public void JumperJump()
    {
        rb.velocity = jumpVelocity;
    }

    private bool groundCheck()
    {
        isGround = Physics2D.OverlapCircle(feetPosition.position, groundCheckRadius, groundLayerMask);
        return isGround != null;
    }
}
