using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualMissions : MonoBehaviour
{

    public Text desc;
    public Transform missionsPatent;
    public GameObject missionsPrefav;
    public GameObject missionsTitlePrefav;

    private List<GameObject> missionsTasks = new List<GameObject>();
   /* public void updateActivesMissions(List<MissionData> missions)
    {
        desc.text = "";
        foreach (MissionData dataMission in missions)
        {
            if (dataMission.state == MissionData.State.Progress)
            {
                desc.text += "** " + dataMission.nameMission + " **" + "\n";
                foreach (MissionData.MissionTask task in dataMission.missionsTasks)
                {
                    if (task.isCompleted)
                        desc.text += "X " + task.description + "\n";
                    else
                        desc.text += "_" + task.description + "\n";
                    if (task.enemiesToKill.enemyName != "")
                    {
                        desc.text += "Quedan " + task.enemiesToKill.count + " " + task.enemiesToKill.enemyName + "\n";

                    }
                }
            }
        }
    }
   */
    public void updateActivesMissions(List<MissionData> missions)
    {
        ClearMissionsTasks();
        foreach (MissionData dataMission in missions)
        {
            if (dataMission.state == MissionData.State.Progress)
            {
                InstantiateTitleMission(dataMission.nameMission);
                foreach (MissionData.MissionTask task in dataMission.missionsTasks)
                {
                    string content = task.description;
                    if (task.enemiesToKill.enemyName != "")
                    {
                        content += "Quedan " + task.enemiesToKill.count + " " + task.enemiesToKill.enemyName;

                    }
                    InstantiateMissionTask(task.isCompleted, content);
                }
            }
        }
    }

    private void InstantiateTitleMission(string content)
    {
        GameObject instance = Instantiate(missionsTitlePrefav, missionsPatent);
        instance.transform.localScale = Vector3.one;
        Text title = instance.GetComponentInChildren<Text>();
        title.text = content;
        missionsTasks.Add(instance);
    }

    private void InstantiateMissionTask(bool completed, string content)
    {
        GameObject instance = Instantiate(missionsPrefav, missionsPatent);
        instance.transform.localScale = Vector3.one;
        MissionTaskUI missionTaskUI = instance.GetComponent<MissionTaskUI>();
        missionTaskUI.SetTask(completed,content);
        missionsTasks.Add(instance);
    }

    private void ClearMissionsTasks()
    {
        foreach(GameObject task in missionsTasks)
        {
            Destroy(task);
        }
    }

    public void Hidde()
    {
        gameObject.SetActive(false);
    }   
    
    public void Show()
    {
        gameObject.SetActive(true);

    }
}

