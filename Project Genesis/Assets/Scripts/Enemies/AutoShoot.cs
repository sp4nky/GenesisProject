using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 10;
    
    public Transform firePos;
    public float rateOfFire = 2;
    private float nextTimeToShoot=0;
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
        if(Time.time > nextTimeToShoot && fp.tarjetConfirmed)
        {
            StartCoroutine(EnemyFire());
            nextTimeToShoot = Time.time + rateOfFire;
        }

        
    }

    private IEnumerator EnemyFire()
    {
        yield return new WaitForSeconds(1);
        GameObject feedBackInstance = Instantiate(bullet);
        feedBackInstance.transform.position = firePos.transform.position;

        feedBackInstance.transform.up = -firePos.transform.right;
        feedBackInstance.tag = "EnemyBullet";


        Rigidbody2D rb = feedBackInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -firePos.transform.right * bulletSpeed;
        }
    }
}
