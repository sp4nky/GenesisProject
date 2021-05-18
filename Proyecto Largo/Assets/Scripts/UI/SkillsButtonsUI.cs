using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillsButtonsUI : MonoBehaviour
{
    public GameObject skillButton;
    public Transform buttonsParent;
    private List<GameObject> listskillsButtons = new List<GameObject>();
    public SkillEvent OnSkillPressed;

    public void SetSkills(SkillData[] skillsCharacter)
    {
        clearSkillsButtons();
        foreach (SkillData skill in skillsCharacter)
        {
            instanceSkillButton(skill);
        }
    }

    private void instanceSkillButton(SkillData skill)
    {
        GameObject instanceButton = Instantiate(skillButton, buttonsParent);
        instanceButton.transform.localScale = Vector3.one;
        SkillButton skillBtn = instanceButton.GetComponent<SkillButton>();
        skillBtn.setSkill(skill);
        skillBtn.OnClick.AddListener(OnSkillButtonClick);
        listskillsButtons.Add(instanceButton);
    }

    private void OnSkillButtonClick(SkillButton skillBtn)
    {
        SkillData skill = skillBtn.getSkill();
        OnSkillPressed.Invoke(skill);
    }

    public void clearSkillsButtons()
    {
        foreach (GameObject btn in listskillsButtons)
            Destroy(btn);
        listskillsButtons.Clear();
    }

    [Serializable]
    public class SkillEvent: UnityEvent<SkillData> { }

}
