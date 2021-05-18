using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationSettings : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenMenuAnimation()
    {
        anim.SetBool("Open", true);
    }

    public void CloseMenuAnimation()
    {
        anim.SetBool("Open", false);
    }

    public void Hidde()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}
