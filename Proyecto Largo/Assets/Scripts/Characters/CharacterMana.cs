
using UnityEngine;

public class CharacterMana : MonoBehaviour
{
    public float mana;

    public void defaultMana(float hpMax)
    {
        mana = hpMax;
    }

    public void RestoreMana(float plusHp)
    {
        mana += plusHp;
    }

    public void DecreaseMana(float less)
    {
        mana -= less;
        if (mana < 0)
        {
            mana = 0;
        }
    }
}
