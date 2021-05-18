using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestClaimReward : MonoBehaviour
{
    public MissionData mission;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //GameManagement.instance.missions.CompleteMissionToReward(mission);
            //GameManagement.instance.missions.MissionTaskCompleted(mission, missionTask);
        }
    }
}
