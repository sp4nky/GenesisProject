using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]

public class Character
{
    private string name;
    private float hp;
    private float maxHp;
    private float defence;

    public List<Hability> habilities = new List<Hability>();

    public string Name { get => name; set => name = value; }
    public float Hp { get => hp; set => hp = value; }
    public float Defence { get => defence; set => defence = value; }

    public Character(string name, float hp, float defence)
    {
        this.name = name;
        this.hp = hp;
        this.maxHp = hp;
        this.defence = defence;
    }

    public void heal(float heal)
    {
        this.hp += heal;   
    }

    public void hitByHability(Hability hability)
    {

    }

    public void addHability(Hability hability)
    {
        if(hability!=null)
        {
            habilities.Add(hability);
        }
    }

}
