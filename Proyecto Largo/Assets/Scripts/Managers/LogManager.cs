using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    public GameObject logContainer;
    public GameObject logRowPrefab;
    private List<GameObject> logRows = new List<GameObject>();

    public void setLogRow(string textLog)
    {
        GameObject logRow = Instantiate(logRowPrefab, logContainer.transform, false);
        logRows.Add(logRow);
        logRow.transform.localScale = Vector3.one;
        Text txt = logRow.GetComponent<Text>();
        txt.text = textLog;
    }

    public void ClearLog()
    {
        foreach(GameObject go in logRows)
        {
            Destroy(go);
        }
        logRows.Clear();
    }
}
