using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public GameObject itemsParent;
    private ItemSlot[] items;
    private LogManager log;

    private void Awake()
    {
        GameManagement.instance.itemsManager = this;
        items = itemsParent.GetComponentsInChildren<ItemSlot>();
        log = GetComponent<LogManager>();

    }
    
    public void AddItem(ItemData item)
    {
        InventoryItem invIt = GameManagement.instance.inventory.ContainItem(item);
        GameManagement.instance.inventory.Additem(item);

        if (invIt != null)
        {
            foreach (ItemSlot slot in items)
            {
                if (slot.item.item==item)
                {
                    slot.SetItemData(item, GameManagement.instance.inventory);
                    break;
                }
            }
        }
        else
        {
            foreach (ItemSlot slot in items)
            {
                if (slot.item==null || !slot.item.item)
                {
                    slot.SetItemData(item, GameManagement.instance.inventory);
                    break;
                }
            }
        }
        log.setLogRow("+1 " + item.itemName);
    }

    public void RemoveItem(ItemData item)
    {
        InventoryItem invIt = GameManagement.instance.inventory.ContainItem(item);
        if (invIt.item)
        {

            foreach (ItemSlot slot in items)
            {
                if (slot.item.item == item)
                {
                    slot.RemoveItemData();
                    break;
                }
            }
            GameManagement.instance.inventory.RemoveItem(item);

        }
    }
}
