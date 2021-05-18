using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LearnButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Button btn;
    public SkillData skill;
    public SkillEvent OnClickLearn;
    public SkillPointerEnter OnSkillPointerEnter;
    private Image imgSkill;
    public Color defaultColor
    {
        get; set;
    }

    private void Awake()
    {
        btn = GetComponent<Button>();
        imgSkill = GetComponent<Image>();
    }

    private void Start()
    {
        btn.onClick.AddListener(LearnSkillPlayer);
        defaultColor = imgSkill.color;
    }

    public void LearnSkillPlayer()
    {
        OnClickLearn.Invoke(this);
    }

    public void OnSelect(BaseEventData eventData)
    {
        imgSkill.color = Color.white;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgSkill.color = Color.white;
        OnSkillPointerEnter.Invoke(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgSkill.color = defaultColor;
    }

    [Serializable]
    public class SkillEvent : UnityEvent<LearnButton> { }
    [Serializable]
    public class SkillPointerEnter : UnityEvent<SkillData> { }


}
