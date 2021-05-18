using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text characterName;
    public SizeableBar healthBar;
    public SizeableBar manaBar;
    private CharacterBehaviour character;

    void FixedUpdate()
    {
        if (character) SetStats(character);
    }

    public void Show() 
    {
        gameObject.SetActive(true);
    }

    public void Hidde()
    {
        gameObject.SetActive(false);
    }

    public void SetStats(CharacterBehaviour character)
    {
        this.character = character;
        characterName.text = character.characterName;
        healthBar.SetValue(character.getHealth(), character.characterData.maxHp);
        manaBar.SetValue(character.GetMana(), character.characterData.maxMana);
    }
}
