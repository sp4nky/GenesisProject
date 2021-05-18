using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10;
    public int direction { get; set; }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * Time.deltaTime * speed * direction);
    }
}
