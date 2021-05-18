using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    public Button buyButton;
    private ItemSelecter selecter;

    private void Awake()
    {
        selecter = GetComponent<ItemSelecter>();
    }

    // Start is called before the first frame update
    void Start()
    {
        buyButton.onClick.AddListener(Buy);  
        
    }

    private void Buy()
    {
        if(selecter.selectedItem)
        {
            Inventory invPlayer = GameManagement.instance.inventory;
            if (selecter.selectedItem.price <= invPlayer.gold)
            {
                invPlayer.gold -= selecter.selectedItem.price;
                GameManagement.instance.AddItemInventory(selecter.selectedItem);
                GameManagement.instance.marketManager.inventoryMarket.RemoveItem(selecter.selectedItem);

                GameManagement.instance.marketManager.Reload();
                selecter.SetItem(null);
                
            }
            else
            {
                Debug.Log("No hay oro suficiente");
            }
        }
    }


}
