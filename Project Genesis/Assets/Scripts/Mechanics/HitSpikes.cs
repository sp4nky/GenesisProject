using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitSpikes : MonoBehaviour
{
    public UnityEvent OnPlayerCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            OnPlayerCollision.Invoke();
        }
    }
}
