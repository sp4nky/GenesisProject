using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDataEquip : ItemData
{
    public float attack;
    public float criticProb;
    public float defence;
    public enum Equip {weapon, ring, helmet, boots, armor };
    public Equip type; 
}
