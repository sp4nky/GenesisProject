using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformInMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player")) collision.collider.transform.parent = transform;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (this.enabled && collision.collider.transform.parent == transform)
        {
            collision.collider.transform.parent = null;
        }
    }
}
