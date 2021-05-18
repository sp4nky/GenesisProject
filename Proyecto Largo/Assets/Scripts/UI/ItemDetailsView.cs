using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailsView : MonoBehaviour
{
    public Text itemPriceDetails;
    public Image itemImageDetails;
    public Text itemTextDetails;

    public void ShowItem(ItemData item)
    {
        if (item == null)
        {
            itemTextDetails.gameObject.SetActive(false);
            itemImageDetails.gameObject.SetActive(false);
            if (itemPriceDetails)
            {
                itemPriceDetails.gameObject.SetActive(false);
            }
            return;
        }

        itemTextDetails.gameObject.SetActive(true);
        itemImageDetails.gameObject.SetActive(true);
        itemImageDetails.sprite = item.sprite;
        if (itemPriceDetails)
        {
            itemPriceDetails.gameObject.SetActive(true);
            itemPriceDetails.text = "Precio: " + item.price;
            if (item.price <= GameManagement.instance.inventory.gold)
                itemPriceDetails.color = new Color(0, 1, 0);
            else
                itemPriceDetails.color = new Color(1, 0, 0);
        }
        if (item.GetType().Equals(typeof(ItemDataCons)))
        {
            ItemDataCons itemCons = (ItemDataCons)item;
            itemTextDetails.text = itemCons.itemName + "\n";
            if (itemCons.hpPlus > 0)
                itemTextDetails.text += "HP: +" + itemCons.hpPlus.ToString() + "\n";
            if (itemCons.manaPlus > 0)
                itemTextDetails.text += "MP: +" + itemCons.manaPlus.ToString() + "\n";
            if (itemCons.poisonCure)
                itemTextDetails.text += "Cura: Veneno\n";

        }
        else
        {
            ItemDataEquip itemEquip = (ItemDataEquip)item;
            itemTextDetails.text = itemEquip.itemName + "\n";
            if (itemEquip.attack > 0)
                itemTextDetails.text += "Ataque: +" + itemEquip.attack.ToString() + "\n";
            if (itemEquip.criticProb > 0)
                itemTextDetails.text += "Critico (%): +" + itemEquip.criticProb.ToString() + "\n";
            if (itemEquip.defence > 0)
                itemTextDetails.text += "Defensa: +" + itemEquip.defence.ToString() + "\n";
        }
    }
}
