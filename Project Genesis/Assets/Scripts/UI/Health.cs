using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int hp = 1;
    public int maxHP = 3;
    public string enemyTag = "Player";
    public string bulletTag = "PlayerBullet";
    public UnityEvent OnNoHealth;
    public UnityEvent OnHit;
    public StunEvent OnStun;
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        hp = maxHP;
        OnStun.RemoveAllListeners();
        OnStun.AddListener(AttackEffect);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (/*collision.collider.CompareTag(enemyTag) ||*/ collision.collider.CompareTag(bulletTag))
        {
            hp -= 1;
            OnHit.Invoke();
        }
        if (hp < 0)
            hp = 0;

    }

    
    private void Update()
    {
        if (hp <= 0)
        {
            tag = "Untagged";
            //Plataform Layer is 18
            gameObject.layer = 18;
            OnNoHealth.Invoke();
            NotifyEnemyDeath();

        }
    }

    public void NotifyEnemyDeath()
    {
        if (GameController.instance.events.OnCratureKill != null)
            GameController.instance.events.OnCratureKill.Invoke("Enemy");
    }

    private void AttackEffect(Vector3 startPoint)
    {
        if (anim) anim.SetBool("Hit", true); 
        Vector3 direction = transform.position - startPoint;
        direction = direction.normalized;
        rb.velocity = direction * (500 / rb.mass);
    }

    public void disableObject()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public class StunEvent: UnityEvent<Vector3> { }
}
