using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class MissionData : ScriptableObject
{
    public string nameMission;
    public string missionToActivateOnBegin;
    public MissionTask[] missionsTasks;
    public ItemData[] rewards;
    public int goldReward = 0;
    public string missionToActivateOnFinish;
    public enum State { Active, Progress, Completed, Inactive }
    public State state = State.Active;

    public void ChangeToInactive()
    {
        state = State.Inactive;
    }

    public void ChangeToInProgress()
    {
        state = State.Progress;
        GameManagement.instance.eventsManager.OnMissionChange();
        GameManagement.instance.eventsManager.OnMissionActivated(missionToActivateOnBegin);

    }

    public void ChangeToActive()
    {
        state = State.Active;
        GameManagement.instance.eventsManager.OnMissionChange();


    }

    public void ChangeToCompleted()
    {
        state = State.Completed;
        GameManagement.instance.eventsManager.OnMissionChange();
        GameManagement.instance.eventsManager.OnMissionCompleted(missionToActivateOnFinish);
        GameManagement.instance.eventsManager.OnMissionCompleted(this.nameMission);

    }

    public bool isCompleted()
    {
        return state == State.Completed;
    }

    internal bool IsInactive()
    {
        return state == State.Inactive;
    }

    public bool InProgress()
    {
        return state == State.Progress;
    }

    public bool IsActive()
    {
        return state == State.Active;
    }

    public bool ReadyToReward()
    {
        bool ready = true;
        /*if (missionsTasks.Length > 1)
        {*/
            for (int i = 0; i < missionsTasks.Length; i++)
            {
                if (!missionsTasks[i].isCompleted)
                {
                    ready = false;
                    break;
                }
            }
        //}
        return ready;
    }

    public void CompleteTask(MissionTask missionTask)
    {
        foreach (MissionTask task in missionsTasks)
        {
            if (task.description == missionTask.description)
                task.isCompleted = true;
        }
        GameManagement.instance.eventsManager.OnMissionChange();

    }

    public void OnCreatureKill(string creatureName)
    {
        foreach (MissionTask missionTask in missionsTasks)
        {
            if (missionTask.enemiesToKill.enemyName == creatureName)
            {
                if (missionTask.enemiesToKill.count > 0)
                {
                    missionTask.enemiesToKill.count--;
                }
                if (missionTask.enemiesToKill.count == 0)
                {
                    CompleteTask(missionTask);
                    GameManagement.instance.eventsManager.OnMissionChange();
                }
            }
        }

    }

    public void OnMissionCompleted(string questName)
    {
        if (name == questName)
            ChangeToActive();
    }

    public void OnTaskCompleted(string questName)
    {
        if (name == questName)
            ChangeToActive();
    }

    public void OnQuestEvent(string questEventName)
    {
        foreach (MissionTask missionTask in missionsTasks)
        {
            if (missionTask.questEventName == questEventName && CanComplete(missionTask))
            {
                CompleteTask(missionTask);
            }
        }
    }
    public bool CanCompleteLastTask()
    {
        return CanComplete(missionsTasks[missionsTasks.Length - 1]);
    }

    private bool CanComplete(MissionTask missionTask)
    {
        bool canComplete = true;
        foreach(MissionTask task in missionsTasks)
        {
            if (task.description == missionTask.description)
                break;
            if (!task.isCompleted)
            {
                canComplete = false;
                break;
            }

        }

        return canComplete;
    }

    [Serializable]
    public class MissionTask
    {
        public string description;
        public EnemiesToKill enemiesToKill;
        public string questEventName;
        public string taskEventTrigger;
        public bool isCompleted= false;
    }

    [Serializable]
    public class EnemiesToKill
    {
        public string enemyName;
        public int count;
    }
}
