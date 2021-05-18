using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGold : MonoBehaviour
{
    private Text gold;

    private void Awake()
    {
        gold = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        gold.text = GameManagement.instance.inventory.gold.ToString();
    }
}
