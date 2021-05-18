using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject panel;
    bool opened = false;
    public PlayerMovement movement;
    //public ItemDetailsView itemDetails;

    void Update()
    {
        bool open = Input.GetButtonDown("Inventory");

        if (open && !opened)
        {
            movement.enabled = false;
            opened = true;
            panel.gameObject.SetActive(true);
        }
        else
        {
            if (open && opened)
            {
                movement.enabled = true;
                opened = false;
                panel.gameObject.SetActive(false);

            }
        }
    }
/*
    public void ShowItem(ItemData item)
    {
        itemDetails.ShowItem(item);
    }
*/
}
