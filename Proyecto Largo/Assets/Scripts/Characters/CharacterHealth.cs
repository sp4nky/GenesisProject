using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float hp
    { get; private set; }

    public void defaultHp(float hpMax)
    {
        hp = hpMax;
    }

    public void Health(float plusHp)
    {
        hp += plusHp;
    }

    public void DecreaseHp(float less)
    {
        hp -= less;
        if(hp < 0)
        {
            hp = 0;
        }
    }
}
