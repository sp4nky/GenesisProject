using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(hiddePanelUI))]
public class InventoryCombatUI : MonoBehaviour
{
    public GameObject itemDataPrefav;
    public Transform buttonsParent;
    private List<GameObject> listitemsButtons = new List<GameObject>();
    private hiddePanelUI hiddePanel;
    public enum Type { Consumables, Equipamient}
    public Type itemsToShow;

    private void Awake()
    {
        hiddePanel = GetComponent<hiddePanelUI>();
    }

    public void LoadInventoryItemsButtons()
    {

        hiddePanel.Show();
        Inventory inventory = GameManagement.instance.inventory;
        clearItemsUIButtons();
        foreach (InventoryItem inventoryItem  in inventory.GetInventoryItems())
        {
            if (itemsToShow == Type.Consumables && inventoryItem.item is ItemDataCons)
            {
                    InstanceItemsUIButton(inventoryItem);
            }
            if (itemsToShow == Type.Equipamient && inventoryItem.item is ItemDataEquip)
            {
                    InstanceItemsUIButton(inventoryItem);
            }
        }

        hiddePanel.hidde();
    }

    private void InstanceItemsUIButton(InventoryItem invItem)
    {
        GameObject instanceButton = Instantiate(itemDataPrefav, buttonsParent);
        listitemsButtons.Add(instanceButton);
        instanceButton.transform.localScale = Vector3.one;
        ItemDataUI itemUI = instanceButton.GetComponent<ItemDataUI>();
        //No se puede usar el item en combate
        //if (itemsToShow == Type.Equipamient) itemUI.btn.onClick.RemoveAllListeners();
        itemUI.invItem = invItem;
    }

    public void clearItemsUIButtons()
    {
        hiddePanel.Show();
        foreach (GameObject btn in listitemsButtons)
            Destroy(btn);
        listitemsButtons.Clear();
        hiddePanel.hidde(); 
    }
}
