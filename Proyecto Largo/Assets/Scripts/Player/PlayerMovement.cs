using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speedx = 5;
    public float speedy = 5;

    public SpriteRenderer spritePlayer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(speedx * horizontal, speedy * vertical);
        if(horizontal > 0f)
        {
            spritePlayer.flipX = false;

        }
        else if (horizontal < 0)
        {
            spritePlayer.flipX = true;
        }
    }
}
