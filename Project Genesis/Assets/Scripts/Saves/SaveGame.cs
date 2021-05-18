using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SaveGame : MonoBehaviour
{
    private SpriteRenderer sprite;
    public string collisionTag = "Player";
    public string saveName; 
    private bool saving;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!saving && collision.CompareTag(collisionTag))
            StartCoroutine(SaveCheckpoint());
    }

    private IEnumerator SaveCheckpoint()
    {
        saving = true;
        yield return null;
        ChangeSpriteToGreen();
        GameController.instance.events.OnSaveGame.Invoke(transform.position,saveName);
        yield return new WaitForSeconds(1f);
        DefaultColorSprite();
        yield return new WaitForSeconds(5f);
        saving = false;
    }

    private void DefaultColorSprite()
    {
        Color color = sprite.color;
        color = new Color(1, 1, 1, 1);
        sprite.color = color;
    }

    private void ChangeSpriteToGreen()
    {
        Color color = sprite.color;
        color = new Color(1, 0, 0, 1);
        sprite.color = color;
    }
}
