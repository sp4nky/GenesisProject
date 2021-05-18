using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement
{
    public CheckpointManager checkpoint;
    private static GameManagement _instance;
    public TimeManager timeManager;
    public MainMenu menu;
    public static GameManagement instance { 
        get
        {
            if (_instance == null)
                _instance = new GameManagement();
            return _instance;
        }
    }
    public Inventory inventory;
    //Lista de Personajes
    public ConfirmationPopUp confirmationPopUp;
    public GameObject[] team1, team2;
    public DialogueManager dialogue;
    public MissionManager missions;
    public CombatManager combat;
    public StatsController stats;
    public CharacterBehaviour playerBehaviour;
    public ItemsManager itemsManager;
    public MarketUI marketManager;
    public MarketMenu marketMenu;
    public EventsManager eventsManager;
    public CameraManager disableRPGOnCombat;
    public VolumeManager volumeManager;
    public BlackScreenAnimationSettings blackScreen;
    public RainManager rainManager;
    internal AudioManager audioManager;

    public bool onCombat
    {   
        get; set;
    }

    private GameManagement()
    {
        inventory = new Inventory();
        eventsManager = new EventsManager();
    }

    public void AddItemInventory(ItemData item)
    {
        itemsManager.AddItem(item);
    }

    internal void Unpause()
    {
        Time.timeScale = 1;
    }

    internal void Pause()
    {
        Time.timeScale = 0;
    }

    public GameObject[] GetTeam1()
    {
        return team1;
    }

    public GameObject[] GetTeam2()
    {
        return team2;
    }

    public void StartCombat(EnemyTeam enemyTeam)
    {
        onCombat = true;
        team2 = enemyTeam.team;
        combat.StartCombat(enemyTeam);
    }

    public void ExitGame()
    {
        onCombat = false;
        menu.ExitGame();
    }

    public void Retry()
    {
        //menu.Retry();
        checkpoint.Respawn();
    }

    public void AddRewardPlayer(int gold, ItemData[] items)
    {
        foreach (ItemData item in items)
            itemsManager.AddItem(item);
        inventory.gold += gold;
        
    }

    public void UseItem(ItemData item)
    {
        if (item.GetType().Equals(typeof(ItemDataCons)))
        {
            playerBehaviour.UseItemCons((ItemDataCons) item);
        }
        else
        {
            playerBehaviour.UseItemEquip((ItemDataEquip) item);
        }
        stats.UpdatePlayerStatBar(playerBehaviour);
    }

}
