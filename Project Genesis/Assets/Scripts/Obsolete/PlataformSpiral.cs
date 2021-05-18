using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSpiral : MonoBehaviour
{
    public Rigidbody2D rbexternalPlataform1;
    public Rigidbody2D rbexternalPlataform2;
    public Rigidbody2D rbinternalPlataform1;
    public Rigidbody2D rbinternalPlataform2;



    private Vector3 externalTarjetPoint;
    private List<Vector3> externalPoints= new List<Vector3>();
    private Vector3 internalTarjetPoint;
    private List<Vector3> internalPoints = new List<Vector3>();
    public float speed=5;
    private float internalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        float radio1 = Vector3.Distance(transform.position, rbexternalPlataform1.position);
        float radio2 = Vector3.Distance(transform.position, rbinternalPlataform1.position);
        internalSpeed = (speed * radio2 / radio1);

        //InternalBlocks
        internalPoints.Add(rbinternalPlataform1.position);
        internalPoints.Add(new Vector3(rbinternalPlataform1.position.x, rbinternalPlataform2.position.y));
        internalPoints.Add(rbinternalPlataform2.position);
        internalPoints.Add(new Vector3(rbinternalPlataform2.position.x, rbinternalPlataform1.position.y));

        internalTarjetPoint = internalPoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        internalBlocksMovement();
    }

    void internalBlocksMovement()
    {
        //Si va para -->B
        if (internalTarjetPoint.x > Mathf.Round(rbinternalPlataform1.position.x) &&  internalTarjetPoint.y == Mathf.Clamp(internalTarjetPoint.y,rbinternalPlataform1.position.y, rbinternalPlataform1.position.y+.5f))
        {
            rbinternalPlataform1.velocity = Vector2.right * internalSpeed;
            rbinternalPlataform2.velocity = Vector2.right * -internalSpeed;
            rbexternalPlataform1.velocity = Vector2.right * (float)speed;
            rbexternalPlataform2.velocity = Vector2.right * (float)-speed;
        }
        //Si va para /|\C
        if (internalTarjetPoint.x == Mathf.Clamp(internalTarjetPoint.x,rbinternalPlataform1.position.x, rbinternalPlataform1.position.x+ .5f) && internalTarjetPoint.y > Mathf.Round(rbinternalPlataform1.position.y))
        {
            rbinternalPlataform1.velocity = Vector2.up * internalSpeed;
            rbinternalPlataform2.velocity = Vector2.up * -internalSpeed;
            rbexternalPlataform1.velocity = Vector2.up * (float)speed;
            rbexternalPlataform2.velocity = Vector2.up * (float)-speed;
        }
        //Si va para D<--
        if (internalTarjetPoint.x < Mathf.Round(rbinternalPlataform1.position.x) && internalTarjetPoint.y == Mathf.Clamp(internalTarjetPoint.y,rbinternalPlataform1.position.y, rbinternalPlataform1.position.y+ .5f))
        {
            rbinternalPlataform1.velocity = Vector2.right * -internalSpeed;
            rbinternalPlataform2.velocity = Vector2.right * internalSpeed;
            rbexternalPlataform1.velocity = Vector2.right * (float)-speed;
            rbexternalPlataform2.velocity = Vector2.right * (float)speed;
        }
        //Si va para A\|/
        if (internalTarjetPoint.x == Mathf.Clamp(internalTarjetPoint.x,rbinternalPlataform1.position.x, rbinternalPlataform1.position.x+ .5f) && internalTarjetPoint.y < Mathf.Round(rbinternalPlataform1.position.y))
        {
            rbinternalPlataform1.velocity = Vector2.up * -internalSpeed;
            rbinternalPlataform2.velocity = Vector2.up * internalSpeed;
            rbexternalPlataform1.velocity = Vector2.up * (float)-speed;
            rbexternalPlataform2.velocity = Vector2.up * (float)speed;
        }
        for (int i = 0; i < internalPoints.Count; i++)
        {
            if (Vector3.Distance(internalPoints[i], rbinternalPlataform1.position)<0.1f)
            {
                if (i + 1 == internalPoints.Count)
                    internalTarjetPoint = internalPoints[0];
                else
                    internalTarjetPoint = internalPoints[i + 1];
                return;
            }
        }
    }



}
