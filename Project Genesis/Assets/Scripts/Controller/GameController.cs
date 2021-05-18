using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController
{
    public NivelController nivelController; 
    public EventsController events;
    public MainMenu menu;
    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameController();
            return _instance;
        }
    }
    public int kills { get; set; }
    public int deaths { get; set; }
    public int idCurrentScene { get; set; }
    public Vector3 respawnPosition = Vector3.zero;
    public ConfirmationPopUp confirmationPopUp;


    private Save currentSave;

    public GameController()
    {
        events = new EventsController();
        events.OnCratureKill += AddKillCount;
        events.OnSaveGame += SaveGame;
    }

    public void AddKillCount(string enemyName)
    {
        kills++;
    }

    public void SaveGame(Vector3 position, string name)
    {
        /*
        if(!currentSave)
        {
            currentSave = (Save)ScriptableObject.CreateInstance<Save>();
            currentSave.name = name;
            AssetDatabase.CreateAsset(currentSave, "Assets/Resources/Saves/");
        }
    */
        currentSave.nivelName = name;
        currentSave.saveName = name;
        currentSave.kills = kills;
        currentSave.deaths = deaths;
        currentSave.idScene = idCurrentScene;
        currentSave.checkpointPosition = position;
        respawnPosition = position;

    }

    public void LoadSaveGame(Save save)
    {
        currentSave = save;
        kills = save.kills;
        deaths = save.deaths;
        idCurrentScene = save.idScene;
        respawnPosition = save.checkpointPosition;
        menu.GoToScene(save.idScene);
    }


}
