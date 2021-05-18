using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPopUp : MonoBehaviour
{
    public Text question;
    public Button btnYes;
    public Button btnNo;

    private void Start()
    {
        GameManagement.instance.confirmationPopUp = this;
        Hide();
    }

    public void Show(string question, Action actionYes, Action actionNo)
    {
        gameObject.SetActive(true);
        this.question.text = question;
        btnYes.onClick.RemoveAllListeners();
        btnYes.onClick.AddListener(actionYes.Invoke);
        btnYes.onClick.AddListener(Hide);
        btnNo.onClick.RemoveAllListeners();
        if(actionNo !=null) btnNo.onClick.AddListener(actionNo.Invoke);
        btnNo.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
