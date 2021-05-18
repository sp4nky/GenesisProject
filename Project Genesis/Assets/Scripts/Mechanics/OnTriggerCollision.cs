using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCollision : MonoBehaviour
{

    public UnityEvent OnPlayerCollision;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnPlayerCollision.Invoke();
            col.enabled = false;
        }

    }
}
