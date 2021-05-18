using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrol : MonoBehaviour
{
    public Transform target;
    public float maxDistanceTarjet = 25;
    public LayerMask playerLayer;
    public float patrolDistance = 20;
    private Vector3 startPoint;
    private Vector3 nextPoint;
    public float speed = 5;
    public bool tarjetConfirmed;
    private Rigidbody2D rb;
    private SpriteRenderer[] arrSprite;
    private bool OnGround = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        nextPoint = new Vector3(Random.Range(startPoint.x - patrolDistance, startPoint.x+patrolDistance), Random.Range(startPoint.y - patrolDistance, startPoint.y + patrolDistance), 0);
        arrSprite = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x>0)
        {
            foreach (SpriteRenderer sprite in arrSprite)
            {
                sprite.flipX = true;
            }
        }
        else
        {
            foreach (SpriteRenderer sprite in arrSprite)
            {
                sprite.flipX = false;
            }
        }
        if (target != null)
        {
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
                if (!OnGround)
                    rb.velocity = new Vector2(0, -2f);
                else
                    rb.velocity = Vector2.zero;
                tarjetConfirmed = true;
            }
            else 
            {
                tarjetConfirmed = false;
                facingDirection = nextPoint - transform.position;
                aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                if (aimAngle < 0f)
                {
                    aimAngle = Mathf.PI * 2 + aimAngle;
                }
                aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
                rb.velocity = speed * aimDirection;
                if (Vector3.Distance(transform.position, nextPoint) < 2f)
                {
                    nextPoint = new Vector3(Random.Range(startPoint.x - patrolDistance, startPoint.x + patrolDistance), Random.Range(startPoint.y - patrolDistance, startPoint.y + patrolDistance), 0);

                }

            }
        }


    }


    public void addGravty()
    {
        rb.gravityScale += 1;
    }

    public void disablePatrol()
    {
        this.enabled = false;
    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tarjetConfirmed)
        {
            rb.velocity = Vector2.zero;
            OnGround = true;
        }
        else
        { 
            nextPoint = new Vector3(Random.Range(startPoint.x - patrolDistance, startPoint.x + patrolDistance), Random.Range(startPoint.y - patrolDistance, startPoint.y + patrolDistance), 0);
            OnGround = false;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (tarjetConfirmed)
        {
            rb.velocity = Vector2.zero;
            OnGround = true;
        }
        else
        {
            nextPoint = new Vector3(Random.Range(startPoint.x - patrolDistance, startPoint.x + patrolDistance), Random.Range(startPoint.y - patrolDistance, startPoint.y + patrolDistance), 0);
            OnGround = false;

        }
    }
}
