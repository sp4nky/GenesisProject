using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool buttonShield;
    private PlayerHealth health; //EVA - Agregar precondicion de que tenga Health 
    //public SpriteRenderer shieldSprite;
    public GameObject shield;
    //public Collider2D shieldCollider;
    public float cooldown = 2;
    public float shieldDuration = 1;
    private float nextShieldTime = 0;
    private float shieldActivatedTime = 0;
    private bool shieldActivated = false;
    private AudioController ac;

    private Animator animShield;
    private Collider2D colShield;

    private void Awake()
    {
        ac = GetComponent<AudioController>();
        health = GetComponent<PlayerHealth>();
        colShield = shield.GetComponent<Collider2D>();
        animShield = shield.GetComponent<Animator>();
    }
    void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        buttonShield = Input.GetButtonDown("Fire3");
        //Si se puede activar el escudo se activa al presionar la habilidad
        if(buttonShield && Time.time> nextShieldTime && !shieldActivated)
        {
            //Cambia el tag de la bullet que le quita vida a Untagged
            health.bulletTag = "Enemy";
            shield.SetActive(true);
            ac.playShield();
            shieldActivatedTime = Time.time + shieldDuration;
            shieldActivated = true;
            
        }
        //Si no esta activado
        if(!shieldActivated)
        {
            //Deja todo a su estado inicial
            shield.SetActive(false);
            health.bulletTag = "EnemyBullet";
        }
        else if (shieldActivatedTime < Time.time)
        {
            //Si se acabo el escudo se desactiva y se pone la habilidad en cooldown
            shieldActivated = false;
            nextShieldTime = Time.time + cooldown;
        }
            
    }

}
