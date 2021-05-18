using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavesController : MonoBehaviour
{
    public List<Save> saves = new List<Save>();
    public List<GameObject> savesSolts = new List<GameObject>();
    public GameObject prefabSaveFile;
    public GameObject prefabEmptySaveFile;
    public Transform filesParent;
    private LoadSavedGame loadSavedGame;
    private DeleteSavedGame deleteSavedGame;
    private NewGame newGame;

    private void Awake()
    {
        loadSavedGame = GetComponent<LoadSavedGame>();
        deleteSavedGame = GetComponent<DeleteSavedGame>();
        newGame = GetComponent<NewGame>();
    }

    void Start()
    {
        deleteSavedGame.OnDelete.RemoveAllListeners();
        deleteSavedGame.OnDelete.AddListener(ReloadSlots);
        ReloadSlots();
    }

    private void ReloadSlots()
    {
        foreach (GameObject save in savesSolts)
            Destroy(save);
        savesSolts.Clear();
        LoadSavesFiles();
        LoadEmptySlots();
    }

    private void LoadEmptySlots()
    {
        foreach (Save save in saves)
        {
            if (save.name.Contains("SlotEmpty"))
            {
                GameObject instance = Instantiate(prefabEmptySaveFile, filesParent);
                instance.transform.localScale = Vector3.one;
                SaveFileController saveController = instance.GetComponent<SaveFileController>();
                saveController.save = save;
                saveController.OnSelectButton.AddListener(SelectSave);
                savesSolts.Add(instance);
            }
        }
    }

    private void LoadSavesFiles()
    {
        saves = Resources.LoadAll<Save>("Saves/").ToList();
        foreach (Save save in saves)
        {
            if (!save.name.Contains("SlotEmpty"))
            {
                GameObject instance = Instantiate(prefabSaveFile, filesParent);
                instance.transform.localScale = Vector3.one;
                SaveFileController saveController = instance.GetComponent<SaveFileController>();
                saveController.save = save;
                saveController.OnSelectButton.AddListener(SelectSave);
                savesSolts.Add(instance);
            }
        }
    }

    public void SelectSave(SaveFileController saveFileController)
    {
        if (loadSavedGame) loadSavedGame.saveSelected = saveFileController.save;
        if (newGame) newGame.saveSelected = saveFileController.save;
        if (deleteSavedGame) deleteSavedGame.saveSelected = saveFileController.save;
    }

}
