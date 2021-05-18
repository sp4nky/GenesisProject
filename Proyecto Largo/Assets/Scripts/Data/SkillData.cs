using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite sprite;
    [Header("Requirements")]
    public int points;
    public SkillData skillBefore;
    [Header("Stats")]
    public bool meleeAttack;
    public float damage;
    public float defenceReduction;
    public bool aoe;
    public int mana;

    public Poison poison;
    public Bleeding bleeding;
    public Stun stun;
    public Burning burning;
    public Cure cure;

    [Serializable]
    public class Poison
    {
        public bool enable;
        public int damage;
    }

    [Serializable]
    public class Bleeding
    {
        public bool enable;
        [Range(0,1)]
        public float percentDamage;
        public int turns;
    }

    [Serializable]
    public class Stun
    {
        public bool enable;
        public int turns;
    }

    [Serializable]
    public class Burning
    {
        public bool enable;
        [Range(0, 1)]
        public float damage;
        public int turns;
    }   
    
    [Serializable]
    public class Cure
    {
        public bool enable;
        public int hpPlus;
    }

    public bool CanLearn(PlayerCharacterData playerCharacterData)
    {
        bool can = false;
        if(playerCharacterData.skillpoints>=points)
        {
            var listSkills = playerCharacterData.skills.ToList();

            if (skillBefore)
            {
                if (listSkills.Contains(skillBefore))
                    can = true;
            }
        }
        return can;
    }
}
