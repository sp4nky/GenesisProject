using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseDoorTrigger : MonoBehaviour
{
    public bool playerCollisioned;
    public UnityEvent onTriggerPass;


    private void Start()
    {
        playerCollisioned = false;
    }
    public bool doorPassed
    {
        get { return playerCollisioned; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            playerCollisioned = true;
            onTriggerPass.Invoke();
        }
    }

}
