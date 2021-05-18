using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public int nivel = 1;
    public int exp = 0;
    public float expConstant = 0.1f;
    public float maxHp = 100;
    public float maxPhysicalDefence = 0;
    public float maxMagicalDefence = 0;
    public float maxMana = 50;
    public SkillData[] skills;
    public ItemDataEquip sword;
    public ItemDataEquip armor;
    public ItemDataEquip ring;    
    public ItemDataEquip boots;
    public ItemDataEquip helmet;
    private float defenceBuff;

    public void AddExp(int exp)
    {
        exp += exp;
        nivel = Mathf.CeilToInt(expConstant * Mathf.Sqrt(exp));
    }

    public void AddSkill(SkillData skill)
    {

        Array.Resize(ref skills, skills.Length + 1);
        skills[skills.Length - 1] = skill;
        
    }

    public void ChangeBasicAttack(SkillData skill)
    {
        skills[0] = skill;
    }


    public void ItemEquip(ItemDataEquip item)
    {
        if (item.type == ItemDataEquip.Equip.weapon)
            sword = item;
        if (item.type == ItemDataEquip.Equip.armor)
        {
            //maxPhysicalDefence -= armor.defence;
            armor = item;
        }
        if (item.type == ItemDataEquip.Equip.helmet)
        {
            //maxPhysicalDefence -= helmet.defence;
            helmet = item;
        }
        if (item.type == ItemDataEquip.Equip.boots)
        {
            //maxPhysicalDefence -= boots.defence;
            boots = item;
        }
        if (item.type == ItemDataEquip.Equip.ring)
        {
            //maxPhysicalDefence -= shield.defence;
            ring = item;
        }
    }

    public float CurrentDefence()
    {
        return maxPhysicalDefence +
        (armor ? armor.defence : 0) +
        (helmet ? helmet.defence : 0) +
        (boots ? boots.defence : 0) +
        (ring ? ring.defence :0) + defenceBuff;
    }

    public void decreaseDefence(float defenceReduction)
    {
        defenceBuff -= defenceReduction;
    }

    public void ClearBuff()
    {
        defenceBuff = 0;
    }

}
