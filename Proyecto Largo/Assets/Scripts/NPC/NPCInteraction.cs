using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class NPCInteraction : MonoBehaviour
{
    public DialogueMission[] dialoguesMissions;
    public DialogueData[] dialoguesNormal;
    public UnityEvent onDialogueEnd;
    public NPCGoTo nPCMovement;
    private int normalIndex = 0;
    private int missionIndex = 0;
    public bool interactOnCollision = false;
    public bool misionIncomplete
    {
        get; set;
    }

    private void Start()
    {
        AreMissionsIncomplete();
        GameManagement.instance.eventsManager.OnMissionChange += AreMissionsIncomplete;
    }


    public NPCMarket market
    {
        get
        {
            return GetComponent<NPCMarket>();
        }
    }



    public void AreMissionsIncomplete()
    {
        misionIncomplete = false;
        foreach(DialogueMission dialogueMission in dialoguesMissions)
        {
            if(dialogueMission.mission.IsActive())
            {
                misionIncomplete = true;
            }
            
        }
    }

    public DialogueMission getNextDialogueMission()
    {
        DialogueMission dialogue = null;
        if(dialoguesMissions.Length > 0)
        {
            if(dialoguesMissions[missionIndex].mission.isCompleted())
            {
                missionIndex++;
                if (dialoguesMissions.Length == missionIndex)
                    dialogue = null;
            }
            if (!dialoguesMissions[missionIndex].mission.IsInactive())
            {
                dialogue = dialoguesMissions[missionIndex];
            }
        }
        return dialogue;
    }

    public DialogueData GetNextDialogueNormal()
    {
        DialogueData dialogue = null;
        if (dialoguesNormal.Length > 0)
        {
            dialogue = dialoguesNormal[normalIndex];
            normalIndex++;
            if (dialoguesNormal.Length == normalIndex)
                normalIndex = 0;
        }
        return dialogue;

    }

    [Serializable]
    public class DialogueMission
    {
        public DialogueData dialogueStart;
        public DialogueData dialogueInProgress;
        public DialogueData dialogueMissionCompleted;
        public MissionData mission;
    }
}
