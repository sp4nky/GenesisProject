using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestCompletedItem : MonoBehaviour
{
    public MissionData mission;
    public MissionData.MissionTask missionTask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManagement.instance.missions.MissionTaskCompleted(mission, missionTask);
            gameObject.SetActive(false);
        }
    }

}
