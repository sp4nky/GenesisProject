using System;


[Serializable]
public class Hability
{
    private string name;
    private float damage;
    private float defenceReduction;
    public Hability(string name, float damage,float defenceReduction)
    {
        this.name = name;
        this.damage = damage;
        this.defenceReduction = defenceReduction;
    }

}
