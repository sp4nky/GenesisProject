using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory
{
    private List<InventoryItem> items= new List<InventoryItem>();
    public int gold = 0;

    public List<InventoryItem> GetInventoryItems()
    {
        return items;
    }

    public void Additem(ItemData item)
    {
        InventoryItem itemFind = ContainItem(item);
        if (itemFind == null)
        {
            InventoryItem invItem = new InventoryItem(item, 1);
            items.Add(invItem);
        }
        else
        {
            itemFind.count++;
        }
    }

    public void Additem(ItemData item, int count)
    {
        InventoryItem itemFind = ContainItem(item);
        if (itemFind == null)
        {
            InventoryItem invItem = new InventoryItem(item, count);
            items.Add(invItem);
        }
        else
        {
            itemFind.count += count;
        }
    }

    public void RemoveItem(ItemData item)
    {
        InventoryItem itemFind = ContainItem(item);
        if (itemFind != null)
        {
            if (itemFind.count > 1)
                itemFind.count--;
            else
            {
                items.Remove(itemFind);
                itemFind.count = 0;
            }
        }
    }

    public InventoryItem ContainItem(ItemData item)
    {
        //devuelve el item con el mismo nombre
        InventoryItem itemFind = null;
        foreach (InventoryItem invItem in items)
        {
            if (item.itemName == invItem.item.itemName)
            {
                itemFind = invItem;
            }
        }
        return itemFind;
    }

}
