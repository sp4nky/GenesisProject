using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTaskUI : MonoBehaviour
{
    public Text mark;
    public Text description;

    public void SetTask(bool completed, string description)
    {
        //mark.enabled = completed;
        mark.enabled = false;
        this.description.text = description;
        if (completed)
        {
            Color color = this.description.color;
            color.a = 0.3f;
            this.description.color = color;
        }
    }

    public void SetTitle(string title)
    {
        mark.enabled = false;
        description.text = title;
    }

}
