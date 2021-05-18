using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharacterBehaviour : MonoBehaviour
{
    public CharacterData characterData;
    private CharacterHealth chHealth;
    private CharacterMana chMana;
    private CharacterCombatAnimationSettings playerAnim;
    public string characterName;
    public float attack = 10;
    public float magic = 0;
    public float agitily = 0;
    public AnimationCurve animationCurveGo;
    public AnimationCurve animationCurveReturn;
    public ItemDataEquip sword { get { return characterData.sword; } }
    public ItemDataEquip armor { get { return characterData.armor; } }
    public ItemDataEquip ring { get { return characterData.ring; } }
    public ItemDataEquip boots { get { return characterData.boots; } }
    public ItemDataEquip helmet { get { return characterData.helmet; } }


    public int poisonDamage
    {
        get; set;
    }

    private SkillData affectedBySkill;



    private void Awake()
    {
        playerAnim = GetComponent<CharacterCombatAnimationSettings>();
        InitializeAffectedSkill();

        chHealth = GetComponent<CharacterHealth>();
        if (!chHealth) chHealth = gameObject.AddComponent<CharacterHealth>();

        chMana = GetComponent<CharacterMana>();
        if (!chMana) chMana = gameObject.AddComponent<CharacterMana>();

        if (gameObject.name == "PlayerCombat")
        {
            GameManagement.instance.playerBehaviour = this;
        }
    }

    public SkillData GetAffectedSkill()
    {
        return affectedBySkill;
    }
    public float getHealth()
    {
        return chHealth.hp;
    }

    public float GetPhysicalDefence()
    {
        return characterData.CurrentDefence();
    }

    public float GetMagicalDefence()
    {
        return characterData.CurrentDefence();
    }

    public float GetMana()
    {
        return chMana.mana;
    }

    public void InitializeAffectedSkill()
    {
        affectedBySkill = (SkillData)ScriptableObject.CreateInstance<SkillData>();
        affectedBySkill.skillName = "Affected Skill";
        affectedBySkill.poison = new SkillData.Poison();
        affectedBySkill.bleeding = new SkillData.Bleeding();
        affectedBySkill.stun = new SkillData.Stun();
        affectedBySkill.burning = new SkillData.Burning();
    }

    private void Start()
    {
        chHealth.defaultHp(characterData.maxHp);
        chMana.defaultMana(characterData.maxMana);
    }


    public float hitByBasicAttack(float damage)
    {
        //Formula de daño segun defensa
        float realDamage = damage - (GetPhysicalDefence() / 500) * damage;
        realDamage = Mathf.Clamp(realDamage, 0, damage);
        realDamage = Mathf.Round(realDamage);
        if (realDamage <= 0)
            realDamage = 0;
        DecreaseHp(realDamage);
        return realDamage;
    }

    private void HitAnimation()
    {
        if (playerAnim) playerAnim.AnimationHit();
      
    }

    public float hitByPhysicalDamage(SkillData skill)
    {
        characterData.decreaseDefence(skill.defenceReduction);
        return hitByBasicAttack(skill.damage);
    }


    public void UseItemEquip(ItemDataEquip item)
    {
        characterData.ItemEquip(item);
    }

    public void UseItemCons(ItemDataCons item)
    {
        RestoreHP(item.hpPlus);
        RestoreMana(item.manaPlus);
    }

    public void RestoreHP(float hp)
    {
        if (chHealth.hp + hp > characterData.maxHp)
            chHealth.defaultHp(characterData.maxHp);
        else
            chHealth.Health(hp);
    }

    public void RestoreMana(float mp)
    {
        if (chMana.mana + mp > characterData.maxMana)
            chMana.defaultMana(characterData.maxMana);
        else
            chMana.RestoreMana(mp);
    }

    public void DecreaseMana(int mana)
    {
        chMana.DecreaseMana(mana);
    }

    public void DecreaseHealth(int hp)
    {
        chHealth.DecreaseHp(hp);
    }

    public void AddAffectedSkill(SkillData skill)
    {
        if (!skill)
            return;
        //Update AffectedBySkill
        if (skill.poison.enable)
        {
            affectedBySkill.poison.enable = true; 
            affectedBySkill.poison.damage += skill.poison.damage;
        }
        if (skill.bleeding.enable)
        {
            affectedBySkill.bleeding.enable = true;
            if (affectedBySkill.bleeding.turns < skill.bleeding.turns)
                affectedBySkill.bleeding.turns = skill.bleeding.turns;
            if (affectedBySkill.bleeding.percentDamage < skill.bleeding.percentDamage)
                affectedBySkill.bleeding.percentDamage = skill.bleeding.percentDamage;
        }
        if (skill.stun.enable)
        {
            affectedBySkill.stun.enable = true;
            if (affectedBySkill.stun.turns < skill.stun.turns)
                affectedBySkill.stun.turns = skill.stun.turns;
        }
        if (skill.burning.enable)
        {
            affectedBySkill.burning.enable = true;
            if (affectedBySkill.burning.damage < skill.burning.damage)
                affectedBySkill.burning.damage = skill.burning.damage;
            affectedBySkill.burning.turns = skill.burning.turns;
        }
    }

    public void DecreaseHp(float damage)
    {
        chHealth.DecreaseHp(damage);
        HitAnimation();
        NotyfyDeath();
    }

    private void NotyfyDeath()
    {
        if (chHealth.hp <= 0)
            if (GameManagement.instance.eventsManager.OnCratureKill != null)
                GameManagement.instance.eventsManager.OnCratureKill.Invoke(characterName);
    }
}
