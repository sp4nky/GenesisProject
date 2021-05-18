using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Height=5;
    public float timeVel=2;
    private float t = 0;
    private enum State {opening, closing, waiting}
    private State actualState = State.waiting;
    private Vector3 startPosition;
    private Vector3 DestinyPosition;
    float x = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(actualState == State.opening)
        {
            t += Time.deltaTime;
            x = t / timeVel;
            transform.position = Vector2.Lerp(startPosition, DestinyPosition, x);
        }
        if(actualState == State.closing)
        {

            t += Time.deltaTime;
            x = t / timeVel;
            transform.position = Vector2.Lerp(startPosition, DestinyPosition, x);
        }
        if (x >= 1)
        {
            actualState = State.waiting;
            x = 0;
            t = 0;
        }


    }

    public void openDoor()
    {
        startPosition = transform.position;
        DestinyPosition = new Vector3(startPosition.x, startPosition.y + Height, 0);
        actualState = State.opening;
    }

    public void closeDoor()
    {
        startPosition = transform.position;
        DestinyPosition = new Vector3(startPosition.x, startPosition.y - Height, 0);
        actualState = State.closing;
    }
}
