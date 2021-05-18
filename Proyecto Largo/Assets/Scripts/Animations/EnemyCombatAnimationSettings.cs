using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatAnimationSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void AnimationHit()
    {
        anim.SetTrigger("Hit");
    }
}
