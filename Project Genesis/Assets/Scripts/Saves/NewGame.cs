using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    public Button newGameButton;
    public Save saveSelected
    {
        get; set;
    }

    void Start()
    {
        newGameButton.onClick.RemoveAllListeners();
        newGameButton.onClick.AddListener(DeployConfirmationBox);
    }

    public void DeployConfirmationBox()
    {
        if (!saveSelected.name.Contains("SlotEmpty"))
        {
            ConfirmationPopUp confirmationPopUp = GameController.instance.confirmationPopUp;
            confirmationPopUp.Show("Overwrite savefile?", CreateNewGame, null);
        }
        else
            CreateNewGame();
    }

    private void CreateNewGame()
    {
        if (saveSelected.name.Contains("SlotEmpty"))
        {
            string fileName;
            int i;
            FindFileName(out fileName, out i);
            saveSelected.name = fileName + i;

        }
        SetStartSave();
        GameController.instance.LoadSaveGame(saveSelected);
    }

    private void SetStartSave()
    {
        saveSelected.saveName = "Inicio";
        saveSelected.nivelName = "Nivel 1 (Tutorial)";
        saveSelected.timeHour = 0;
        saveSelected.timeMin = 0;
        saveSelected.kills = 0;
        saveSelected.deaths = 0;
        saveSelected.idScene = 1;
        saveSelected.checkpointPosition = new Vector3(16.2f, 0.6f, 0);
    }

    private static void FindFileName(out string fileName, out int i)
    {
        fileName = "SlotSave";
        i = 1;
        List<Save> saves = Resources.LoadAll<Save>("Saves/").Where(x => x.name.Contains("SlotSave")).ToList();
        foreach (Save save in saves)
            if ((fileName + i.ToString()) == save.name)
                i++;
    }
}
