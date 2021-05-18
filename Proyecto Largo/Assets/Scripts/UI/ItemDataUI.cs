using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemDataUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button btn;
    public Text itemName;
    public Text count;
    public Image img;
    public GameObject burbleItemPrefav;
    private GameObject burble;


    public InventoryItem invItem
    {
        set; get;
    }

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        if(invItem.item is ItemDataCons) btn.onClick.AddListener(UseItem);
        itemName.text = invItem.item.itemName;
        img.sprite = invItem.item.sprite;
    }

    private void Update()
    {
        if (count != null && invItem != null && invItem.item is ItemDataCons) count.text = "X" + invItem.count.ToString();
    }

    public void UseItem()
    {
        GameManagement.instance.UseItem(invItem.item);
        GameManagement.instance.inventory.RemoveItem(this.invItem.item);
        if (this.invItem.count < 1)
            //RemoveItemData();
            Destroy(gameObject);
        GameManagement.instance.combat.inventoryCombatUI.LoadInventoryItemsButtons();
        GameManagement.instance.stats.LoadCurrentStats();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (burble)
        {
            burble.SetActive(true);
            return;
        }
        burble = Instantiate(burbleItemPrefav, transform, false);
        burble.transform.localScale = Vector3.one;
        RectTransform burbleRectTransform = burble.GetComponent<RectTransform>();
        burbleRectTransform.localPosition = Vector3.zero;
        ItemBurbleManager burbleManager = burble.GetComponent<ItemBurbleManager>();
        burbleManager.SetItemsValues(invItem.item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (burble)
            burble.SetActive(false);
    }

    private void OnDestroy()
    {
        if (burble)
            Destroy(burble);
    }
}
