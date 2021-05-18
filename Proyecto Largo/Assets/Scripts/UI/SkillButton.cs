using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    private Button btn;
    private SkillData skill;
    public GameObject burblePrefab;
    private GameObject burble;
    public Transform burbleParent;

    public SkillButtonEvent OnClick;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick.Invoke(this);
    }

    public void setSkill(SkillData skill)
    {
        this.skill = skill;
        Text txt = btn.GetComponentInChildren<Text>();
        txt.text = skill.skillName;
        
    }

    public SkillData getSkill()
    {
        return skill;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(burble)
        {
            burble.SetActive(true);
            return;
        }
        burble = Instantiate(burblePrefab, burbleParent, false);
        burble.transform.localScale = Vector3.one;
        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform burbleRectTransform = burble.GetComponent<RectTransform>();
        burbleRectTransform.localPosition = Vector3.zero;
        SkillBurbleManager burbleController = burble.GetComponent<SkillBurbleManager>();

        burbleController.DefaultValues();
        burbleController.SetDanageSkill(skill.damage, skill.defenceReduction);
        if (skill.poison.enable)
            burbleController.SetDamagePerTurnSkill(skill.poison.damage, 0);
        if(skill.bleeding.enable)
            burbleController.SetDamagePerTurnSkill(skill.bleeding.percentDamage, skill.bleeding.turns);
        if (skill.stun.enable)
            burbleController.SetStunSkill(skill.stun.turns);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if(burble)
            burble.SetActive(false);
    }

    private void OnDestroy()
    {
        if(burble)
            Destroy(burble);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btn.Select();
    }

    [Serializable]
    public class SkillButtonEvent : UnityEvent<SkillButton> { }

}
