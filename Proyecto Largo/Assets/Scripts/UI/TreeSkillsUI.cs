using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TreeSkillsUI : MonoBehaviour
{
    private LearnButton[] skillsToLearn;
    public Text txtSkillPoints;
    private PlayerCharacterData playerCharacterData;
    public Text skillTitle;
    public Text skillDesc;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacterData = (PlayerCharacterData)GameManagement.instance.playerBehaviour.characterData;
        skillsToLearn = GetComponentsInChildren<LearnButton>();
        foreach (LearnButton learnButton in skillsToLearn)
        {
            learnButton.OnClickLearn.AddListener(LearnSkillPressed);
            learnButton.OnSkillPointerEnter.AddListener(ShowSkillData);
        }
        if (skillsToLearn.Length > 0)
        {
            skillsToLearn[0].OnClickLearn.RemoveAllListeners();
            skillsToLearn[0].OnClickLearn.AddListener(UpgradeBasicAttack);
        }
    }

    private void Update()
    {
        if(txtSkillPoints)
        {
            txtSkillPoints.text = playerCharacterData.skillpoints.ToString();
        }
    }

    public void LearnSkillPressed(LearnButton learnButton)
    {
        SkillData skill = learnButton.skill;
        var listSkills = GameManagement.instance.playerBehaviour.characterData.skills.ToList();
        if (!listSkills.Contains(skill) && skill.CanLearn(playerCharacterData))
        {
            learnButton.defaultColor = Color.white;
            learnButton.enabled = false;
            playerCharacterData.skillpoints -= skill.points;
            if (!skill.aoe)
                playerCharacterData.AddSkill(skill);
            else
                UpgradeToAOE(listSkills);
        }

    }

    private void UpgradeToAOE(List<SkillData> listSkills)
    {
        for(int i=1; i < listSkills.Count;i++)        
        {
            listSkills[i].aoe = true;
        }
    }

    public void UpgradeBasicAttack(LearnButton learnButton)
    {
        SkillData skill = learnButton.skill;

        if (playerCharacterData.skillpoints > 0)
        {
            learnButton.defaultColor = Color.white;
            learnButton.enabled = false;
            playerCharacterData.ChangeBasicAttack(skill);
            playerCharacterData.skillpoints--;
        }
    }

    public void ShowSkillData(SkillData skill)
    {
        skillTitle.text = skill.skillName;
        skillDesc.text = "";
        skillDesc.text += "Puntos requeridos: " + skill.points + "\n";
        if(skill.skillBefore) skillDesc.text += "Necesitas aprender antes: " + skill.skillBefore.skillName + "\n";
        if (skill.damage>0)  skillDesc.text += "Daño: " + skill.damage + "\n";
        if (skill.defenceReduction > 0)   skillDesc.text += "Reduccion de defensa: " + skill.defenceReduction + "\n";
        if (skill.stun.enable)
        {
            skillDesc.text += "Stun \n";
            skillDesc.text += "Aturde por " + skill.stun.turns + " turnos.\n";
        }
        if (skill.bleeding.enable)
        {
            skillDesc.text += "Sangrado \n";
            skillDesc.text += "Daño(%) por turno: " + skill.bleeding.percentDamage + "por " + skill.bleeding.turns + " turnos.\n";
        }
        if (skill.poison.enable)
        {
            skillDesc.text += "Veneno \n";
            skillDesc.text += "Daño por turno: " + skill.poison.damage + " (se reduce -1 el daño por turno)\n";
        }
        if (skill.aoe)
        {
            skillDesc.text += "Todas las habilidades afectan todos los enemigos.\n";
        }
    }

}
