using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsController
{
    public CreatureKillEvent OnCratureKill;
    public PlayerDeath OnPlayerDeath;
    public SaveGameEvent OnSaveGame;

    public delegate void CreatureKillEvent(string creatureName);
    public delegate void PlayerDeath();
    public delegate void SaveGameEvent(Vector3 position, string name);
}
