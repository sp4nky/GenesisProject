using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUI : MonoBehaviour
{
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    private ItemSlot[] slotsMarketCons;
    public GameObject itemsMarketConsParent;

    private ItemSlot[] slotsMarketEquip;
    public GameObject itemsMarketEquipParent;

    private ItemSlot[] slotsInvCons;
    public GameObject itemsInvConsParent;

    private ItemSlot[] slotsInvEquip;
    public GameObject itemsInvEquipParent;

    public Inventory inventoryMarket;


    private void Awake()
    {
        GameManagement.instance.marketManager = this;
        slotsMarketCons = itemsMarketConsParent.GetComponentsInChildren<ItemSlot>();
        slotsMarketEquip = itemsMarketEquipParent.GetComponentsInChildren<ItemSlot>();
        slotsInvCons = itemsInvConsParent.GetComponentsInChildren<ItemSlot>();
        slotsInvEquip = itemsInvEquipParent.GetComponentsInChildren<ItemSlot>();
    }

    public void Reload()
    {
        ClearInventoryMarket();
        ClearMarket();
        LoadInventoryMarket();
        LoadMarket(inventoryMarket);
    }

    public void ClearInventoryMarket()
    {
        foreach (ItemSlot slot in slotsInvCons)
        {
            slot.RemoveItemData();
        }

        foreach (ItemSlot slot in slotsInvEquip)
        {
            slot.RemoveItemData();
        }
    }

    public void ClearMarket()
    {
        foreach (ItemSlot slot in slotsMarketCons)
        {
           
            slot.RemoveItemData();
        }

        foreach (ItemSlot slot in slotsMarketEquip)
        {
            slot.RemoveItemData();
        }

    }

    public void LoadInventoryMarket()
    {
        inventoryItems = GameManagement.instance.inventory.GetInventoryItems();
        foreach (InventoryItem invIt in inventoryItems)
        {
            if (invIt.item is ItemDataCons)
            {
                foreach (ItemSlot slot in slotsInvCons)
                {
                    if (slot.item == null || !slot.item.item)
                    {
                        slot.SetItemData(invIt.item, GameManagement.instance.inventory);
                        break;
                    }
                    
                }
            }
            else
            {
                foreach (ItemSlot slot in slotsInvEquip)
                {
                    if (slot.item == null || !slot.item.item)
                    {
                        slot.SetItemData(invIt.item, GameManagement.instance.inventory);
                        break;
                    }
                }
            }
            
        }
    }

    public void LoadMarket(Inventory inventory)
    {
        inventoryMarket = inventory;
        inventoryItems = inventory.GetInventoryItems();
        foreach (InventoryItem invIt in inventoryItems)
        {
            if (invIt.item is ItemDataCons)
            {
                foreach (ItemSlot slot in slotsMarketCons)
                {
                    if (slot.item == null || !slot.item.item)
                    {
                        slot.SetItemData(invIt.item, inventory);
                        break;
                    }
                }
            }
            else
            {
                foreach (ItemSlot slot in slotsMarketEquip)
                {
                    if (slot.item == null || !slot.item.item)
                    {
                        slot.SetItemData(invIt.item, inventory);
                        break;
                    }
                }
            }
        }
    }


}
