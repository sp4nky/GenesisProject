using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndNivel1 : MonoBehaviour
{
    public UnityEvent OnFinalNivel1;
    public UnityEvent GoToNivel2;


    public void endNivel1()
    {
        OnFinalNivel1.Invoke();
    }

    public void StartNivel2()
    {
        GoToNivel2.Invoke();
    }
}
