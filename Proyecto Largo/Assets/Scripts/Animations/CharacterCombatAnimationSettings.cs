using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatAnimationSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimationIdle()
    {
        anim.SetTrigger("Idle");
    }

    public void AnimationHit()
    {
        anim.SetTrigger("Hit");
    }

    public void AnimationChase()
    {
        anim.SetTrigger("Chase");
    }

    public void AnimationAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void AnimationReturn()
    {
        anim.SetTrigger("Return");
    }

    public void AnimationSkill()
    {
        anim.SetTrigger("Skill");
    }
}
