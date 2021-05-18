using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileUI : MonoBehaviour
{
    public Text time;
    public Text fileName;
    public Text nivelName;

    internal void SetFileValues(Save saveFile)
    {
        if (saveFile)
        {
            time.text = GetTime(saveFile);
            fileName.text = saveFile.saveName;
            nivelName.text = saveFile.nivelName;
        }
    }

    private string GetTime(Save saveFile)
    {
        string time = saveFile.timeHour.ToString();
        if (saveFile.timeMin < 10)
        {
            time += ":" + "0" + saveFile.timeMin;
        }
        else
            time += ":" + saveFile.timeMin;
        return time;
    }

}
