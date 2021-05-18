using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager 
{
    public CreatureKillEvent OnCratureKill;
    public QuestEvent OnQuestEvent;
    public MissionChange OnMissionChange;
    public MissionCompleted OnMissionCompleted;
    public MissionActivated OnMissionActivated;

    public delegate void CreatureKillEvent(string creatureName);
    public delegate void QuestEvent(string eventName);
    public delegate void MissionChange();
    public delegate void MissionCompleted(string missionNameToActive);
    public delegate void MissionActivated(string missionName);

}
