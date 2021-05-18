using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallToEndGame : MonoBehaviour
{
    private bool playerCollisioned = false;
    public bool finalGame
    {
        get { return playerCollisioned; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerCollisioned = true;
    }
}
