using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBurbleManager : MonoBehaviour
{
    public Text title;
    public Text description;
    public Image img;

    public void SetItemsValues(ItemData item)
    {
        title.text = item.itemName;
        img.sprite = item.sprite;
        description.text = "";
        if (item is ItemDataCons)
        {
            ItemDataCons itemCons = (ItemDataCons)item;
            if (itemCons.hpPlus > 0)
            {
                description.text += "HP: +" + itemCons.hpPlus + "\n";
            }
            if (itemCons.manaPlus > 0)
            {
                description.text += "MP: +" + itemCons.manaPlus + "\n";

            }
            if (itemCons.poisonCure)
            {
                description.text += "Cura el veneno\n";
            }
        }
        else
        {
            ItemDataEquip itemEquip = (ItemDataEquip)item;
            if (itemEquip.attack >0)
                description.text += "Ataque: +" + itemEquip.attack + "\n";
            if (itemEquip.defence > 0)
                description.text += "Defensa: +" + itemEquip.defence + "\n";
            if (itemEquip.criticProb > 0)
                description.text += "Probabilidad de Critico: +" + itemEquip.criticProb + "\n";
        }

    }
}
