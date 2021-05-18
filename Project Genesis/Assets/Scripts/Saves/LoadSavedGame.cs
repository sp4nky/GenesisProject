using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSavedGame : MonoBehaviour
{
    public Button loadButton;
    public Save saveSelected
    {
        get; set;
    }

    void Start()
    {
        loadButton.onClick.RemoveAllListeners();
        loadButton.onClick.AddListener(Load);
    }

    private void Load()
    {
        if (!saveSelected.name.Contains("SlotEmpty"))
        {
            GameController.instance.LoadSaveGame(saveSelected);
        }
    }
}
