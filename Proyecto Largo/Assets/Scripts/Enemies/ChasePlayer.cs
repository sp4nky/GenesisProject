using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChasePlayer : MonoBehaviour
{
    public SpriteRenderer enemySprite;
    public float rangeChase=10f;
    public float velocityChase;
    public LayerMask playerMask;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagement.instance.onCombat)
            return;
        if(rb.velocity.x>0.2f)
        {
            enemySprite.flipX = enabled;
        }
        else if(rb.velocity.x < 0.2f)
        {
            enemySprite.flipX = false;
        }
        Collider2D col= Physics2D.OverlapCircle(transform.position, rangeChase, playerMask);
        if(col)
        {
            Vector2 direction =  col.transform.position - transform.position;
            rb.velocity = direction.normalized * velocityChase;
        }
        else
        {
            rb.velocity = Vector2.zero;

        }
    }
}
