using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public string unitName;
    public float maxHp;
    public float defence;
    public SkillData[] skills;
}
