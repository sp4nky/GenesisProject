using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineChangeTimePostPros : MonoBehaviour
{
    public GameObject cameraDay;
    public GameObject cameraEvening;
    public GameObject cameraNight;
    public GameObject cameraCave;

    public void ChangeToDay()
    {
        cameraDay.SetActive(true);
        cameraEvening.SetActive(false);
        cameraNight.SetActive(false);
    }

    public void ChangeToEvening()
    {        
        cameraEvening.SetActive(true);
        cameraDay.SetActive(false);
        cameraNight.SetActive(false);
    }

    public void ChangeToNight()
    {
        cameraNight.SetActive(true);
        cameraDay.SetActive(false);
        cameraEvening.SetActive(false);
    }

    internal void ChangeToCave()
    {
        cameraNight.SetActive(false);
        cameraDay.SetActive(false);
        cameraEvening.SetActive(false);
        cameraCave.SetActive(true);
    }
}
