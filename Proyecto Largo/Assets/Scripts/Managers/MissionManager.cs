using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private List<MissionData> missions = new List<MissionData>();
    public ActualMissions actualMissions;
    public MissionData initualMission;

    void Start()
    {
        missions = Resources.LoadAll<MissionData>("Data/Missions/").ToList();
        if (GameManagement.instance.missions==null)
            GameManagement.instance.missions = this;
        GameManagement.instance.eventsManager.OnMissionChange += UpdateActualMissions;
        GameManagement.instance.eventsManager.OnMissionCompleted += ActivateMission;
        GameManagement.instance.eventsManager.OnMissionActivated += ActivateMission;
        AddNewMission(initualMission);
    }

    public void UpdateActualMissions()
    {
        ClearMissions();
        actualMissions.Show();
        actualMissions.updateActivesMissions(missions);
        if (MissionsInProgress() <= 0)
        {
            actualMissions.Hidde();
        }
    }

    private int MissionsInProgress()
    {
        int count = 0;
        foreach(MissionData mission in missions)
        {
            if (mission.state == MissionData.State.Progress)
                count++;
        }
        return count;
    }

    public void ClearMissions()
    {
        foreach(MissionData mission in missions)
        {
            if(mission.state == MissionData.State.Progress && mission.ReadyToReward())
            {
                GameManagement.instance.AddRewardPlayer(mission.goldReward, mission.rewards);
                GameManagement.instance.eventsManager.OnCratureKill -= mission.OnCreatureKill;
                GameManagement.instance.eventsManager.OnQuestEvent -= mission.OnQuestEvent;
                mission.ChangeToCompleted();
            }
        }
    }

    public void ActivateMission(string nameMissionToActive)
    {
        foreach(MissionData mission in missions)
        {
            if (mission.name == nameMissionToActive)
            {
                mission.ChangeToActive();
            }
        }

    }

    
    public void AddNewMission(MissionData mission)
    {
        mission.ChangeToInProgress();
        UpdateActualMissions();

        GameManagement.instance.eventsManager.OnQuestEvent += mission.OnQuestEvent;
        GameManagement.instance.eventsManager.OnCratureKill += mission.OnCreatureKill;

    }

    public void MissionTaskCompleted(MissionData mission, MissionData.MissionTask missionTask)
    {
        actualMissions.Show();
        if (!missions.Contains(mission))
        {
            missions.Add(mission);
        }

        actualMissions.updateActivesMissions(missions);

        if (missions.Count<1)
        {
            actualMissions.Hidde();
        }
        GameManagement.instance.eventsManager.OnCratureKill -= mission.OnCreatureKill;
        GameManagement.instance.eventsManager.OnQuestEvent -= mission.OnQuestEvent;

    }

}
