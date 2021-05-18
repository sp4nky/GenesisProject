using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCollision : MonoBehaviour
{
    
    public UnityEvent OnPlayerCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnPlayerCollision.Invoke();
        }

    }

}
