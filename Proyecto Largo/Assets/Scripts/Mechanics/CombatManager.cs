using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LogManager))]

public class CombatManager : MonoBehaviour
{
    public GameObject exitTraining;
    public PhisicalAttackUI phisicalAttack;
    public InventoryCombatUI inventoryCombatUI;
    public SlotManager slotManagerTeam1;
    public SlotManager slotManagerTeam2;
    public VictoryDetailsUI victoryPanel;
    public Image defeatSprite;
    private bool attackInProgress = false;
    private int expForWin = 0;
    private LogManager combatLog;
    private bool stuned;
    private CharacterBehaviour currentCharacter;
    private CharacterBehaviour oponentCharacter;
    private CharacterSlot currentSlot;
    private CharacterSlot oponentSlot;
    private bool winTeam1 = false;
    private bool winTeam2 = false;
    private int turnTeam = 0;
    private EnemyTeam enemyTeam;
    private SlotManager oponentSlotManager;

    private void Awake()
    {
        combatLog = GetComponent<LogManager>();
    }

    void Start()
    {
        GameManagement.instance.combat = this;
    }

    void Update()
    {
        bool esc = Input.GetKeyDown(KeyCode.Escape);
        if (esc && slotManagerTeam2 && slotManagerTeam2.isTraining)
        {
            slotManagerTeam2.ClearSlots();
            ClearSlots();
            slotManagerTeam1.slots.Clear();
            slotManagerTeam2.isTraining = false;
        }

    }

    public void StartCombat(EnemyTeam enemyTeam)
    {
        this.enemyTeam = enemyTeam;
        turnTeam = 0;
        winTeam1 = false;
        winTeam2 = false;
        slotManagerTeam1.DefaultSlots();
        slotManagerTeam2.DefaultSlots();
        combatLog.ClearLog();
        attackInProgress = false;
        oponentSlotManager = slotManagerTeam2;
        slotManagerTeam2.SetSlotsCharacters(enemyTeam.team);

        slotManagerTeam2.isTraining = enemyTeam.isTraining;

        slotManagerTeam1.SetSlotsCharacters(GameManagement.instance.GetTeam1());
        slotManagerTeam1.isTraining = true;
        slotManagerTeam1.ClearEmptySlots();


        if (enemyTeam.isTraining)
        {
            //slotManagerTeam1.SetSlotsCharacters(GameManagement.instance.GetTeam1());
            exitTraining.SetActive(true);
        }
        else
        {
            exitTraining.SetActive(false);
        }
        //Calculate Exp
        expForWin = slotManagerTeam2.GetExpForTeam();
        //Comienza la primera unidad del Equipo 1
        StartCoroutine(FirstSelect());
    }

    private IEnumerator FirstSelect()
    {
        yield return null;
        if (slotManagerTeam1.GetCharactersCount() != 0 & slotManagerTeam2.GetCharactersCount() != 0)
        {
            slotManagerTeam1.MarkFirstTurn();
            slotManagerTeam2.SelectFirst();
            currentSlot = slotManagerTeam1.GetCurrentTurnSlot();
            currentCharacter = currentSlot.getCharacter();
            StartCoroutine(Combat());
           
        }
    }

    private IEnumerator Combat()
    {
        yield return null;
        if (slotManagerTeam1.GetCharactersCount() != 0 & slotManagerTeam2.GetCharactersCount() != 0)
        {
            while (!winTeam1 && !winTeam2)
            {
                if (!attackInProgress)
                {
                    if (turnTeam == 1)
                    {
                        oponentSlotManager = slotManagerTeam1;
                        if (!enemyTeam.isTraining)
                        {
                            StartCoroutine(IAMovement());
                        }
                        else
                        {
                            slotManagerTeam1.SelectSolt();
                            OponentSelected(slotManagerTeam1.GetSelectedSlot());
                        }
                    }
                    else
                    {
                        oponentSlotManager = slotManagerTeam2;

                        if (slotManagerTeam2.GetCharactersCount() > 0)
                        {
                            slotManagerTeam2.SelectSolt();
                            OponentSelected(slotManagerTeam2.GetSelectedSlot());
                        }
                    }
                }
                yield return null;
                ChequearVictoria();

            }
            attackInProgress = true;
            if (winTeam1)
            {
                Debug.Log("Gano el Player");
                yield return new WaitForSeconds(2);
                if (!enemyTeam.isTraining)
                {
                    SetVictoryPanel();
                    GameManagement.instance.AddRewardPlayer(enemyTeam.goldReward, enemyTeam.rewards);
                    //Time to read
                    yield return new WaitForSeconds(3);
                    victoryPanel.DefaultValue();
                    victoryPanel.gameObject.SetActive(false);
                }
                GameManagement.instance.onCombat = false;
            }
            else
            {
                Debug.Log("Gano el enemigo");
                StartCoroutine(defeatImage());
                winTeam2 = false;
            }

        }
        yield return null;
        victoryPanel.gameObject.SetActive(false);
    }

