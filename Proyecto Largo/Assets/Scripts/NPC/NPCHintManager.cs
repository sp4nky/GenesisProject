using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHintManager : MonoBehaviour
{
    public SpriteRenderer alertSprite;
    public SpriteRenderer hintDialogue;
    public SpriteRenderer hintMarket;
    public SpriteRenderer HintBurble;

    public NPCInteraction interaction;

    // Update is called once per frame
    void Update()
    {
        if (interaction.misionIncomplete)
        {
            alertSprite.enabled = true;

        }
        else
            alertSprite.enabled = false;
        if (hintMarket)
        {
            hintMarket.enabled = true;
        }
        if (hintDialogue)
            hintDialogue.enabled = true;
        if (HintBurble)
            HintBurble.enabled = true;

    }
}
