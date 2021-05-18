using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public Transform target;
    private FlyingPatrol fp;

    private void Awake()
    {
        fp = GetComponentInParent<FlyingPatrol>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fp.tarjetConfirmed)
        {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.right = -direction;
        }
        else
        {
            transform.right = Vector3.zero;
        }
    }
}
