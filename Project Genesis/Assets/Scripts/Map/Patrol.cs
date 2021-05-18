using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 1;
    public Vector3 pointA;
    public Vector3 pointB;

    public Vector3 tarjetPoint;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pointA = transform.position - Vector3.right * 2;
        pointB = transform.position + Vector3.right * 2;
        tarjetPoint = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if(tarjetPoint.x < transform.position.x)
        {
            rb.velocity = Vector2.right * -speed;
        }
        if(tarjetPoint.x > transform.position.x)
        {
            rb.velocity = Vector2.right * speed;
        }

        if (Mathf.Floor(transform.position.x) == Mathf.Floor(pointA.x))
            tarjetPoint = pointB;
        if (Mathf.Floor(transform.position.x) == Mathf.Floor(pointB.x))
            tarjetPoint = pointA;

    }
}
