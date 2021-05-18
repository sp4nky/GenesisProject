using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBurbleManager : MonoBehaviour
{
    public Text damageValue;
    public Text defenceReductionValue;
    public Text damagePerTurnValue;
    public Text stunValue;
    //public GameObject burblePrefab;

    public void DefaultValues()
    {
        damageValue.text = "0";
        defenceReductionValue.text = "0";
        damagePerTurnValue.text = "0";
        stunValue.text = "No";
    }

    public void SetDanageSkill(float damage, float defenceReduction)
    {
        damageValue.text = damage.ToString();
        defenceReductionValue.text = defenceReduction.ToString();

    }

    public void SetDamagePerTurnSkill(float damagePerTunr, int turns)
    {
        damagePerTurnValue.text = damagePerTunr.ToString();
        if (turns != 0)
        {
            damagePerTurnValue.text += " por " + turns.ToString() + " turnos";
        }
    }

    public void SetStunSkill(int tuns)
    {
        stunValue.text = tuns + " turnos";
    }
}
