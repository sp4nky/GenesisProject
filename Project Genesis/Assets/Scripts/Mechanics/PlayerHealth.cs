using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int hp = 1;
    public int maxHP = 3;
    public string enemyTag = "Enemy";
    public string enemyTag2 = "Brutus"; 
    public string enemyTag3 = "T453R";
    public string bulletTag = "EnemyBullet";
    public bool destroyIfNoHP = true;
    private Rigidbody2D rb;
    public UnityEvent OnHealthLoss;
    public float invulnetableTime = 1f;
    public bool inmune = false;

    private LayerMask enemyMask;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag(enemyTag) || collision.collider.CompareTag(enemyTag2) || collision.collider.CompareTag(enemyTag3) || collision.collider.CompareTag(bulletTag)) & !inmune)
        {
            hp -= 1;
            OnHealthLoss.Invoke();
            enemyMask = collision.gameObject.layer;
            StartCoroutine(invulnerable());
            if (!collision.collider.CompareTag(bulletTag))
            {
                
                if (collision.contacts[0].point.x > transform.position.x)
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(new Vector2(-20f, 20f), ForceMode2D.Impulse);
                }

                else
                {
                    rb.velocity = Vector2.zero;
                    rb.AddForce(new Vector2(20f, 20f), ForceMode2D.Impulse);
                }

            }
            

        }

        if (hp < 0)
        { 
            hp = 0;
            }
    }

    private IEnumerator invulnerable()
    {
        inmune = true;
        yield return new WaitForSeconds(invulnetableTime);
        inmune = false;
    }
    
    private void reactivateCollision()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyMask, false);
    }

    private void Update()
    {
        destroyIfnoHealth();
    }

    public void destroyIfnoHealth()
    {
        // If hp is less than 0 then destroy the object
        if (hp == 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Jump>().enabled = false;
            rb.velocity = Vector2.zero;
            Invoke("hiddePlayer", 1.5f);
        }
    }

    public void hiddePlayer()
    {
        gameObject.SetActive(false);
    }

    public void kill()
    {
        hp = 0;
    }

    public void refillHealth()
    {
        hp = 3;
    }

}
