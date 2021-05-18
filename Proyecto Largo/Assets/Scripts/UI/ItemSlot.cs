using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    public bool selectOnPointerEnter = true;
    public Image imageItem;
    public Text itemCount;
    public ItemSlotEvent onClickItem;
    public InventoryItem item
    {
        get; private set;
    }
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }


    private void Update()
    {
        if (imageItem.sprite != null && item != null && item.item is ItemDataCons)
        {
            itemCount.text = "x" + this.item.count.ToString();
        }
    }


    public void SetItemData(ItemData item, Inventory inventory)
    {
        this.item = inventory.ContainItem(item);
        imageItem.enabled = true;
        itemCount.enabled = true;
        imageItem.sprite = item.sprite;
    }

    public void RemoveItemData()
    {
        if (item != null)
        {
            item = null;
            imageItem.sprite = null;
            itemCount.enabled = false;
            imageItem.enabled = false;
            onClickItem.Invoke(this);
        }
    }

    public void UseItem()
    {
        if(imageItem.sprite != null)
        {

            
            if (item.item is ItemDataCons)
            {
                GameManagement.instance.UseItem(item.item);
                GameManagement.instance.inventory.RemoveItem(this.item.item);
                if(this.item.count < 1)
                    RemoveItemData();
            }
            else
            {
                ItemDataEquip oldEquip = null;
                ItemDataEquip currentItem = (ItemDataEquip)item.item;
                if (currentItem.type == ItemDataEquip.Equip.weapon)
                    oldEquip = GameManagement.instance.playerBehaviour.sword;
                if (currentItem.type == ItemDataEquip.Equip.armor)
                    oldEquip = GameManagement.instance.playerBehaviour.armor;
                if (currentItem.type == ItemDataEquip.Equip.ring)
                    oldEquip = GameManagement.instance.playerBehaviour.ring;
                if (currentItem.type == ItemDataEquip.Equip.helmet)
                    oldEquip = GameManagement.instance.playerBehaviour.helmet;
                if (currentItem.type == ItemDataEquip.Equip.boots)
                    oldEquip = GameManagement.instance.playerBehaviour.boots;
                if (oldEquip)
                {
                    GameManagement.instance.UseItem(item.item);
                    item.item = oldEquip;
                    imageItem.sprite = oldEquip.sprite;
                    SelectItem(item);
                }
                else
                {
                    GameManagement.instance.UseItem(item.item);
                    GameManagement.instance.inventory.RemoveItem(this.item.item);
                    RemoveItemData();
                }
            }
            GameManagement.instance.stats.LoadCurrentStats();
        }
    }

    public void SelectItem(InventoryItem item)
    {
        /*
        ItemSelecter select = GetComponentInParent<ItemSelecter>();
        if (select)
        {
            if (item != null)
                select.SetItem(item.item);
            else
                select.SetItem(null);
        }
        */
        onClickItem.Invoke(this);
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }

    public void OnSelect(BaseEventData eventData)
    {
        SelectItem(item);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (selectOnPointerEnter)
            SelectItem(item);
    }

    [Serializable]
    public class ItemSlotEvent : UnityEvent<ItemSlot> { }
}