    private void SetVictoryPanel()
    {
        victoryPanel.gameObject.SetActive(true);
        victoryPanel.DefaultValue();
        victoryPanel.SetExp(expForWin);
        PlayerCharacterData characterData = (PlayerCharacterData) GameManagement.instance.playerBehaviour.characterData;
        int oldNivel = characterData.nivel;
        characterData.AddExp(expForWin);
        if (oldNivel < characterData.nivel)
        {
            characterData.skillpoints++;
            victoryPanel.EnableLevelUp();
            victoryPanel.SetNivel(characterData.nivel);
            //Sube las Stats
            characterData.maxHp += 10;
            characterData.maxMana += 30;
            GameManagement.instance.playerBehaviour.RestoreHP(characterData.maxHp);
            GameManagement.instance.playerBehaviour.RestoreMana(characterData.maxMana);
        }
        characterData.ClearBuff();
        GameManagement.instance.stats.UpdatePlayerStatBar(GameManagement.instance.playerBehaviour);
    }

    private IEnumerator IAMovement()
    {
        //attackInProgress = true;
        yield return null;
        slotManagerTeam1.DeselectAllSlots();
        slotManagerTeam1.SelectFirst();
        OponentSelected(slotManagerTeam1.GetSelectedSlot());
        //elige una skill aleatoria
        SkillData skill = currentCharacter.characterData.skills[Random.Range(0, currentCharacter.characterData.skills.Length - 1)];
        //si no da el mana para la skill elige la primera skill (ataque basico)
        if (skill.mana <= currentCharacter.GetMana())
            ExecuteSkill(skill);
        else
            ExecuteSkill(currentCharacter.characterData.skills.First());
    }

    private void OponentSelected(CharacterSlot characterSlot)
    {
        oponentCharacter = characterSlot.getCharacter();
        oponentSlot = characterSlot;
    }

    private void ChequearVictoria()
    {
        if (slotManagerTeam1.GetCharactersCount() == 0)
            winTeam2 = true;
        else if (slotManagerTeam2.GetCharactersCount() == 0)
            winTeam1 = true;
    }

