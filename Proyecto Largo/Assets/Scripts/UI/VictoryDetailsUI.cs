using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDetailsUI : MonoBehaviour
{
    public Text exp;
    public Text nivel;
    public Text levelUpTitle;
    public Text levelUpTitileValue;

    public void SetExp(int exp)
    {
        this.exp.text = exp.ToString();
    }

    public void SetNivel(int nivel)
    {
        this.nivel.text = nivel.ToString();
    }

    public void EnableLevelUp()
    {
        nivel.gameObject.SetActive(true);
        levelUpTitle.gameObject.SetActive(true);
        levelUpTitileValue.gameObject.SetActive(true);
    }

    public void DefaultValue()
    {
        nivel.gameObject.SetActive(false);
        levelUpTitle.gameObject.SetActive(false);
        levelUpTitileValue.gameObject.SetActive(false);
    }


}
