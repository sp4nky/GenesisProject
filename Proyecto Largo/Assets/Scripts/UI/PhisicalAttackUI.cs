using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhisicalAttackUI : MonoBehaviour
{
    private Button btn;
    public SkillEvent OnSkillPressed;

    public SkillData phisicalSkill
    { get; set; }


    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void SetPhisicalSkill(SkillData skill)
    {
        phisicalSkill = skill;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnSkillButtonClick);
    }

    private void OnSkillButtonClick()
    {
        OnSkillPressed.Invoke(phisicalSkill);
    }


    [Serializable]
    public class SkillEvent : UnityEvent<SkillData> { }
}
