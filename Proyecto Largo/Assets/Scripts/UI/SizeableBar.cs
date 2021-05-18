using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeableBar : MonoBehaviour
{
    public void SetValue(float hp, float hpMax)
    {
        float scale = hp / hpMax;
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);

    }

    public void Clear()
    {
        transform.localScale = new Vector3(0 , transform.localScale.y, transform.localScale.z);
    }
}
