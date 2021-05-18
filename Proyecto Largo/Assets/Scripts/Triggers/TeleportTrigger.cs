using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeleportTrigger : MonoBehaviour
{
    public Transform teleportTo;
    public bool exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Teleport(collision));
            if (!exit)
            {
                GameManagement.instance.rainManager.StopRain();
            }
        }
    }

    public IEnumerator Teleport(Collider2D collision)
    {
        yield return null;
        GameManagement.instance.blackScreen.BlackIn(true);
        yield return new WaitForSeconds(2);
        GameManagement.instance.timeManager.inCave = !exit;
        collision.gameObject.transform.position = teleportTo.position;
        GameManagement.instance.blackScreen.BlackIn(false);
    }
}
