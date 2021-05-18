using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletShoot : MonoBehaviour
{
    public GameObject bullet;
    public float timeCouldawn = 0.5f;
    public int direction { get; set;}
    private bool couldawn;


    void Update()
    {
        bool shoot= Input.GetButton("Shoot");
        if(!couldawn && shoot)
        {
            StartCoroutine(ShootBullet());
         
        }
    }

    private IEnumerator ShootBullet()
    {
        couldawn = true;
        yield return null;
        GameObject instanceBullet = Instantiate(bullet);
        instanceBullet.transform.position = transform.position;
        BulletMovement shootBullet = instanceBullet.GetComponent<BulletMovement>();
        shootBullet.direction = direction;
        yield return new WaitForSeconds(timeCouldawn);
        couldawn = false;
    }
}
