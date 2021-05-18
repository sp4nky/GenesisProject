using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBullets : MonoBehaviour
{
    public GameObject BulletReflected;
    public float velocityOfBulletReflected = 10;
    public LayerMask enemyMask;

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
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            //Reflect bullet to enemy position
            Vector2 reflected;
            Vector2 pointCollision;
            GameObject feedBackInstance = Instantiate(BulletReflected);
            pointCollision = collision.contacts[0].point;
            feedBackInstance.transform.position = pointCollision;
            Collider2D enemyCol = Physics2D.OverlapCircle(transform.position, 100, enemyMask);
            if (enemyCol != null)
            {
                Debug.Log(enemyCol.tag);
                Vector2 facingDirection = enemyCol.gameObject.transform.position - transform.position;

                feedBackInstance.transform.up = facingDirection;
            }
            else
            {
                reflected = new Vector2(transform.position.x, transform.position.y) - pointCollision;
                feedBackInstance.transform.eulerAngles = new Vector3(0f, 0f, -Vector2.Angle(reflected, transform.position));
            }
            feedBackInstance.tag = "PlayerBullet";
            Rigidbody2D rb = feedBackInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = feedBackInstance.transform.up * velocityOfBulletReflected;
            }
        }
    }
}
