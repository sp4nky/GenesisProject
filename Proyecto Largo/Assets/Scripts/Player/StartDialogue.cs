using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private NPCSensor sensor;

    private void Awake()
    {
        sensor = GetComponent<NPCSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        bool action = Input.GetButtonDown("Action");
        NPCInteraction interaction = sensor.NearbyNPCInteraction();

        if (interaction && (action || interaction.interactOnCollision))
        {
            if (!GameManagement.instance.dialogue.dialogueInProgress)
            {
                NPCInteraction.DialogueMission dialogueMission = interaction.getNextDialogueMission();
                DialogueData dialogue = null;
                dialogue = interaction.GetNextDialogueNormal();

                if (dialogueMission!=null && dialogueMission.mission != null)
                {
                    //Empezar la mision
                    if (dialogueMission.mission.IsActive())
                    {
                        GameManagement.instance.dialogue.DialogueData(dialogueMission.dialogueStart);
                        GameManagement.instance.eventsManager.OnQuestEvent.Invoke(dialogueMission.dialogueStart.name);
                        StartCoroutine(AddMissionAfterDialogue(dialogueMission.mission));

                    }
                    else if (dialogueMission.mission.InProgress() && dialogueMission.dialogueInProgress != null && !dialogueMission.mission.CanCompleteLastTask())
                    {
                        dialogue = dialogueMission.dialogueInProgress;
                        GameManagement.instance.dialogue.DialogueData(dialogue);

                    }
                    else if (dialogueMission.mission.CanCompleteLastTask())
                    {
                        dialogueMission.mission.ChangeToCompleted();
                        dialogue = dialogueMission.dialogueMissionCompleted;
                        GameManagement.instance.dialogue.DialogueData(dialogue);
                        StartCoroutine(ActionAfterMissionComplete(interaction));
                    }
                }

                if (dialogueMission == null && dialogue != null)
                {
                    GameManagement.instance.dialogue.DialogueData(dialogue);
                }
                StartCoroutine(StopMovementNPC(interaction));
            }
            interaction.interactOnCollision = false;
        }
    }

    private IEnumerator StopMovementNPC(NPCInteraction interaction)
    {
        yield return null;
        if (interaction.nPCMovement)
        {


            interaction.nPCMovement.StopMovement();
            while (GameManagement.instance.dialogue.dialogueInProgress)
            {
                yield return null;
            }
            interaction.nPCMovement.ResumeMovement();
        }
    }

    private IEnumerator ActionAfterMissionComplete(NPCInteraction interaction)
    {
        yield return null;
        while (GameManagement.instance.dialogue.dialogueInProgress)
        {
            yield return null;
        }
        interaction.onDialogueEnd.Invoke();
    }

    private IEnumerator AddMissionAfterDialogue(MissionData mission)
    {
        yield return null;
        while (GameManagement.instance.dialogue.dialogueInProgress)
        {
            yield return null;
        }
        mission.ChangeToInProgress();
        GameManagement.instance.missions.AddNewMission(mission);

    }
}



