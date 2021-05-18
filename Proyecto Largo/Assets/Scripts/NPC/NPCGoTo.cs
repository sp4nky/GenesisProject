using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGoTo : MonoBehaviour
{
    public Transform[] positions;
    public float velocity = 2f;
    public SpriteRenderer sprite;
    private Rigidbody2D rb;
    private int index = -1;
    private bool changePosition = true;
    private bool stop = false;
    private bool talking = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.x > 0.2f)
        {
            sprite.flipX = false;
        }
        else if(rb.velocity.x < -0.2f)
        {
            sprite.flipX = true;
        }

        if (!talking)
        {
            if (positions.Length > 0 && changePosition)
            {
                if (index >= positions.Length - 1)
                {
                    index = 0;
                }
                else
                    index++;
                Vector2 direction = positions[index].transform.position - transform.position;
                rb.velocity = direction.normalized * velocity;
                changePosition = false;
            }
            if (Vector2.Distance(transform.position, positions[index].position) < 1f && !stop)
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(Thinking());
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator Thinking()
    {
        stop = true;
        yield return new WaitForSeconds(2);
        changePosition = true;
        stop = false;
    }

    public void StopMovement()
    {
        talking = true;
    }

    public void ResumeMovement()
    {
        talking = false;
        Vector2 direction = positions[index].transform.position - transform.position;
        rb.velocity = direction.normalized * velocity;
    }
}
