using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonSelected : MonoBehaviour, ISelectHandler
{
    public SelectedButton OnSelectButton;

    public void OnSelect(BaseEventData eventData)
    {
        OnSelectButton.Invoke(this);
    }

    [Serializable]
    public class SelectedButton : UnityEvent<UIButtonSelected>{ };
}
