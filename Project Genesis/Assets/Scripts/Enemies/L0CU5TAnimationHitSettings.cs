using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0CU5TAnimationHitSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void disableAnimator()
    {
        anim.enabled = false;
    }
}
