using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public GameObject aureola;
    public GameObject marcador;
    private bool select=false;
    private CharacterBehaviour character;
    public Transform characterParent;
    public UISlot ui;
    private GameObject characterInstance;
    public Transform hitPosition;

    // Start is called before the first frame update
    void Start()
    {
        aureola.SetActive(false);
        marcador.SetActive(false);
        CharacterBehaviour actualCharacter = characterParent.GetComponentInChildren<CharacterBehaviour>();
        if (actualCharacter)
        {
            character = actualCharacter;
            characterInstance = actualCharacter.gameObject;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (characterInstance)
        {
            CharacterHealth chHealth = characterInstance.GetComponent<CharacterHealth>();
            float hp = chHealth.hp;
            ui.setHp(hp);
            aureola.SetActive(select);
            /*if(hp==0)
            {
                characterInstance = null;

                gameObject.SetActive(false);
            }*/
        }
    }

    public Transform GetHitPosition()
    {
        return hitPosition;
    }

    public float GetHP()
    {
        CharacterHealth chHealth = characterInstance.GetComponent<CharacterHealth>();
        return chHealth.hp;
    }

    public CharacterCombatAnimationSettings GetAnimationSettings()
    {
        return characterInstance.GetComponent<CharacterCombatAnimationSettings>();
    }


    public void slotSelected()
    {
        select = true;
    }

    public void characterMark()
    {
        marcador.SetActive(true);
    }

    public void characterMarkDeselected()
    {
        marcador.SetActive(false);
    }

    public void slotDeselected()
    {
        select = false;
    }

    public void SetCharacter(GameObject character)
    {
        character.transform.parent = characterParent;
        character.transform.localPosition = Vector3.zero;
        character.transform.localScale = Vector3.one;
        this.character = character.GetComponent<CharacterBehaviour>();
    }

    public CharacterBehaviour getCharacter()
    {
        return character;
    }

    public UISlot GetUISlot()
    {
        return ui;
    }

    public ParticleEffectsManager GetParticleEffectsManager()
    {
        if (characterInstance)
            return character.GetComponent<ParticleEffectsManager>();
        else
            return null;
    }

    public void SetDamageHit(float damage)
    {
        ui.SetDamageHit(damage);
    }

    public void setCharacter(GameObject character)
    {
        characterInstance = Instantiate(character);
        SetCharacter(characterInstance);
    }

    public void RemoveCharacterInstance()
    {
        Destroy(characterInstance);
    }

}
