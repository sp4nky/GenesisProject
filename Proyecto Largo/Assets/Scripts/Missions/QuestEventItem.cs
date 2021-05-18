using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestEventItem : MonoBehaviour
{
    public string activateOnEvent;
    public string eventQuest;
    public SpriteRenderer sprite;
    public SpriteRenderer spriteShadow;

    public SpriteRenderer hint;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        GameManagement.instance.eventsManager.OnMissionActivated += EnableObject;
    }

    public void EnableObject(string eventName)
    {
        if (eventName == activateOnEvent)
        {
            GameManagement.instance.eventsManager.OnMissionActivated -= EnableObject;
            col.enabled = true;
            if(sprite) sprite.enabled = true;
            if (spriteShadow) spriteShadow.enabled = true;

            if (hint) hint.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManagement.instance.eventsManager.OnQuestEvent(eventQuest);
            if (sprite && hint)
            {
                sprite.enabled = false;
                if (spriteShadow) spriteShadow.enabled = false;
                hint.enabled = false;
                col.enabled = false;
            }
        }
    }
}


