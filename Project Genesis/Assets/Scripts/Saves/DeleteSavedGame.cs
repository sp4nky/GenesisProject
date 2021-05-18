using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeleteSavedGame : MonoBehaviour
{
    public UnityEvent OnDelete;
    public Button deleteButton;
    public Save saveSelected
    {
        get; set;
    }

    void Start()
    {
        deleteButton.onClick.RemoveAllListeners();        
        deleteButton.onClick.AddListener(DeployConfirmationBox);
        
    }

    public void DeployConfirmationBox()
    {
        ConfirmationPopUp confirmationPopUp = GameController.instance.confirmationPopUp;
        confirmationPopUp.Show("Delete Save?", Delete, null);
    }

    public void Delete()
    {
        if (!saveSelected.name.Contains("SlotEmpty"))
        {
            string fileName;
            int i;
            FindNameFile(out fileName, out i);
            saveSelected.name = fileName + i;
            OnDelete.Invoke();
        }
    }

    private void FindNameFile(out string fileName, out int i)
    {
        fileName = "SlotEmpty";
        i = 1;
        List<Save> saves = Resources.LoadAll<Save>("Saves/").Where(x => x.name.Contains("SlotEmpty")).ToList();
        foreach (Save save in saves)
            if ((fileName + i.ToString()) == save.name)
                i++;
    }
}
