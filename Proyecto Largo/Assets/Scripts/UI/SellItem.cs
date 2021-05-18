using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    public Button sellButton;
    private ItemSelecter selecter;

    private void Awake()
    {
        selecter = GetComponent<ItemSelecter>();
    }

    void Start()
    {
        sellButton.onClick.AddListener(Sell);
    }

    private void Sell()
    {
        if (selecter.selectedItem)
        {
            Inventory invPlayer = GameManagement.instance.inventory;

            invPlayer.gold += selecter.selectedItem.price;
            invPlayer.RemoveItem(selecter.selectedItem);
            GameManagement.instance.marketManager.inventoryMarket.Additem(selecter.selectedItem);
            GameManagement.instance.marketManager.Reload();
        }
    }

    

}
