using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatMenu : MonoBehaviour
{
    public CharacterDataUI characterDataUI;
    public PhisicalAttackUI phisicalAttack;
    public InventoryCombatUI inventoryConsumablestUI;
    public InventoryCombatUI inventoryEquipamientUI;

    public void OpenIn(Vector2 position)
    {
        gameObject.SetActive(true);
        RectTransform rect = GetComponentInChildren<RectTransform>();
        //rect.position = position;

    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetCombatMenu(CharacterBehaviour character)
    {
        characterDataUI.ClearSkills();
        characterDataUI.CargarSkills(character.characterData.skills.Skip(1).ToArray());
        phisicalAttack.SetPhisicalSkill(character.characterData.skills[0]);
        phisicalAttack.OnSkillPressed.RemoveAllListeners();
        phisicalAttack.OnSkillPressed.AddListener(GameManagement.instance.combat.ExecuteSkill);
        SkillsButtonsUI uibtn = characterDataUI.GetSkillsButtonsUI();
        uibtn.OnSkillPressed.RemoveAllListeners();
        uibtn.OnSkillPressed.AddListener(GameManagement.instance.combat.ExecuteSkill);
        inventoryConsumablestUI.clearItemsUIButtons();
        inventoryConsumablestUI.LoadInventoryItemsButtons();
        inventoryEquipamientUI.clearItemsUIButtons();
        inventoryEquipamientUI.LoadInventoryItemsButtons();
    }

}
