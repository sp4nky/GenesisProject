using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StartToRain : MonoBehaviour
{
    public RainScript2D rainScript;
    public enum RainType {Stop, Weak, Medium, Heavy}
    public RainType rainForce;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            float actualIntensity = rainScript.RainIntensity;
            switch (rainForce)
            {
                case RainType.Stop:
                    GameManagement.instance.rainManager.RainIntensity(actualIntensity, 0f);
                    break;

                case RainType.Weak:
                    GameManagement.instance.rainManager.RainIntensity(actualIntensity, 0.20f);
                break;

                case RainType.Medium:
                    GameManagement.instance.rainManager.RainIntensity(actualIntensity, 0.60f);
                    break;

                case RainType.Heavy:
                    GameManagement.instance.rainManager.RainIntensity(actualIntensity, 0.90f);
                    break;
            }
            col.enabled = false;
        }
    }

}
