using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataUI : MonoBehaviour
{
    private SkillsButtonsUI uiButtons;
    private hiddePanelUI hiddePanel;
    private void Awake()
    {
        uiButtons = GetComponent<SkillsButtonsUI>();
        hiddePanel = GetComponent<hiddePanelUI>();
    }

    public void CargarSkills(SkillData[] skillsCharacter)
    {
        hiddePanel.Show();
        uiButtons.SetSkills(skillsCharacter);
        hiddePanel.hidde();
    }

    public void ClearSkills()
    {
        hiddePanel.Show();
        uiButtons.clearSkillsButtons();
        hiddePanel.hidde();
    }

    public SkillsButtonsUI GetSkillsButtonsUI()
    {
        return uiButtons;
    }
}
