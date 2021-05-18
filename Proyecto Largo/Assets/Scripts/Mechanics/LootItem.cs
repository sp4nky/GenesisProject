using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class LootItem : MonoBehaviour
{
    public UnityEvent OnPlayerCollision;
    public ItemData item;
    public int gold = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (item)
            {
                GameManagement.instance.itemsManager.AddItem(item);
            }
            if(gold > 0)
            {
                GameManagement.instance.inventory.gold += gold;
            }
            gameObject.SetActive(false);

        }

    }

}
