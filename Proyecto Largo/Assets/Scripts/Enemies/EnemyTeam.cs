using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyTeam : MonoBehaviour
{
    public GameObject[] team;
    public ItemData[] rewards;
    public int goldReward = 0;
    public bool isTraining;
    public bool respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {            
            GameManagement.instance.StartCombat(this);
            if (!isTraining)
            {
                gameObject.SetActive(false);
                if (respawn)
                    Invoke("Respawn", 300f);
            }
            else
                collision.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 10, gameObject.transform.position.z);
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}

