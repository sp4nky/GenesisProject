using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsManager : MonoBehaviour
{
    public ParticleSystem bleedingEffect;
    public ParticleSystem stunEffect;
    public ParticleSystem fireEffect;
    public ParticleSystem cureEffect;
    public ParticleSystem poisonEffect;
    private List<ParticleSystem> currentEffects = new List<ParticleSystem>();
    public bool isPhisycalDamage;

    public void SetCurrentParticleEffect(SkillData skill)
    {
        if (!skill)
        {
            return;
        }
        currentEffects.Clear();
        if (skill.poison.enable)
            currentEffects.Add(poisonEffect);
        if (skill.burning.enable)
            currentEffects.Add(fireEffect);
        if (skill.bleeding.enable)
            currentEffects.Add(bleedingEffect);
        if (skill.stun.enable)
            EnableStunEffect();
        else
            DisableStunEffect();
    }

    public void ActiveBleeding()
    {
        if(bleedingEffect) bleedingEffect.Play();
    }
    public void ActiveCure()
    {
        if (cureEffect) cureEffect.Play();
    }


    public void ActiveEffects()
    {
        if (!isPhisycalDamage)
        {
            foreach (ParticleSystem particle in currentEffects)
                particle.Play();
        }
        else
            isPhisycalDamage = false;
    }

    public void EnableStunEffect()
    {
        stunEffect.Play();
    }

    public void DisableStunEffect()
    {
        stunEffect.Stop();
    }
}
