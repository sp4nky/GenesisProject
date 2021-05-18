using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem 
{
    public ItemData item;
    public int count;

    public InventoryItem(ItemData item, int count)
    {
        this.item = item;
        this.count = count;
    }
}
