using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCMarket : MonoBehaviour
{
    public Inventory inventory
    {
        get; private set;
    }
    public List<ItemData> itemsMarket = new List<ItemData>();
    public bool loadItemsFromResources;

    private void Start()
    {
        if (loadItemsFromResources)
            LoadResourcesItems();
        CreateInventory();
    }

    private void LoadResourcesItems()
    {
        itemsMarket = Resources.LoadAll<ItemData>("Data/Items").ToList();
    }

    private void CreateInventory()
    {
        inventory = new Inventory();
        foreach (ItemData item in itemsMarket)
        {
            var count = IsConsumable(item) ? 10 : 1;
            inventory.Additem(item, count);
        }
    }

    private static bool IsConsumable(ItemData item)
    {
        return item is ItemDataCons;
    }
}
