using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{

    public float time = 1;
    public bool DestroyOnCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", time);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(DestroyOnCollision)
        {
            Destroy(gameObject);
        }
    }


}
