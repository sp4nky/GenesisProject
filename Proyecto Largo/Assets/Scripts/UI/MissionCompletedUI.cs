using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCompletedUI : MonoBehaviour
{
    private Animator anim;
    public Text txtMissionName;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.eventsManager.OnMissionCompleted += ShowMission;
    }

    public void ShowMission(string missionName)
    {
        txtMissionName.text = missionName;
        ShowMissionAnimation();
    }


    private void ShowMissionAnimation()
    {
        anim.SetTrigger("MissionCompleted");
    }
}
