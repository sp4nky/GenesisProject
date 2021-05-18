using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSelecter : MonoBehaviour
{
    public ItemDetailsView itemDetails;
    public ItemData selectedItem;

    private void Start()
    {
        List<ItemSlot> slots = GetComponentsInChildren<ItemSlot>(true).ToList();
        foreach (ItemSlot slot in slots)
        {
            slot.onClickItem.RemoveAllListeners();
            slot.onClickItem.AddListener(SelectItem);
        }
    }

    public void SelectItem(ItemSlot slot)
    {
        ItemSelecter select = GetComponentInParent<ItemSelecter>();
        if (select)
        {
            if (slot.item != null)
                select.SetItem(slot.item.item);
            else
                select.SetItem(null);
        }
    }

    public void SetItem(ItemData item)
    {
        selectedItem = item;
        itemDetails.ShowItem(item);
    }
}
