using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenAnimationSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManagement.instance.blackScreen = this;
    }

    public void BlackIn(bool goToBlack)
    {
        anim.SetBool("blackIn",goToBlack);
    }
}
