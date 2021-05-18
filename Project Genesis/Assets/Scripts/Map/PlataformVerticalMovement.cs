using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformVerticalMovement : MonoBehaviour
{
    public float speed = 1;
    private Vector3 pointA;
    private Vector3 pointB;
    public float distance = 20;
    private Vector3 tarjetPoint;
    private Rigidbody2D rb;
    public bool invertMovement = false;
    private float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pointA = new Vector3(transform.position.x, transform.position.y + distance, 0);
        pointB = transform.position; 
        tarjetPoint = pointA;
        if (invertMovement)
        {
            pointB = new Vector3(transform.position.x, transform.position.y - distance, 0);
            pointA = transform.position;
            tarjetPoint = pointB;

        }
    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime * speed * 0.1f;
        if (tarjetPoint == pointA)
            transform.position = Vector3.Lerp(pointB, tarjetPoint, t);
        else
            transform.position = Vector3.Lerp(pointA, tarjetPoint, t);
        if (t >= 1)
        {
            if (tarjetPoint == pointA)
                tarjetPoint = pointB;
            else
                tarjetPoint = pointA;
            t = 0;
        }
        /*
        if (tarjetPoint.y < transform.position.y)
        {
            rb.velocity = Vector2.up * -speed;
        }
        if (tarjetPoint.y > transform.position.y)
        {
            rb.velocity = Vector2.up * speed;
        }

        if (Mathf.Floor(transform.position.y) == Mathf.Floor(pointA.y))
            tarjetPoint = pointB;
        if (Mathf.Floor(transform.position.y) == Mathf.Floor(pointB.y))
            tarjetPoint = pointA;
            */
    }
}
