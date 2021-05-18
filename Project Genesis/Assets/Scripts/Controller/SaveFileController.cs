using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveFileController : MonoBehaviour
{
    public SelectedButton OnSelectButton;
    public Save save;
    private Button btn;
    private SaveFileUI fileUI;

    private void Awake()
    {
        fileUI = GetComponent<SaveFileUI>();
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        if(fileUI) fileUI.SetFileValues(save);
        if (btn) btn.onClick.AddListener(SelectSaveFile);
    }

    public void SelectSaveFile()
    {
        OnSelectButton.Invoke(this);
    }

    [Serializable]
    public class SelectedButton : UnityEvent<SaveFileController> { };
}
