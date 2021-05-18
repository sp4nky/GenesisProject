using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipStatsUI : MonoBehaviour
{
    public Image weaponImage;
    public Text weaponStats;
    public Image armorImage;
    public Text armorStats;
    public Image helmetImage;
    public Text helmetStats;
    public Image bootsImage;
    public Text bootsStats;
    public Image shieldImage;
    public Text shieldStats;

    public void LoadWeaponStats(ItemDataEquip sword)
    {
        weaponImage.sprite = sword.sprite;
        weaponStats.text = sword.name + "\n" +
            sword.attack.ToString() + "\n" +
            sword.criticProb.ToString() + " % de crítico";
    }

    public void LoadArmorStats(ItemDataEquip armor)
    {
        armorImage.enabled = true;
        armorStats.enabled = true;
        armorImage.sprite = armor.sprite;
        armorStats.text = armor.name + "\n" +
            armor.criticProb.ToString() + " % de crítico" +
            armor.defence.ToString() + "+ defensa";
    }

    public void LoadBootsStats(ItemDataEquip boots)
    {
        bootsImage.enabled = true;
        bootsStats.enabled = true;
        bootsImage.sprite = boots.sprite;
        bootsStats.text = boots.name + "\n" +
            boots.criticProb.ToString() + " % de crítico" +
            boots.defence.ToString() + "+ defensa";
    }

    public void LoadHelmetStats(ItemDataEquip helmet)
    {
        helmetImage.enabled = true;
        helmetStats.enabled = true;
        helmetImage.sprite = helmet.sprite;
        helmetStats.text = helmet.name + "\n" +
            helmet.criticProb.ToString() + " % de crítico" +
            helmet.defence.ToString() + "+ defensa";
    }

    public void LoadShieldStats(ItemDataEquip shield)
    {
        shieldImage.enabled = true;
        shieldStats.enabled = true;
        shieldImage.sprite = shield.sprite;
        shieldStats.text = shield.name + "\n" +
            shield.criticProb.ToString() + " % de crítico" +
            shield.defence.ToString() + "+ defensa";
    }
}
