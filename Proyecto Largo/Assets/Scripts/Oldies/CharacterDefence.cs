using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefence : MonoBehaviour
{
    public float defence;
    
    public void defaultDefence(float maxDefence)
    {
        defence = maxDefence;
    }

    public void decreaseDefence(float less)
    {
        defence -= less;
        if (defence < 0)
        {
            defence = 0;
        }
    }
}
