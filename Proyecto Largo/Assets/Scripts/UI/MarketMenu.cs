using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenu : MonoBehaviour
{
    public GameObject buyPanel, sellPanel;
    private bool opened = false;
    public PlayerMovement movement;

    private void Start()
    {
        GameManagement.instance.marketMenu = this;
    }

    public void OpenCloseMarket(NPCMarket market)
    {
        if (!opened)
            Open(market);
        else
            Close(market);
    }

    public void Open(NPCMarket market)
    {
        movement.enabled = false;
        opened = true;
        buyPanel.gameObject.SetActive(true);
        sellPanel.gameObject.SetActive(true);
        GameManagement.instance.marketManager.LoadInventoryMarket();
        GameManagement.instance.marketManager.LoadMarket(market.inventory);
    }

    private void Close(NPCMarket market)
    {
        movement.enabled = true;
        opened = false;
        GameManagement.instance.marketManager.ClearInventoryMarket();
        GameManagement.instance.marketManager.ClearMarket();
        buyPanel.gameObject.SetActive(false);
        sellPanel.gameObject.SetActive(false);
    }
}
