using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject player;
    private Vector2 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.checkpoint = this;
        respawnPosition = new Vector2(160, -9);
        player.transform.position = respawnPosition;
    }

    public void SetRespawnVillage()
    {
        respawnPosition = new Vector2(151, 127);
    }

    public void Respawn()
    {
        player.transform.position = respawnPosition;
        GameManagement gameManagement = GameManagement.instance;
        gameManagement.timeManager.actualHour = 8;
        gameManagement.blackScreen.BlackIn(false);
        gameManagement.playerBehaviour.RestoreHP(gameManagement.playerBehaviour.characterData.maxHp);
        gameManagement.playerBehaviour.RestoreMana(gameManagement.playerBehaviour.characterData.maxMana);
    }

}
