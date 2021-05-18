using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxTyping : MonoBehaviour
{
    public Text txtName;
    public Text txtContent;
    public float timePerLetter=.1f;
    private string currentText;
    public bool isTyping
    {
        get; private set;
    }


    public void SetText(string name, string content)
    {
        gameObject.SetActive(true);
        currentText = content;
        isTyping = true;
        txtName.text = name + ":";
        txtContent.text = "";
        StartCoroutine(typingContent(content));
    }

    public void Hide()
    {
        gameObject.SetActive(false);

    }


    public void CompleteText()
    {
        StopAllCoroutines();
        txtContent.text = currentText;
        isTyping = false;
    }

    private IEnumerator typingContent(string content)
    {
        isTyping = true;
        int i = 0;
        while (i < content.Length)
        {
            txtContent.text += content[i];
            i++;
            yield return new WaitForSecondsRealtime(timePerLetter);
        }
        yield return null;
        isTyping = false;
    }
}
