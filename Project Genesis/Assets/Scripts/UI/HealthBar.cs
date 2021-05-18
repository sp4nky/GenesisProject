using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public Transform bar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float percent = (float) health.hp / (float) health.maxHP;
        bar.localScale = new Vector3(percent,1,1);
    }
}
