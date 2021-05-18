using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapForContinue : MonoBehaviour
{
    private Animator anim;
    public UnityEvent OnKeyPressed;
    private bool reading = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && reading)
        {
            OnKeyPressed.Invoke();
        }
        if (Input.anyKey)
        {
            anim.SetTrigger("KeyPressed");
            reading = true;
        }

    }

    public void InvokeEvents()
    {
        OnKeyPressed.Invoke();
    }

}
