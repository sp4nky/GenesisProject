using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SleepTrigger : MonoBehaviour
{
    private Collider2D col;
    private bool actionPressed;
    private ConfirmationPopUp confirmationPopUp;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        actionPressed = Input.GetButton("Action");
        if(actionPressed && confirmationPopUp)
        {
            confirmationPopUp.Show("¿Dormir en La Tienda?", Sleep, null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            confirmationPopUp = GameManagement.instance.confirmationPopUp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        confirmationPopUp = null;

    }

    public void Sleep()
    {
        StartCoroutine(Sleeping());
    }

    private IEnumerator Sleeping()
    {
        yield return null;
        GameManagement.instance.blackScreen.BlackIn(true);
        yield return new WaitForSeconds(1.5f);
        GameManagement gameManagement = GameManagement.instance;
        gameManagement.timeManager.actualHour = 7;
        gameManagement.blackScreen.BlackIn(false);
        gameManagement.playerBehaviour.RestoreHP(gameManagement.playerBehaviour.characterData.maxHp);
        gameManagement.playerBehaviour.RestoreMana(gameManagement.playerBehaviour.characterData.maxMana);
        gameManagement.playerBehaviour.InitializeAffectedSkill();
        gameManagement.stats.UpdatePlayerStatBar(gameManagement.playerBehaviour);

    }
}
