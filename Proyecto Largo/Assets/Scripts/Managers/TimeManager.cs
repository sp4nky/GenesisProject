using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public CinemachineChangeTimePostPros cinemachine;
    public int secondsDurationPerHour = 60;
    [Range(0, 23)]
    public int hourStartDay = 6;
    [Range(0, 23)]
    public int hourStartEvening = 17;
    [Range(0, 23)]
    public int hourStartNight = 21;
    public int actualHour = 15;
    public int actualMinutes = 0;
    private float t = 0;
    public Text clock;
    public bool inCave;
    private bool changedToCave;

    private void Start()
    {
        GameManagement.instance.timeManager = this;
    }

    private void Update()
    {
        if(t == 0)
        {
            StartCoroutine(StartClock());
        }
        clock.text = GetCurrentTime();
        if(inCave && !changedToCave)
        {
            changedToCave = true;
            cinemachine.ChangeToCave();
        }
    }

    private IEnumerator StartClock()
    {
        while (t < secondsDurationPerHour)
        {
            actualMinutes = Mathf.RoundToInt(Mathf.Lerp(0, 59, t / secondsDurationPerHour));
            if (!GameManagement.instance.onCombat)
            {
                t += Time.deltaTime;
            }
            
            yield return null;
        }
        t = 0;
        actualMinutes = 0;
        actualHour++;
        if (actualHour >= 24)
            actualHour = 0;
        if (!inCave)
        {
            if (actualHour == hourStartDay)
                cinemachine.ChangeToDay();
            else if (actualHour == hourStartEvening)
                cinemachine.ChangeToEvening();
            else if (actualHour == hourStartNight)
                cinemachine.ChangeToNight();
        }
    }

    private string GetCurrentTime()
    {
        string now = actualHour.ToString();
        if(actualMinutes<10)
        {
            now += ":" + "0" + actualMinutes;
        }
        else
            now += ":" + actualMinutes;
        return now;
    }
}
