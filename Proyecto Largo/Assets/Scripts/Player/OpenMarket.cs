using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMarket : MonoBehaviour
{
    private NPCSensor sensor;

    private void Awake()
    {
        sensor = GetComponent<NPCSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        bool open = Input.GetButtonDown("Market");
        if(open)
        {
            NPCInteraction interaction = sensor.NearbyNPCInteraction();
            if(interaction && interaction.market)
            {
                GameManagement.instance.marketMenu.OpenCloseMarket(interaction.market);
            }
        }
    }
}