    private IEnumerator defeatImage()
    {
        yield return null;
        float t = 0;
        float tope = 2f;
        while (t < tope)
        {
            var tempColor = defeatSprite.color;
            tempColor.a = Mathf.Lerp(0, 1, t / tope);
            defeatSprite.color = tempColor;

            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(3);
        var color = defeatSprite.color;
        color.a = 0;
        defeatSprite.color = color;
        slotManagerTeam2.ClearSlots();
        //ClearSlots();
        slotManagerTeam1.slots.Clear();
        slotManagerTeam2.isTraining = false;
        GameManagement.instance.onCombat = false;
        GameManagement.instance.blackScreen.BlackIn(true);
        GameManagement.instance.Retry();
        yield return new WaitForSeconds(1.5f);
    }

    public void ExecuteSkill(SkillData skill)
    {
        if (attackInProgress)
            return;
        attackInProgress = true;
        if (!stuned)
        {
            if (skill.mana <= currentCharacter.GetMana())
            {
                currentCharacter.DecreaseMana(skill.mana);

                StartCoroutine(attackAnimation(currentSlot, oponentSlot.GetHitPosition(), skill, EndTurn));
                if (skill.aoe)
                {
                    foreach (CharacterSlot characterSlot in oponentSlotManager.slots)
                    {
                        CharacterBehaviour character = characterSlot.getCharacter();
                        character.AddAffectedSkill(skill);
                        PlayParticlesEffects(characterSlot, skill);
                    }
                }
                else
                {
                    oponentCharacter.AddAffectedSkill(skill);
                    PlayParticlesEffects(oponentSlot, skill);
                }
                LogAffectedBySkill(skill);
            }
            else
                combatLog.setLogRow("Mana insuficiente.");
        }
        else
        {
            EndTurn();
        }
    }

    private void PlayParticlesEffects(CharacterSlot slot, SkillData skill)
    {
        ParticleEffectsManager effectsManager = slot.GetParticleEffectsManager();
        if (effectsManager) effectsManager.SetCurrentParticleEffect(skill);
        if (effectsManager) effectsManager.ActiveEffects();
    }

    private void EndTurn()
    {
        SkillData affectedBySkill = currentCharacter.GetAffectedSkill();
        PoisonResolve(affectedBySkill);
        BleedingResolve(affectedBySkill);
        BurningResolve(affectedBySkill);
        PlayParticlesEffects(currentSlot, affectedBySkill);
        oponentSlot.slotDeselected();
        ChangeTurn();
    }

    private IEnumerator attackAnimation(CharacterSlot fromCharacter, Transform toPosition, SkillData skill, Action action)
    {
        yield return null;
        float t = 0;
        float moveDuration = .7f;
        float animationDuration = .5f;
        Transform fromPosition = fromCharacter.characterParent;
        Vector3 initialPos = fromPosition.position;
        Vector3 finalPos = toPosition.position;
        AnimationCurve currentAnimationCurveGo = fromCharacter.getCharacter().animationCurveGo;
        AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        CharacterCombatAnimationSettings animationSettings = currentSlot.GetAnimationSettings();
        if (skill.meleeAttack)
        {
            if (animationSettings != null)
            {
                animationSettings.AnimationChase();
            }
            while (t < moveDuration)
            {
                fromCharacter.characterParent.position = Vector2.Lerp(initialPos, finalPos, animationCurve.Evaluate(t / moveDuration));
                fromCharacter.characterParent.position += Vector3.up * currentAnimationCurveGo.Evaluate(t / moveDuration);
                yield return null;
                t += Time.deltaTime;
            }
            //Attack animation
            if (animationSettings != null)
            {
                animationSettings.AnimationAttack();
            }
            yield return new WaitForSeconds(animationDuration);
            if (animationSettings != null)
            {
                animationSettings.AnimationReturn();
            }
            fromCharacter.characterParent.position = finalPos;
            PhysicalDamageResolve(skill);
            t = 0;
            AnimationCurve animationCurveReturn = fromCharacter.getCharacter().animationCurveReturn;

            while (t < moveDuration)
            {
                fromCharacter.characterParent.position = Vector2.Lerp(finalPos, initialPos, animationCurve.Evaluate(t / moveDuration));
                fromCharacter.characterParent.position += Vector3.up * animationCurveReturn.Evaluate(t / moveDuration);

                yield return null;
                t += Time.deltaTime;
            }
            fromCharacter.characterParent.position = initialPos;

        }
        else
        {
            if (skill.sprite != null)
            {
                if (animationSettings != null)
                {
                    animationSettings.AnimationChase();
                }
                yield return new WaitForSeconds(.5f);
                //Instance bullet
                GameObject bullet = new GameObject();
                SpriteRenderer spriteRend = bullet.AddComponent<SpriteRenderer>();
                spriteRend.sprite = skill.sprite;
                GameObject instance = Instantiate(bullet, fromCharacter.characterParent);
                instance.transform.localPosition = Vector3.up;
                //Traslation bullet
                while (t < moveDuration)
                {
                    Vector3 position = instance.transform.position;
                    instance.transform.position = Vector2.Lerp(initialPos, finalPos, animationCurve.Evaluate(t / moveDuration));
                    instance.transform.position += Vector3.up * currentAnimationCurveGo.Evaluate(t / moveDuration);
                    Vector3 direction = instance.transform.position - position;
                    instance.transform.LookAt(instance.transform.position + direction);
                    instance.transform.right = instance.transform.forward;
                    yield return null;
                    t += Time.deltaTime;
                }
                PhysicalDamageResolve(skill);
                //Destroy bullet
                Destroy(instance);
            }
            else
            {
                if (animationSettings != null)
                {
                    animationSettings.AnimationSkill();
                }
                yield return new WaitForSeconds(1f);
                PhysicalDamageResolve(skill);

            }
        }
        if (animationSettings != null)
        {
            animationSettings.AnimationIdle();
        }
        yield return new WaitForSeconds(1f);
        CureResolve(skill);

        action.Invoke();
    }

    private void LogAffectedBySkill(SkillData skill)
    {
        if (skill.poison.enable)
        {
            combatLog.setLogRow(oponentCharacter.characterName + " ha sido envenenado");

        }
        if (skill.bleeding.enable)
        {
            combatLog.setLogRow(oponentCharacter.characterName + " se esta desangrando");

        }
        if (skill.stun.enable)
        {
            combatLog.setLogRow(oponentCharacter.characterName + " se encuentra aturdido por " + skill.stun.turns + " turnos");

        }
        if (skill.burning.enable)
        {
            combatLog.setLogRow(oponentCharacter.characterName + " se esta quemando.");
        }

    }
    
    private void CureResolve(SkillData skill)
    {
        if (!skill.cure.enable)
            return;
        ParticleEffectsManager effectsManager = currentSlot.GetParticleEffectsManager();
        effectsManager.ActiveCure();
        currentCharacter.InitializeAffectedSkill();
        currentCharacter.RestoreHP(skill.cure.hpPlus);
    }

    private void PhysicalDamageResolve(SkillData skill)
    {
        float damage = 0;
        if (skill == currentCharacter.characterData.skills[0])
        {
            ParticleEffectsManager effectsManager = oponentSlot.GetParticleEffectsManager();
            effectsManager.isPhisycalDamage = true;
            damage = (int)skill.damage;
            ItemDataEquip sword = currentCharacter.sword;
            if(sword)
            {
                damage += sword.attack;
                if ((sword.criticProb != 0) && (Random.Range(1,100) < sword.criticProb))
                {
                    damage = damage * 2;
                }
            }
            damage = oponentCharacter.hitByBasicAttack(damage);
        }
        else
        {
            damage = oponentCharacter.hitByPhysicalDamage(skill);
        }
        oponentSlot.SetDamageHit(damage);
        string log = currentCharacter.characterName + " causo  " + damage + " de daño a " + oponentCharacter.characterName + " con la habilidad " + skill.skillName;
        combatLog.setLogRow(log);

    }

    private void PoisonResolve(SkillData skill)
    {
        if (!skill.poison.enable)
        {
            return;
        }
        currentCharacter.DecreaseHp(skill.poison.damage);
        currentSlot.SetDamageHit(skill.poison.damage);
        combatLog.setLogRow(currentCharacter.characterName + " recibió " + skill.poison.damage + " de daño por veneno");

        skill.poison.damage--;
        if (skill.poison.damage <= 0)
            skill.poison.enable = false;
    }

    private void BleedingResolve(SkillData skill)
    {
        if (!skill.bleeding.enable)
        {
            return;
        }
        int damage = (int)(currentCharacter.characterData.maxHp * skill.bleeding.percentDamage);
        currentCharacter.DecreaseHp(damage);
        currentSlot.SetDamageHit(damage);
        combatLog.setLogRow(currentCharacter.characterName + " recibió " + damage + " de daño por sangrado");

        skill.bleeding.turns--;
        if (skill.bleeding.turns <= 0)
            skill.bleeding.enable = false;
    }

    private bool StunResolve(SkillData skill)
    {
        bool stuned = false;
        ParticleEffectsManager effectsManager = currentSlot.GetParticleEffectsManager();

        if (skill.stun.enable)
        {
            stuned = true;
            effectsManager.EnableStunEffect();
            combatLog.setLogRow(currentCharacter.characterName + " se encuentra en stun por " + skill.stun.turns + " turnos");
            skill.stun.turns--;

            if (skill.stun.turns <= 0)
                skill.stun.enable = false;
        }
        else
            effectsManager.DisableStunEffect();
        return stuned;
    }

    private void BurningResolve(SkillData skill)
    {
        if (!skill.burning.enable)
        {
            return;
        }
        int damage = (int)(currentCharacter.characterData.maxHp * skill.burning.damage);

        currentCharacter.DecreaseHp(damage);
        currentSlot.SetDamageHit(damage);
        combatLog.setLogRow(currentCharacter.characterName + " recibió " + damage + " de daño de quemadura");

        skill.burning.turns--;
        if (skill.burning.turns <= 0)
            skill.burning.enable = false;
    }

    //Cuando cualquier skill es presionada cambia de turno
    private void ChangeTurn()
    {
        attackInProgress = false;
        ClearSlots();
        if (slotManagerTeam1.GetCharactersCount() > 0 && slotManagerTeam2.GetCharactersCount() > 0)
        {
            if (turnTeam == 1)
            {
                turnTeam = 0;
                slotManagerTeam2.SelectFirst();
                slotManagerTeam2.DesmarkAllSlots();
                slotManagerTeam1.NextTurnMark();
                currentSlot = slotManagerTeam1.GetCurrentTurnSlot();
                currentCharacter = currentSlot.getCharacter();
            }
            else
            {
                turnTeam = 1;
                slotManagerTeam1.SelectFirst();
                slotManagerTeam1.DesmarkAllSlots();
                slotManagerTeam2.NextTurnMark();
                currentSlot = slotManagerTeam2.GetCurrentTurnSlot();
                currentCharacter = currentSlot.getCharacter();
            }
        }
        stuned = StunResolve(currentCharacter.GetAffectedSkill());

    }

    private void ClearSlots()
    {
        //Remueve los slots con HP = 0
        if (currentSlot.GetHP() == 0)
        {
            currentSlot.getCharacter().characterData.ClearBuff();
            slotManagerTeam1.RemoveSlot(currentSlot);
            slotManagerTeam2.RemoveSlot(currentSlot);
        }
        if (oponentSlot.GetHP() == 0)
        {
            oponentSlot.getCharacter().characterData.ClearBuff();
            slotManagerTeam1.RemoveSlot(oponentSlot);
            slotManagerTeam2.RemoveSlot(oponentSlot);
        }
    }



}

