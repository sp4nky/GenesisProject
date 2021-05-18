using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0CU5TBulletHit : MonoBehaviour
{

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void EndHitAnimation()
    {
        anim.SetBool("Hit", false);
    }

    public void StartHitAnimation()
    {
        anim.SetBool("Hit", true);
    }
}
