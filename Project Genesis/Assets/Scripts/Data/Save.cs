using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Save : ScriptableObject
{
    public string saveName;
    public string nivelName;
    public int timeHour;
    public int timeMin;
    public int kills;
    public int deaths;
    public int idScene;
    public Vector3 checkpointPosition;
}
