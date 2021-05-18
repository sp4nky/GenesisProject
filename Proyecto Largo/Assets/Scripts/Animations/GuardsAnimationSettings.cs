using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsAnimationSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenGate()
    {
        anim.SetBool("Open", true);

    }

    public void CloseGate()
    {
        anim.SetBool("Open", false);
    }

}
