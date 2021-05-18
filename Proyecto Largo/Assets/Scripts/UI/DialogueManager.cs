using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextBoxTyping typing;
    public DialogueData testData;
    public bool dialogueInProgress = false;
    public DialogueData initialDialogue;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameManagement.instance.dialogue)
            GameManagement.instance.dialogue = this;
    }

    public void StartInitialDialogue()
    {
        DialogueData(initialDialogue);
    }

    public void DialogueData(DialogueData dialogueData)
    {
        if(!dialogueInProgress)
            StartCoroutine(Dialogue(dialogueData));
    }

    private IEnumerator Dialogue(DialogueData dialogueData)
    {
        dialogueInProgress = true;

        yield return null;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Event on dialogue
        GameManagement.instance.eventsManager.OnQuestEvent(dialogueData.name);

        foreach (DialogueData.DialoguePart dialoguePart in dialogueData.dialogues)
        {
            foreach(string textDialogue in dialoguePart.texts)
            {
                typing.SetText(dialoguePart.characterName, textDialogue);
                while(typing.isTyping)
                {
                    if (Submit())
                        typing.CompleteText();
                    yield return null;
                }
                while (!Submit())
                {
                    yield return null;
                }
                yield return null;

            }
            yield return null;

        }
        typing.Hide();
        dialogueInProgress = false;
        player.GetComponent<PlayerMovement>().enabled = true;

        yield return null;
    }

    private static bool Submit()
    {
        return Input.GetButtonDown("Submit");
    }
}
