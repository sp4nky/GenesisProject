using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelController : MonoBehaviour
{
    public GameObject player;
    private GameController gameInstance;

    // Start is called before the first frame update
    void Start()
    {
        gameInstance = GameController.instance;
        gameInstance.nivelController = this;
        if (gameInstance.respawnPosition != Vector3.zero)
            player.transform.position = gameInstance.respawnPosition;
    }

}
