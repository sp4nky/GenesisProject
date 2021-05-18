using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrutusMovement : MonoBehaviour
{
    //public Collider2D armCollider;
    public Transform target;
    public float visionRange = 20;
    public float patrolDistance = 10;
    public LayerMask playerLayer;
    public GameObject arm;
    public float patrolVeloicity;
    private float followVelocity;
    public SpriteRenderer body;

    private Rigidbody2D rb;
    private BoxCollider2D armCollider;
    private ButusAnimationSettings anim;
    private Health hp;

    public bool busy = false;
    public bool stuned = false;

    public Collider2D attack2Collider;

    private enum State
    {
        attack,
        stun,
        following,
        patrol,
        dead
    }

    private State actualState = State.patrol;

    private bool flip = false;
    public bool lookingRight
    {
        get
        {
            return body.flipX;
        }
    }

    private void Start()
    {
        
        followVelocity = patrolVeloicity;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<ButusAnimationSettings>();
        armCollider = arm.GetComponent<BoxCollider2D>();
        hp = GetComponent<Health>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!busy)
        {

            if (hp.hp == 2 && !stuned)
            {
                busy = true;
                rb.velocity = Vector2.zero;
                StartCoroutine(stunBrutus());
                stuned = true;
            }
            if (actualState == State.patrol)
            {
                rb.velocity = new Vector2(patrolVeloicity, 0);
                if (rb.velocity.x>0)
                {
                    body.flipX = true;
                }
                else
                {
                    body.flipX = false;
                }
                

                var facingDirection = target.position - transform.position;
                var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                if (aimAngle < 0f)
                {
                    aimAngle = Mathf.PI * 2 + aimAngle;
                }
                var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

                var ray = Physics2D.Raycast(transform.position, aimDirection, visionRange, playerLayer);
                if (ray.collider != null)
                {
                    actualState = State.following;
                }
            }
            if (actualState == State.attack)
            {
                anim.BrutusAttack1();
                busy = true;
            }

            if (actualState == State.following)
            {
                if (transform.position.x > target.position.x)
                {
                    rb.velocity = new Vector2(-followVelocity, 0);
                    flip = false;
                }
                else
                {
                    rb.velocity = new Vector2(followVelocity, 0);
                    flip = true;
                }
                body.flipX = flip;
                var facingDirection = target.position - transform.position;
                var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                if (aimAngle < 0f)
                {
                    aimAngle = Mathf.PI * 2 + aimAngle;
                }
                var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

                var ray = Physics2D.Raycast(transform.position, aimDirection, 10, playerLayer);
                if (ray.collider != null)
                {
                    actualState = State.attack;
                    rb.velocity = Vector2.zero;
                }
            }
              

        }
        
    }

    private IEnumerator stunBrutus()
    {
        
        anim.BrutuStun();
        yield return new WaitForSeconds(4);
        anim.BrutusAttack2();
        actualState = State.patrol;
    }

    private IEnumerator attackPlayer()
    {
        Vector3 startPosition = transform.position;
        armCollider.enabled = true;
        float armLenght;
        if(flip)
        {
            armLenght = 10;

        }
        else
        {
            armLenght = -10;

        }
        float t = 0;
        while (t<0.3f)
        {
            t += Time.deltaTime;
            armCollider.offset = new Vector2(Mathf.Lerp(0,armLenght,t/.3f), armCollider.offset.y);
            yield return null;

        }
        t = 0;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            armCollider.offset = new Vector2(Mathf.Lerp(armLenght, 0, t / .3f), armCollider.offset.y);
            yield return null;

        }
        actualState = State.patrol;

    }

    public void changeToNotBusy()
    {
        busy = false;
    }

    public void runAtack()
    {
        StartCoroutine(attackPlayer());

    }

    public void BrutusDeath()
    {
        busy = true;
        rb.velocity = Vector2.zero;
        //Cambio el tag para que no dañe al player
        armCollider.tag = "Player";
        GetComponent<Collider2D>().tag = "Player";
        anim.BrutusDead();
    }

    public void enableAttack2Collider()
    {
        attack2Collider.enabled = true;
    }

    public void disableAttack2Collider()
    {
        attack2Collider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Untagged") && actualState==State.patrol)
        {
            patrolVeloicity = -patrolVeloicity;
        }
    }
}
